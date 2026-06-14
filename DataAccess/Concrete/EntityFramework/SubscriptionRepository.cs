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
    public class SubscriptionRepository : EfEntityRepositoryBase<Subscription, ProjectDbContext>, ISubscriptionRepository
    {
        public SubscriptionRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<Subscription> GetActiveMemberSubscription(int memberId)
        {
            return await Context.Subscriptions
                .Where(s => s.MemberId == memberId && s.Status == 1 && s.EndDate >= DateTime.Today)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Subscription>> GetExpiringSubscriptions(int daysAhead)
        {
            var targetDate = DateTime.Today.AddDays(daysAhead);
            return await Context.Subscriptions
                .Where(s => s.Status == 1 && s.EndDate.Date == targetDate.Date)
                .ToListAsync();
        }
    }
}
