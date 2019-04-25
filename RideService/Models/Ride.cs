using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideService.Models
{
    public class Ride
    {
        private int id;
        private string name;
        private string description;
        private RideCategory category;
        private Status status;
        private List<Report> reports = new List<Report>();

        public Ride(Status status, RideCategory category, string description, string name, int id)
        {
            Status = status;
            Category = category;
            Description = description;
            Name = name;
            Id = id;
        }

        public Ride(List<Report> reports, Status status, RideCategory category, string description, string name, int id) : this(status, category, description, name, id)
        {
            Reports = reports;
        }

        public List<Report> Reports
        {
            get { return reports; }
            set { reports = value; }
        }


        public Status Status
        {
            get { return status; }
            set { status = value; }
        }


        public RideCategory Category
        {
            get { return category; }
            set { category = value; }
        }


        public string Description
        {
            get { return description; }
            set { description = value; }
        }


        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        //public int NumberOfShutdowns()
        //{

        //}

        //public int DaysSinceLasyShutdown()
        //{

        //}

        //public string GetShortDescription()
        //{

        //}
    }
}
