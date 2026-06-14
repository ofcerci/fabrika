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

namespace Business.Handlers.Trainers.Commands
{
    public class CreateTrainerCommand : IRequest<IResult>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Specialization { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime HireDate { get; set; }
        public string Notes { get; set; }

        public class CreateTrainerCommandHandler : IRequestHandler<CreateTrainerCommand, IResult>
        {
            private readonly ITrainerRepository _trainerRepository;

            public CreateTrainerCommandHandler(ITrainerRepository trainerRepository)
            {
                _trainerRepository = trainerRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateTrainerCommand request, CancellationToken cancellationToken)
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
                    PhotoUrl = request.PhotoUrl,
                    HireDate = request.HireDate,
                    Notes = request.Notes,
                    Status = true
                };

                _trainerRepository.Add(trainer);
                await _trainerRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}
