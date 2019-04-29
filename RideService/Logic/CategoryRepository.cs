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

        public List<RideCategory> GetRideCategories()
        {
            string sql = $"SELECT * FROM RideCategories";
            DataSet ds = ExecuteQuery(sql);
            List<RideCategory> rcl = new List<RideCategory>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                RideCategory rideCategory = new RideCategory(
                    (string)row["Description"],
                    (string)row["Name"],
                    (int)row["RideCategoryId"]
                );

                rcl.Add(rideCategory);
            }

            return rcl;
        }

        public RideCategory GetMostBrokenCategory(List<Ride> ridesList = null)
        {
            CategoryRepository categoryRepository = new CategoryRepository();
            RideRepository rideRepository = new RideRepository();
            ReportRepository reportRepository = new ReportRepository();
            List<RideCategory> categories = categoryRepository.GetRideCategories();
            RideCategory categoryToReturn = null;
            int breakdowns = 0;
            List<Ride> rides = ridesList;

            foreach (RideCategory category in categories)
            {
                if (categoryToReturn is null)
                {
                    categoryToReturn = category;
                    if (rides is null)
                    {
                        breakdowns = reportRepository.TotalBreakdowns(rideRepository.GetMostBrokenRide(category.Id).Id);
                    }
                    else
                    {
                        breakdowns = reportRepository.TotalBreakdowns(rideRepository.GetMostBrokenRide(category.Id, rides).Id, rides);
                    }
                }
                else
                {
                    int categoryBreakdowns = 0;
                    if (rides is null)
                    {
                        Ride mostBrokenRide = rideRepository.GetMostBrokenRide(category.Id);
                        if (mostBrokenRide != null)
                        {
                            categoryBreakdowns = reportRepository.TotalBreakdowns(mostBrokenRide.Id);
                        }
                    }
                    else
                    {
                        Ride mostBrokenRide = rideRepository.GetMostBrokenRide(category.Id, rides);
                        if (mostBrokenRide != null)
                        {
                            categoryBreakdowns = reportRepository.TotalBreakdowns(mostBrokenRide.Id, rides);
                        }
                    }

                    if (categoryBreakdowns > breakdowns)
                    {
                        categoryToReturn = category;
                        breakdowns = categoryBreakdowns;
                    }
                }
            }

            return categoryToReturn;
        }
        public int InsertRideCategory(RideCategory r)
        {
            return ExecuteNonQuery($"Insert into RideCategories(Name, Description) values('{r.Name}','{r.Description}')");
        }
    }
}
