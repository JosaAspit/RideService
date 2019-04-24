﻿using RideService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RideService.Logic
{
    public class RideRepository : BaseRepository
    {
        public List<Ride> GetRides()
        {
            List<Ride> rides = new List<Ride>();
            string sql =
                "SELECT Rides.Name, Rides.Status, Rides.Description, Reports. FROM Rides" +
                "INNER JOIN Reports ON Rides.RideId = Reports.RideId";
            DataSet ds = ExecuteQuery(sql);

            foreach (DataRow row in ds.Tables[0].Rows)
            {

            }
        }
    }
}
