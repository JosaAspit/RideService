using RideService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RideService.Logic
{
    public class RideRepository : BaseRepository
    {
        public Ride GetRide(int id, List<Ride> ridesList = null)
        {
            List<Ride> rides = ridesList;
            if (rides is null)
            {
                rides = GetRides();
            }

            foreach (Ride ride in rides)
            {
                if (ride.Id == id)
                {
                    return ride;
                }
            }

            return null;
        }

        public List<Ride> SearchRides(string param, int categoryId, int status)
        {
            CategoryRepository categoryRepository = new CategoryRepository();
            ReportRepository reportRepository = new ReportRepository();
            string sql = "";
            if (!string.IsNullOrEmpty(param))
            {
                sql = $"SELECT * FROM Rides WHERE Name LIKE '%{param}%'";
                if (categoryId != -1)
                {
                    sql += $"AND CategoryId = {categoryId}";
                }
                if (status != -1)
                {
                    sql += $"AND Status = {status}";
                }
            }
            else if (categoryId != -1)
            {
                sql = $"SELECT * FROM Rides WHERE CategoryId = {categoryId}";
                if (status != -1)
                {
                    sql += $"AND Status = {status}";
                }
            }
            else if (status != -1)
            {
                sql = $"SELECT * FROM Rides WHERE Status = {status}";
            }
            else
            {
                sql = $"SELECT * FROM Rides";
            }
            DataSet ds = ExecuteQuery(sql);
            List<Ride> rides = new List<Ride>();
            List<RideCategory> categories = categoryRepository.GetRideCategories();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                
                List<Report> reports = reportRepository.GetReportsForRide((int)row["RideId"]);

                Ride ride = new Ride(
                    (Status)row["Status"],
                    (string)row["Description"],
                    (string)row["Name"],
                    (int)row["RideId"]
                );
                ride.Category = categories.Find(c => c.Id == (int)row["CategoryId"]);
                rides.Add(ride);
            }

            return rides;
        }
        

        public List<Ride> GetRides()
        {
            CategoryRepository categoryRepository = new CategoryRepository();
            ReportRepository reportRepository = new ReportRepository();

            List<Ride> rides = new List<Ride>();
            string sql = "SELECT Rides.RideId, Rides.Name, Rides.Description, Rides.CategoryId, Rides.Status, Reports.Status AS ReportStatus, Reports.ReportTime, Reports.Notes, Reports.RideId AS ReportRideId FROM Rides LEFT JOIN Reports ON Rides.RideId = Reports.RideId";

            DataSet ds = ExecuteQuery(sql);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                RideCategory rideCategory = categoryRepository.GetRideCategory((int)row["CategoryId"]);

                bool rideExists = false;

                if (rides.Count == 0)
                {
                    Ride ride = new Ride(
                        (Status)(int)row["Status"],
                        rideCategory,
                        (string)row["Description"],
                        (string)row["Name"],
                        (int)row["RideId"]
                    );

                    if (int.TryParse(row["ReportStatus"].ToString(), out int status))
                    {
                        Report report = new Report(
                            (string)row["Notes"],
                            (DateTime)row["ReportTime"],
                            (Status)(int)row["ReportStatus"],
                            ride,
                            (int)row["ReportRideId"]
                        );

                        ride.Reports.Add(report);
                    }

                    rides.Add(ride);
                }
                else
                {
                    foreach (Ride ride in rides)
                    {
                        if (ride.Id == (int)row["RideId"])
                        {
                            if (int.TryParse(row["ReportStatus"].ToString(), out int status))
                            {
                                Report report = new Report(
                                    (string)row["Notes"],
                                    (DateTime)row["ReportTime"],
                                    (Status)(int)row["ReportStatus"],
                                    ride,
                                    (int)row["ReportRideId"]
                                );

                                ride.Reports.Add(report);
                            }

                            rideExists = true;
                        }
                    }

                    if (!rideExists)
                    {
                        Ride ride = new Ride(
                            (Status)(int)row["Status"],
                            rideCategory,
                            (string)row["Description"],
                            (string)row["Name"],
                            (int)row["RideId"]
                        );

                        if (int.TryParse(row["ReportStatus"].ToString(), out int status))
                        {
                            Report report = new Report(
                                (string)row["Notes"],
                                (DateTime)row["ReportTime"],
                                (Status)(int)row["ReportStatus"],
                                ride,
                                (int)row["ReportRideId"]
                            );

                            ride.Reports.Add(report);
                        }

                        rides.Add(ride);
                    }
                }
            }

            return rides;
        }
        public int InsertRide(Ride r)
        {
            string q = $"insert into Rides (Name, Description, CategoryId, Status) values('{r.Name}', '{r.Description}', {r.Category.Id}, 0)";

            return ExecuteNonQuery(q);
        }

        public Ride GetMostBrokenRide(int? categoryId = null, List<Ride> ridesList = null)
        {
            RideRepository rideRepository = new RideRepository();
            ReportRepository reportRepository = new ReportRepository();
            List<Ride> rides = ridesList;
            if (rides is null)
            {
                rides = rideRepository.GetRides();
            }
            Ride rideToReturn = null;
            int breakdowns = 0;

            foreach (Ride ride in rides)
            {
                if (rideToReturn is null)
                {
                    if (categoryId is null)
                    {
                        rideToReturn = ride;
                        breakdowns = reportRepository.TotalBreakdowns(ride.Id, rides);
                    }
                    else if (ride.Category.Id == categoryId)
                    {
                        rideToReturn = ride;
                        breakdowns = reportRepository.TotalBreakdowns(ride.Id, rides);
                    }
                }
                else
                {
                    int rideBreakdowns = reportRepository.TotalBreakdowns(ride.Id, rides);
                    if (rideBreakdowns > breakdowns)
                    {
                        if (categoryId is null)
                        {
                            rideToReturn = ride;
                            breakdowns = rideBreakdowns;
                        }
                        else if (ride.Id == categoryId)
                        {
                            rideToReturn = ride;
                            breakdowns = rideBreakdowns;
                        }
                    }
                }
            }

            return rideToReturn;
        }

        public Ride GetLastBrokenRide(List<Ride> ridesList = null)
        {
            RideRepository rideRepository = new RideRepository();
            ReportRepository reportRepository = new ReportRepository();
            List<Ride> rides = ridesList;
            if (rides is null)
            {
                rides = rideRepository.GetRides();
            }
            Ride rideToReturn = null;
            int lastBreakdown = 0;

            foreach (Ride ride in rides)
            {
                int rideLastBreakdown = reportRepository.DaysSinceLastRideBreakdown(ride.Id, rides);
                if (rideToReturn is null && rideLastBreakdown != -1)
                {
                    rideToReturn = ride;
                    lastBreakdown = reportRepository.DaysSinceLastRideBreakdown(ride.Id, rides);
                }
                else if (rideLastBreakdown < lastBreakdown && rideLastBreakdown != -1)
                {
                    rideToReturn = ride;
                    lastBreakdown = rideLastBreakdown;
                }
            }

            return rideToReturn;
        }

        public Ride GetLeastBrokenRide(List<Ride> ridesList = null)
        {
            RideRepository rideRepository = new RideRepository();
            ReportRepository reportRepository = new ReportRepository();
            List<Ride> rides = ridesList;
            if (rides is null)
            {
                rides = rideRepository.GetRides();
            }
            Ride rideToReturn = null;
            int breakdowns = 0;

            foreach (Ride ride in rides)
            {
                if (rideToReturn is null)
                {
                    rideToReturn = ride;
                    breakdowns = reportRepository.TotalBreakdowns(ride.Id, rides);
                }
                else
                {
                    int rideBreakdowns = reportRepository.TotalBreakdowns(ride.Id, rides);
                    if (rideBreakdowns < breakdowns)
                    {
                        rideToReturn = ride;
                        breakdowns = rideBreakdowns;
                    }
                }
            }

            return rideToReturn;
        }
    }
}
