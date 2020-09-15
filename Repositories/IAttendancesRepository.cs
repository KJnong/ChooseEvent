using ChooseEvent2.Models;
using System.Collections.Generic;

namespace ChooseEvent2.Repositories
{
    public interface IAttendancesRepository
    {
        IEnumerable<Attendance> GetAttendance();

        void AddAttendance(Attendance attendance);
        void RemoveAttendance(Attendance attendance);
    }
}