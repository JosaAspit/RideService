using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RideService.Logic;
using RideService.Models;

namespace RideService.Pages
{
    [BindProperties]
    public class AddRideCategoryModel : PageModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public void OnGet()
        {
            
        }

        public void OnPost()
        {
            CategoryRepository cr = new CategoryRepository();
            if (cr.InsertRideCategory(new RideCategory(Name, Description)) == 1)
            {
                ViewData["Message"] = "Ride category added succesfully";
            }
            
        }
    }
}