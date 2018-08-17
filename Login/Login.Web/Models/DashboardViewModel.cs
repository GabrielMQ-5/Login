using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Login.Entities;

namespace Login.Models
{
    public class DashboardViewModel
    {
        public User user { get; set; }
        public string userFullname { get; set; }
        public DashboardViewModel() { }
    }
}