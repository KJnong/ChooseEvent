using ChooseEvent2.Controllers;
using ChooseEvent2.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace ChooseEvent2.ViewModels
{
    public class GigsViewModel
    {
        public int Id { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
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

        public string Heading { get; set; }

        public string Action { 
            get
            {
                Expression<Func<GigsController, ActionResult>> update = (c => c.Update(null));
                Expression<Func<GigsController, ActionResult>> create = (c => c.Create(null));

                var action = (Id != 0) ? update : create;

                return (action.Body as MethodCallExpression).Method.Name;
            }
        }

    }
}