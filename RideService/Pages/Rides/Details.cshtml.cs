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

        public void OnGet()
        {
            RideRepository rs = new RideRepository();

            Ride = rs.GetRide(Id);
        }
    }
}