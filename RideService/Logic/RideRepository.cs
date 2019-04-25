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
        public Ride GetRide(int id)
        {
            List<Ride> rides = GetRides();

            foreach (Ride ride in rides)
            {
                if (ride.Id == id)
                {
                    return ride;
                }
            }

            return null;
        }

        public List<Ride> SearchRides(string param)
        {
            CategoryRepository categoryRepository = new CategoryRepository();
            ReportRepository reportRepository = new ReportRepository();

            string sql = $"SELECT * FROM Rides WHERE Name LIKE '%{param}%'";
            DataSet ds = ExecuteQuery(sql);
            List<Ride> rides = new List<Ride>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                RideCategory rideCategory = categoryRepository.GetRideCategory((int)row["CategoryId"]);
                List<Report> reports = reportRepository.GetReportsForRide((int)row["RideId"]);

                Ride ride = new Ride(
                    reports,
                    (Status)row["Status"],
                    rideCategory,
                    (string)row["Description"],
                    (string)row["Name"],
                    (int)row["RideId"]
                );

                rides.Add(ride);
            }

            return rides;
        }

        public List<Ride> GetRides()
        {
            CategoryRepository categoryRepository = new CategoryRepository();
            ReportRepository reportRepository = new ReportRepository();

            List<Ride> rides = new List<Ride>();
            string sql = "SELECT * FROM Rides INNER JOIN Reports ON Rides.RideId = Reports.RideId";
            DataSet ds = ExecuteQuery(sql);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                RideCategory rideCategory = categoryRepository.GetRideCategory((int)row["CategoryId"]);
                Ride rideToAdd = null;

                bool rideExists = false;
                foreach (Ride ride in rides)
                {
                    if (ride.Id == (int)row["RideId"])
                    {
                        rideExists = true;

                        Report report = new Report(
                            (string)row["Notes"],
                            (DateTime)row["ReportTime"],
                            (Status)(int)row["Status"],
                            ride,
                            (int)row["RideId"]
                        );

                        ride.Reports.Add(report);

                        if (rideToAdd is null)
                        {
                            rideToAdd = ride;
                        }
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

                    Report report = new Report(
                        (string)row["Notes"],
                        (DateTime)row["ReportTime"],
                        (Status)(int)row["Status"],
                        ride,
                        (int)row["RideId"]
                    );

                    ride.Reports.Add(report);

                    rideToAdd = ride;
                }

                rides.Add(rideToAdd);
            }

            return rides;
        }
        public int InsertRide(Ride r)
        {
            string q = $"insert into Rides (Name, Description, CategoryId, Status) values('{r.Name}', '{r.Description}', {r.Category}, {r.Status})";

            return ExecuteNonQuery(q);
        } 
    }
}
