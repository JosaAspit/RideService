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
    public class CreateModel : PageModel
    {
        RideRepository rp = new RideRepository();
        CategoryRepository cp = new CategoryRepository();
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string Description { get; set; }
        [BindProperty]
        public int CategoryId { get; set; }
        
        public string Message { get; set; }

        public SelectList CategoryIds { get; set; }
        public void OnGet()
        {
            CategoryIds = new SelectList(cp.GetRideCategories(), "Id", "Name");
        }

        public IActionResult OnPost()
        {
            rp.InsertRide(new Ride(new RideCategory(CategoryId), Description, Name));
            
                return new RedirectToPageResult("Index");
           
        }
    }
}