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
    public class AddReportModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int RideId { get; set; }
        [BindProperty]
        public string Notes { get; set; }
        [BindProperty]
        public Status Status { get; set; }
        [BindProperty]
        public DateTime ReportDate { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            ReportRepository rr = new ReportRepository();
            rr.InsertReport(new Report(Notes, ReportDate, Status, new Ride(RideId)));
            return new RedirectToPageResult("Index");
        }
    }
}