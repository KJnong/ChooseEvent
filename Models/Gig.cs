using ChooseEvent2.ViewModels;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChooseEvent2.Models
{
    public class Gig
    {

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

        public int Id { get; set; }

        public bool IsCancelled { get; set; }

        public ApplicationUser Artist { get; set; }

        [Required]
        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        
        public Genre Genre { get; set; }

        [Required]
        public byte Genreid { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }

        public void Cancel()
        {
            IsCancelled = true;

            var notification = new Notification(NotificationType.GigCancelled, this);

            foreach (var atendee in Attendances.Select(a => a.Attendee))
            {
                atendee.Notify(notification);
            }
        }

        public void Update(GigsViewModel updateModel, DateTime originalDateTime, string originalVenue )
        {
            DateTime = updateModel.DateTime;
            Genreid = updateModel.Genre;
            Venue = updateModel.Venue;
           
            var notification = new Notification(NotificationType.GigUpdated, this, originalDateTime, originalVenue);

            foreach (var atendee in Attendances.Select(a => a.Attendee))
            {
                atendee.Notify(notification);
            }
        }
    }
}