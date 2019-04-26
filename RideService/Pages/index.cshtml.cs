using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RideService.Models;
using RideService.Logic;

namespace RideService.Pages
{
    public class indexModel : PageModel
    {
        public Ride MostBrokenRide { get; set; }
        public Ride LastBrokenRide { get; set; }
        public Ride LeastBrokenRide { get; set; }
        public int? DaysSinceLastBreakdown { get; set; }
        public RideCategory MostBrokenCategory { get; set; }

        public void OnGet()
        {
            CategoryRepository categoryRepository = new CategoryRepository();
            ReportRepository reportRepository = new ReportRepository();
            RideRepository rideRepository = new RideRepository();

            List<Ride> rides = rideRepository.GetRides();

            MostBrokenRide = rideRepository.GetMostBrokenRide(null, rides);
            LastBrokenRide = rideRepository.GetLastBrokenRide(rides);
            LeastBrokenRide = rideRepository.GetLeastBrokenRide(rides);
            DaysSinceLastBreakdown = reportRepository.DaysSinceLastBreakdownOnRides(rides);
            MostBrokenCategory = categoryRepository.GetMostBrokenCategory(rides);
        }
    }
}