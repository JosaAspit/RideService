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

            List<Report> reports = new List<Report>();
            string sql = $"SELECT * FROM Reports WHERE RideId = {id}";
            DataSet ds = ExecuteQuery(sql);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Ride ride = rideRepository.GetRide((int)row["RideId"]);

                Report report = new Report(
                    (string)row["Notes"],
                    (DateTime)row["ReportTime"],
                    (Status)(int)row["Status"],
                    ride,
                    (int)row["RideId"]
                );

                reports.Add(report);
            }

            return reports;
        }
    }
}
