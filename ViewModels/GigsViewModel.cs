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

        public DateTime DateTime { get 
            {
                return DateTime.Parse(String.Format("{0} {1}", Date, Time));
            } }

        public string Venue { get; set; }

        public byte Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

    }
}