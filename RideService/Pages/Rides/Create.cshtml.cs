using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RideService.Models;

namespace RideService.Pages.Rides
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string Description { get; set; }
        [BindProperty]
        public Status Status { get; set; }
        [BindProperty]
        public int CategoryId { get; set; }

        public SelectList CategoryIds { get; set; }
        public void OnGet()
        {

        }
    }
}