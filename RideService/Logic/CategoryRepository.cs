using RideService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RideService.Logic
{
    public class CategoryRepository : BaseRepository
    {
        public RideCategory GetRideCategory(int id)
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
    }
}
