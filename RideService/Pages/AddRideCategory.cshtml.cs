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
            int res = cr.InsertRideCategory(new RideCategory(Description, Name));
            if (res == 1)
            {
                ViewData["Message"] = "Ride category added succesfully";
            }
            else if (res == 0)
            {
                ViewData["Message"] = "Error! No empty values";
            }
            else if (res == -1)
            {
                ViewData["Message"] = "Error! Name already exists";
            }
        }
    }
}