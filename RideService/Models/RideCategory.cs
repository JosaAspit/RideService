using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideService.Models
{
    public class RideCategory
    {
        private int id;
        private string name;
        private string description;

        public RideCategory(int id)
        {
            Id = id;
        }

        public RideCategory(string description, string name, int id)
        {
            Description = description;
            Name = name;
            Id = id;
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

    }
}
