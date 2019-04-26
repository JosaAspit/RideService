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

        public int TotalBreakdowns(int id)
        {
            RideRepository rideRepository = new RideRepository();
            Ride ride = rideRepository.GetRide(id);
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

        public int DaysSinceLastBreakdown(int id)
        {
            RideRepository rideRepository = new RideRepository();
            Ride ride = rideRepository.GetRide(id);
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
                return 0;
            }

            DateTime lastReportDate = lastReport.ReportTime.Date;
            days = (today - lastReportDate).Days;

            return days;
        }

        public List<Report> SearchReports(int rideId, DateTime date, Status? status, string note)
        {
            List<Report> matchingReports = new List<Report>();

            string q = "select * from reports ";

            if (rideId > 0)
                q += $"where RideId = {rideId} ";

            if (date != null)
                q += $"where ReportTime = '{date}' ";

            if (status != null)
                q += $"where status = {status.Value} ";

            if (string.IsNullOrEmpty(note))
                q += $"where notes like '%{note}%' ";

            DataSet ds = ExecuteQuery(q);

            matchingReports = GetReportsFromDataSet(ds);

            return matchingReports;
        }

        List<Report> GetReportsFromDataSet(DataSet ds)
        {
            List<Report> reports = new List<Report>();
            RideRepository rp = new RideRepository();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Report report = new Report(
                    (string)row["Notes"],
                    (DateTime)row["ReportTime"],
                    (Status)row["status"],
                    rp.GetRide((int)row["rideid"]),
                    (int)row["id"]);

                reports.Add(report);
            }
            return reports;
        }
    }
}
