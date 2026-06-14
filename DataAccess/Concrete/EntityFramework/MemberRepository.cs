using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class MemberRepository : EfEntityRepositoryBase<Member, ProjectDbContext>, IMemberRepository
    {
        public MemberRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
