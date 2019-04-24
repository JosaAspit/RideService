using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RideService.Logic
{
    public class BaseRepository
    {
        private string conString = @"Data Source=cvdb3,1488;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public DataSet ExecuteQuery(string sql)
        {
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand com = new SqlCommand(sql, con))
            using (SqlDataAdapter dap = new SqlDataAdapter(com))
            {
                dap.Fill(ds);
            }

            return ds;
        }

        public int ExecuteNonQuery(string sql)
        {
            int rowsAffected = 0;

            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand com = new SqlCommand(sql, con))
            {
                con.Open();

                rowsAffected = com.ExecuteNonQuery();
            }

            return rowsAffected;
        }
    }
}
