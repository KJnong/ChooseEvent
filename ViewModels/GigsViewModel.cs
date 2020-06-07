using ChooseEvent2.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChooseEvent2.ViewModels
{
    public class GigsViewModel
    {

        public string Date { get; set; }

        public string Time { get; set; }

        public string Venue { get; set; }

        public string Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

    }
}