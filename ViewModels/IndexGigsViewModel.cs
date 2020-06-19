using ChooseEvent2.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChooseEvent2.ViewModels
{
    public class IndexGigsViewModel
    {
        public IEnumerable<Gig> UpcomingGigs { get; set; }

        public bool Authorized { get; set; }

        public string Heading { get; set; }
    }
}