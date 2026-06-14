using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.Members.Queries
{
    public class GetMembersQuery : IRequest<IDataResult<IEnumerable<Member>>>
    {
        public class GetMembersQueryHandler : IRequestHandler<GetMembersQuery, IDataResult<IEnumerable<Member>>>
        {
            private readonly IMemberRepository _memberRepository;

            public GetMembersQueryHandler(IMemberRepository memberRepository)
            {
                _memberRepository = memberRepository;
            }

            [SecuredOperation(Priority = 1)]
            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<Member>>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
            {
                var list = await _memberRepository.GetListAsync();
                return new SuccessDataResult<IEnumerable<Member>>(list);
            }
        }
    }
}
