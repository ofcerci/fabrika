using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class AttendanceRepository : EfEntityRepositoryBase<Attendance, ProjectDbContext>, IAttendanceRepository
    {
        public AttendanceRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<List<Attendance>> GetMemberAttendances(int memberId)
        {
            return await Context.Attendances
                .Where(a => a.MemberId == memberId)
                .OrderByDescending(a => a.CheckIn)
                .ToListAsync();
        }

        public async Task<List<Attendance>> GetAttendancesByDate(DateTime date)
        {
            return await Context.Attendances
                .Where(a => a.CheckIn.Date == date.Date)
                .ToListAsync();
        }
    }
}
