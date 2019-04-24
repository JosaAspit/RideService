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
        CategoryRepository categoryRepository = new CategoryRepository();
        ReportRepository reportRepository = new ReportRepository();

        public Ride GetRide(int id)
        {
            string sql = $"SELECT * FROM Rides WHERE RideId = {id}";
            DataSet ds = ExecuteQuery(sql);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                RideCategory rideCategory = categoryRepository.GetRideCategory((int)row["CategoryId"]);

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

        public List<Ride> GetRides()
        {
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
        public int InsertRide(Ride r)
        {
            string q = $"insert into Rides (Name, Description, CategoryId, Status) values('{r.Name}', '{r.Description}', {r.Category}, {r.Status})";

            return ExecuteNonQuery(q);
        } 
    }
}
