using ChooseEvent2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChooseEvent2.Repositories
{
    public class AttendancesRepository : IAttendancesRepository
    {
        private readonly ApplicationDbContext db;
        public AttendancesRepository(ApplicationDbContext _db)
        {
            db = _db;
        }

        public IEnumerable<Attendance> GetAttendance()
        {
            return db.Attendances.ToList();
        }

        public void AddAttendance(Attendance attendance)
        {
            db.Attendances.Add(attendance);
        }

        public void RemoveAttendance(Attendance attendance)
        {
            db.Attendances.Remove(attendance);
        }
    }
}