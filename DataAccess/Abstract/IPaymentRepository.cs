using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IPaymentRepository : IEntityRepository<Payment>
    {
        Task<List<Payment>> GetMemberPayments(int memberId);
        Task<List<Payment>> GetPaymentsByDateRange(DateTime start, DateTime end);
    }
}
