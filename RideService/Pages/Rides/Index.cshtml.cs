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
    public class IndexModel : PageModel
    {
        RideRepository rp = new RideRepository();
        public List<Ride> Rides { get; set; }
        public void OnGet()
        {
            Rides = rp.GetRides();
        }
    }
}