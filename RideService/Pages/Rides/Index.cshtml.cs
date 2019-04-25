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
        [BindProperty(SupportsGet = true)]
        public string SearchName { get; set; }
        public void OnGet()
        {
            if (!string.IsNullOrEmpty(SearchName))
            {
                Rides = rp.SearchRides(SearchName);
            }
            else
            {
                Rides = rp.GetRides();
            }
            
        }
    }
}