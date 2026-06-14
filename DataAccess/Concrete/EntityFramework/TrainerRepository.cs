using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class TrainerRepository : EfEntityRepositoryBase<Trainer, ProjectDbContext>, ITrainerRepository
    {
        public TrainerRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
