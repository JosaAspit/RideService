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
            List<Report> reports = new List<Report>();
            string sql = $"SELECT * FROM Reports WHERE RideId = {id}";
            DataSet ds = ExecuteQuery(sql);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                
            }
        }
    }
}
