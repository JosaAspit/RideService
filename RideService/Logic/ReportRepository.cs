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
            RideRepository rs = new RideRepository();
            Ride ride = rs.GetRide(id);
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
                if (lastReport is null)
                {
                    lastReport = report;
                }
                else
                {
                    if (report.ReportTime.Date < lastReport.ReportTime.Date)
                    {
                        lastReport = report;
                    }
                }
            }

            if (lastReport is null || TotalBreakdowns(id) == 0)
            {
                return 0;
            }

            DateTime lastReportDate = lastReport.ReportTime.Date;
            days = (today - lastReportDate).Days;

            return days;
        }
    }
}
