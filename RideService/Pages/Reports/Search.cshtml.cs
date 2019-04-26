using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RideService.Logic;
using RideService.Models;

namespace RideService.Pages.Reports
{
    public class SearchModel : PageModel
    {
        ReportRepository reportRepository = new ReportRepository();
        RideRepository rideRepository = new RideRepository();

        public List<Report> MatchingReports { get; set; }
        public SelectList Rideids { get; set; }

        [BindProperty(SupportsGet = true)]
        public int SearchRideId { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime SearchDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public Status SearchStatus { get; set; } = (Status)(-1);

        [BindProperty(SupportsGet = true)]
        public string SearchNote { get; set; }


        public void OnGet()
        {
            Rideids = new SelectList(rideRepository.GetRides(), "Id", "Name");

            //reportRepository.SearchReports(SearchRideId, SearchDate, SearchStatus, SearchNote);
        }
    }
}