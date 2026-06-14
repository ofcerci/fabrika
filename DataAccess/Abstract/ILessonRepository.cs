using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface ILessonRepository : IEntityRepository<Lesson>
    {
        Task<List<Lesson>> GetLessonsByDateRange(DateTime start, DateTime end);
        Task<List<Lesson>> GetLessonsByTrainer(int trainerId);
    }
}
