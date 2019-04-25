using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RideService.Logic;
using RideService.Models;

namespace RideService.Pages.Reports
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public Ride Ride { get; set; }

        public void OnGet()
        {
            RideRepository rideRepository = new RideRepository();

            Ride = rideRepository.GetRide(Id);
        }
    }
}