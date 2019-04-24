using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RideService.Models;

namespace RideService.Pages.Rides
{
    public class IndexModel : PageModel
    {
        public List<Ride> Rides { get; set; }
        public void OnGet()
        {

        }
    }
}