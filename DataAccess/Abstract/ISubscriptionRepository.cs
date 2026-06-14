using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface ISubscriptionRepository : IEntityRepository<Subscription>
    {
        Task<Subscription> GetActiveMemberSubscription(int memberId);
        Task<List<Subscription>> GetExpiringSubscriptions(int daysAhead);
    }
}
