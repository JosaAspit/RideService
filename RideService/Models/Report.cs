using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideService.Models
{
    public class Report
    {
        private int id;
        private Ride ride;
        private Status status;
        private DateTime reportTime;
        private string notes;

        public Report(string notes, DateTime reportTime, Status status, Ride ride, int id)
        {
            Notes = notes;
            ReportTime = reportTime;
            Status = status;
            Ride = ride;
            Id = id;
        }

        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }


        public DateTime ReportTime
        {
            get { return reportTime; }
            set { reportTime = value; }
        }


        public Status Status
        {
            get { return status; }
            set { status = value; }
        }


        public Ride Ride
        {
            get { return ride; }
            set { ride = value; }
        }


        public int Id
        {
            get { return id; }
            set { id = value; }
        }

    }
}
