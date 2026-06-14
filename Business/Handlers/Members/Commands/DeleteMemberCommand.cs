using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Members.Commands
{
    public class DeleteMemberCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, IResult>
        {
            private readonly IMemberRepository _memberRepository;

            public DeleteMemberCommandHandler(IMemberRepository memberRepository)
            {
                _memberRepository = memberRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
            {
                var member = await _memberRepository.GetAsync(m => m.Id == request.Id);
                if (member == null)
                    return new ErrorResult(Messages.Unknown);

                _memberRepository.Delete(member);
                await _memberRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
