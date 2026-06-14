using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.Members.Queries
{
    public class GetMemberQuery : IRequest<IDataResult<Member>>
    {
        public int Id { get; set; }

        public class GetMemberQueryHandler : IRequestHandler<GetMemberQuery, IDataResult<Member>>
        {
            private readonly IMemberRepository _memberRepository;

            public GetMemberQueryHandler(IMemberRepository memberRepository)
            {
                _memberRepository = memberRepository;
            }

            [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<Member>> Handle(GetMemberQuery request, CancellationToken cancellationToken)
            {
                var member = await _memberRepository.GetAsync(m => m.Id == request.Id);
                return new SuccessDataResult<Member>(member);
            }
        }
    }
}
