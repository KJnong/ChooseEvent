using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChooseEvent2.ViewModels
{
    public class InfoViewModel
    {
        public string Name { get; set; }

        public string Vanue { get; set; }

        public DateTime DateTime { get; set; }

        public bool Following { get; set; }

        public bool Attending { get; set; }

    }
}