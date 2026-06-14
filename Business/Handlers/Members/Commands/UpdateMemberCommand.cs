using System;
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
    public class UpdateMemberCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }
        public string PhotoUrl { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public bool Status { get; set; }
        public string Notes { get; set; }

        public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, IResult>
        {
            private readonly IMemberRepository _memberRepository;

            public UpdateMemberCommandHandler(IMemberRepository memberRepository)
            {
                _memberRepository = memberRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
            {
                var member = await _memberRepository.GetAsync(m => m.Id == request.Id);
                if (member == null)
                    return new ErrorResult(Messages.Unknown);

                member.FirstName = request.FirstName;
                member.LastName = request.LastName;
                member.Email = request.Email;
                member.Phone = request.Phone;
                member.BirthDate = request.BirthDate;
                member.Gender = request.Gender;
                member.Address = request.Address;
                member.PhotoUrl = request.PhotoUrl;
                member.EmergencyContactName = request.EmergencyContactName;
                member.EmergencyContactPhone = request.EmergencyContactPhone;
                member.Status = request.Status;
                member.Notes = request.Notes;

                _memberRepository.Update(member);
                await _memberRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
