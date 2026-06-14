using System;
using System.Threading;
using System.Threading.Tasks;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Fakes.Handlers.Members
{
    public class CreateMemberInternalCommand : IRequest<IResult>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }

        public class CreateMemberInternalCommandHandler : IRequestHandler<CreateMemberInternalCommand, IResult>
        {
            private readonly IMemberRepository _memberRepository;

            public CreateMemberInternalCommandHandler(IMemberRepository memberRepository)
            {
                _memberRepository = memberRepository;
            }

            [CacheRemoveAspect()]
            public async Task<IResult> Handle(CreateMemberInternalCommand request, CancellationToken cancellationToken)
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
