using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RideService.Logic;
using RideService.Models;

namespace RideService.Pages.Rides
{
    public class DetailsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public Ride Ride { get; set; }
        public int TotalBreakdowns { get; set; }
        public int DaysSinceLastBreakdown { get; set; }

        public void OnGet()
        {
            ReportRepository reportRepository = new ReportRepository();
            RideRepository rideRepository = new RideRepository();

            Ride = rideRepository.GetRide(Id);
            TotalBreakdowns = reportRepository.TotalBreakdowns(Id);
            DaysSinceLastBreakdown = reportRepository.DaysSinceLastRideBreakdown(Id);

            if (DaysSinceLastBreakdown == -1)
            {
                DaysSinceLastBreakdown = 0;
            }
        }
    }
}