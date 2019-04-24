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

        public List<Ride> GetRides()
        {
            List<Ride> rides = new List<Ride>();
            string sql = "SELECT * FROM dbo.Rides";
            DataSet ds = ExecuteQuery(sql);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                RideCategory rideCategory = GetRideCategory((int)row["CategoryId"]);

                Ride ride = new Ride(
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
