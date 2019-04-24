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
            CategoryRepository categoryRepository = new CategoryRepository();
            ReportRepository reportRepository = new ReportRepository();

            string sql = $"SELECT * FROM Rides WHERE RideId = {id}";
            DataSet ds = ExecuteQuery(sql);

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

                return ride;
            }

            return null;
        }

        public List<Ride> GetRides()
        {
            CategoryRepository categoryRepository = new CategoryRepository();
            ReportRepository reportRepository = new ReportRepository();

            List<Ride> rides = new List<Ride>();
            string sql = "SELECT * FROM dbo.Rides";
            DataSet ds = ExecuteQuery(sql);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                RideCategory rideCategory = categoryRepository.GetRideCategory((int)row["CategoryId"]);
                List<Report> reports = reportRepository.GetReportsForRide((int)row["RideId"]);

                Ride ride = new Ride(
                    reports,
                    (Status)(int)row["Status"],
                    rideCategory,
                    (string)row["Description"],
                    (string)row["Name"],
                    (int)row["RideId"]
                );

                rides.Add(ride);
            }

            return rides;
        }
    }
}
