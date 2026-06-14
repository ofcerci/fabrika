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
    public class PaymentRepository : EfEntityRepositoryBase<Payment, ProjectDbContext>, IPaymentRepository
    {
        public PaymentRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<List<Payment>> GetMemberPayments(int memberId)
        {
            return await Context.Payments
                .Where(p => p.MemberId == memberId)
                .OrderByDescending(p => p.PaidAt)
                .ToListAsync();
        }

        public async Task<List<Payment>> GetPaymentsByDateRange(DateTime start, DateTime end)
        {
            return await Context.Payments
                .Where(p => p.PaidAt >= start && p.PaidAt <= end)
                .ToListAsync();
        }
    }
}
