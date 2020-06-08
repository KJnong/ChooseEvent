using ChooseEvent2.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChooseEvent2.ViewModels
{
    public class GigsViewModel
    {
        [Required]
        public string Date { get; set; }

        [Required]
        public string Time { get; set; }

        [Required]
        public DateTime DateTime { get 
            {
                return DateTime.Parse(String.Format("{0} {1}", Date, Time));
            } }

        [Required]
        public string Venue { get; set; }

        [Required]
        public byte Genre { get; set; }

        [Required]
        public IEnumerable<Genre> Genres { get; set; }

    }
}