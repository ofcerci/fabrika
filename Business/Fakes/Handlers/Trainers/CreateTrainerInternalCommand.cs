using System;
using System.Threading;
using System.Threading.Tasks;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Fakes.Handlers.Trainers
{
    public class CreateTrainerInternalCommand : IRequest<IResult>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Specialization { get; set; }
        public string Notes { get; set; }

        public class CreateTrainerInternalCommandHandler : IRequestHandler<CreateTrainerInternalCommand, IResult>
        {
            private readonly ITrainerRepository _trainerRepository;

            public CreateTrainerInternalCommandHandler(ITrainerRepository trainerRepository)
            {
                _trainerRepository = trainerRepository;
            }

            [CacheRemoveAspect()]
            public async Task<IResult> Handle(CreateTrainerInternalCommand request, CancellationToken cancellationToken)
            {
                var existing = await _trainerRepository.GetAsync(t => t.Email == request.Email);
                if (existing != null)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var trainer = new Trainer
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Phone = request.Phone,
                    Specialization = request.Specialization,
                    Notes = request.Notes,
                    HireDate = DateTime.Now,
                    Status = true
                };

                _trainerRepository.Add(trainer);
                await _trainerRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}
