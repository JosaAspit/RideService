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
        private RideCategory GetRideCategory(int id)
        {
            string sql = $"SELECT * FROM RideCategories WHERE RideCategoryId = {id}";
            DataSet ds = ExecuteQuery(sql);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                RideCategory rideCategory = new RideCategory(
                    (string)row["Description"],
                    (string)row["Name"],
                    (int)row["RideCategoryId"]
                );

                return rideCategory;
            }

            return null;
        }

        private Ride GetRide(int id)
        {
            string sql = $"SELECT * FROM Rides WHERE RideId = {id}";
            DataSet ds = ExecuteQuery(sql);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                RideCategory rideCategory = GetRideCategory((int)row["CategoryId"]);

                Ride ride = new Ride(
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

        public List<Report> GetReportsForRide(int id)
        {
            List<Report> reports = new List<Report>();
            string sql = $"SELECT * FROM Reports WHERE RideId = {id}";
            DataSet ds = ExecuteQuery(sql);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Ride ride = GetRide((int)row["RideId"]);

                Report report = new Report(
                    (string)row["Notes"],
                    (DateTime)row["ReportTime"],
                    (Status)row["Status"],
                    ride,
                    (int)row["RideId"]
                );

                reports.Add(report);
            }

            return reports;
        }
    }
}
