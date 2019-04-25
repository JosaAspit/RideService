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
        public Status SearchStatus { get; set; }
        [BindProperty(SupportsGet = true)]
        public int SearchCategoryId { get; set; }
        public void OnGet()
        {
            CategoryIds = new SelectList(new List<RideCategory> { new RideCategory("våd", "vandland", 1), new RideCategory("whee", "pendul", 2) }, "Id", "Name");
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