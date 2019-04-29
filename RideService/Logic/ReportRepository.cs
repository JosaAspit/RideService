using RideService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RideService.Logic
{
    public class ReportRepository : BaseRepository
    {
        public List<Report> GetReportsForRide(int id)
        {
            RideRepository rideRepository = new RideRepository();

            Ride ride = rideRepository.GetRide(id);

            return ride.Reports;
        }

        public int InsertReport(Report r)
        {
            return ExecuteNonQuery($"insert into Reports(Status, ReportTime, Notes, RideId) values({(int)r.Status}, '{r.ReportTime.ToString("dd-MM-yyyy")} 00:00:00', '{r.Notes}', {r.Ride.Id})");
        }

        public int TotalBreakdowns(int id, List<Ride> ridesList = null)
        {
            RideRepository rideRepository = new RideRepository();
            List<Ride> rides = ridesList;
            Ride ride = null;
            if (rides is null)
            {
                ride = rideRepository.GetRide(id);
            }
            else
            {
                ride = rideRepository.GetRide(id, rides);
            }
            int breakdowns = 0;

            foreach (Report report in ride.Reports)
            {
                if (report.Status == Status.Broken)
                {
                    breakdowns++;
                }
            }

            return breakdowns;
        }



        public int DaysSinceLastRideBreakdown(int id, List<Ride> ridesList = null)
        {
            RideRepository rideRepository = new RideRepository();
            List<Ride> rides = ridesList;
            Ride ride = null;
            if (rides is null)
            {
                ride = rideRepository.GetRide(id);
            }
            else
            {
                ride = rideRepository.GetRide(id, rides);
            }
            int days = 0;

            DateTime today = DateTime.Now.Date;
            Report lastReport = null;

            foreach (Report report in ride.Reports)
            {
                if (report.Status == Status.Broken)
                {
                    if (lastReport is null)
                    {
                        lastReport = report;
                    }
                    else
                    {
                        if (report.ReportTime.Date > lastReport.ReportTime.Date)
                        {
                            lastReport = report;
                        }
                    }
                }
            }

            if (lastReport is null)
            {
                return -1;
            }

            DateTime lastReportDate = lastReport.ReportTime.Date;
            days = (today - lastReportDate).Days;

            return days;
        }

        public int? DaysSinceLastBreakdownOnRides(List<Ride> ridesList = null)
        {
            RideRepository rideRepository = new RideRepository();
            List<Ride> rides = ridesList;
            if (rides is null)
            {
                rides = rideRepository.GetRides();
            }
            Ride rideToReturn = null;
            int days = 0;

            foreach (Ride ride in rides)
            {
                int daysSinceBreakdown = DaysSinceLastRideBreakdown(ride.Id, rides);
                if (rideToReturn is null && daysSinceBreakdown != -1)
                {
                    days = daysSinceBreakdown;
                    rideToReturn = ride;
                }
                else
                {
                    if (daysSinceBreakdown < days && daysSinceBreakdown != -1)
                    {
                        days = daysSinceBreakdown;
                        rideToReturn = ride;
                    }
                }
            }

            if (rideToReturn is null)
            {
                return null;
            }

            return days;
        }


        public List<Report> GetAllReports()
        {
            string q = "select * from reports";
            DataSet ds = ExecuteQuery(q);

            return ConvertDataSetToReports(ds);
        }

        List<Report> ConvertDataSetToReports(DataSet dataSet)
        {
            List<Report> reports = new List<Report>();
            RideRepository rb = new RideRepository();

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                reports.Add(new Report(
                    (string)row["notes"],
                    (DateTime)row["reporttime"],
                    (Status)row["status"],
                    rb.GetRide((int)row["rideid"]),
                    (int)row["reportid"]
                    ));
            }
            return reports;
        }

        public List<Report> GetAllMatchingReports(int rideId, DateTime date, int status, string notes)
        {
            string q = "select * from reports where";

            if (rideId > 0)
                q += $" rideid = {rideId} and";
            if (date != default(DateTime))
                q += $" reporttime = '{date.ToString("yyyy-MM-dd")}' and";
            if (status != -1)
                q += $" status = {status} and";
            if (!string.IsNullOrEmpty(notes))
                q += $" notes like '%{notes}%' and";

            q = q.Substring(0, q.Length - 3);

            DataSet ds = ExecuteQuery(q);

            return ConvertDataSetToReports(ds);
        }


    }

}
