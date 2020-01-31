using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Install  entity framework 6 on Tools > Manage Nuget Packages > Microsoft Entity Framework (ver 6.4)
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace PetGrooming.Models
{
    public class GroomService
    {

        public int GroomServiceID { get; set; }
        public string ServiceName { get; set; }
        public int ServiceCost { get; set; }
        public int ServiceDuration { get; set; }
    }
}