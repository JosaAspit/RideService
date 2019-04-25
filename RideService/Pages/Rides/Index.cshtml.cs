using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RideService.Logic;
using RideService.Models;

namespace RideService.Pages.Rides
{
    public class IndexModel : PageModel
    {
        RideRepository rp = new RideRepository();
        CategoryRepository cp = new CategoryRepository();
        public List<Ride> Rides { get; set; }
        public SelectList CategoryIds { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchName { get; set; }
        [BindProperty(SupportsGet = true)]
        public Status SearchStatus { get; set; } = (Status)(-1);
        [BindProperty(SupportsGet = true)]
        public int SearchCategoryId { get; set; } = -1;
        public void OnGet()
        {
            CategoryIds = new SelectList(cp.GetRideCategories(), "Id", "Name");
            if (!string.IsNullOrEmpty(SearchName) || (int)SearchStatus != -1 || SearchCategoryId != -1)
            {
                Rides = rp.SearchRides(SearchName, SearchCategoryId, (int)SearchStatus);
            }
            else
            {
                Rides = rp.GetRides();
            }

        }
    }
}