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
    public class LessonRepository : EfEntityRepositoryBase<Lesson, ProjectDbContext>, ILessonRepository
    {
        public LessonRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<List<Lesson>> GetLessonsByDateRange(DateTime start, DateTime end)
        {
            return await Context.Lessons
                .Where(l => l.StartTime >= start && l.StartTime <= end && l.IsActive)
                .ToListAsync();
        }

        public async Task<List<Lesson>> GetLessonsByTrainer(int trainerId)
        {
            return await Context.Lessons
                .Where(l => l.TrainerId == trainerId && l.IsActive)
                .ToListAsync();
        }
    }
}
