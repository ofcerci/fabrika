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
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.Members.Commands
{
    public class CreateMemberCommand : IRequest<IResult>
    {
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
        public string Notes { get; set; }

        public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, IResult>
        {
            private readonly IMemberRepository _memberRepository;

            public CreateMemberCommandHandler(IMemberRepository memberRepository)
            {
                _memberRepository = memberRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
            {
                var existing = await _memberRepository.GetAsync(m => m.Email == request.Email);
                if (existing != null)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var member = new Member
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Phone = request.Phone,
                    BirthDate = request.BirthDate,
                    Gender = request.Gender,
                    Address = request.Address,
                    PhotoUrl = request.PhotoUrl,
                    EmergencyContactName = request.EmergencyContactName,
                    EmergencyContactPhone = request.EmergencyContactPhone,
                    Notes = request.Notes,
                    RegisterDate = DateTime.Now,
                    Status = true
                };

                _memberRepository.Add(member);
                await _memberRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}
