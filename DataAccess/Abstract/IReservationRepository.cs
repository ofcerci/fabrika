using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IReservationRepository : IEntityRepository<Reservation>
    {
        Task<List<Reservation>> GetMemberReservations(int memberId);
        Task<int> GetLessonReservationCount(int lessonId);
    }
}
