using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChooseEvent2.Models
{
    public class Notification
    {
        public int Id { get; private set; }

        public DateTime DateTime { get; private set; }

        public NotificationType Type { get; private set; }

        public DateTime? OriginalDateTime { get; set; }

        public string OriginalVanue { get; set; }

        [Required]
        public Gig Gig { get; private set; }

        public Notification()
        {

        }

        public Notification(NotificationType type, Gig gig)
        {
            if (gig == null)
                throw new ArgumentException("Gig");

            DateTime = DateTime.Now;
            Type = type;
            Gig = gig;
        }

        public Notification(NotificationType type, Gig gig, DateTime originalDateTime, string originalVanue)
        {
            if (gig == null)
                throw new ArgumentException("Gig");

            OriginalDateTime = originalDateTime;
            OriginalVanue = originalVanue;
            DateTime = DateTime.Now;
            Type = type;
            Gig = gig;
        }
    }
}