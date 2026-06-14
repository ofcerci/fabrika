using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IAttendanceRepository : IEntityRepository<Attendance>
    {
        Task<List<Attendance>> GetMemberAttendances(int memberId);
        Task<List<Attendance>> GetAttendancesByDate(DateTime date);
    }
}
