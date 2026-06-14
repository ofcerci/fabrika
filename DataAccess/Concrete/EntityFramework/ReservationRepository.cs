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
    public class ReservationRepository : EfEntityRepositoryBase<Reservation, ProjectDbContext>, IReservationRepository
    {
        public ReservationRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<List<Reservation>> GetMemberReservations(int memberId)
        {
            return await Context.Reservations
                .Where(r => r.MemberId == memberId)
                .ToListAsync();
        }

        public async Task<int> GetLessonReservationCount(int lessonId)
        {
            return await Context.Reservations
                .CountAsync(r => r.LessonId == lessonId && r.Status != 2); // 2 = Cancelled
        }
    }
}
