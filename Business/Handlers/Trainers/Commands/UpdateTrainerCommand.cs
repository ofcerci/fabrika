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

namespace Business.Handlers.Trainers.Commands
{
    public class UpdateTrainerCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Specialization { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime HireDate { get; set; }
        public bool Status { get; set; }
        public string Notes { get; set; }

        public class UpdateTrainerCommandHandler : IRequestHandler<UpdateTrainerCommand, IResult>
        {
            private readonly ITrainerRepository _trainerRepository;

            public UpdateTrainerCommandHandler(ITrainerRepository trainerRepository)
            {
                _trainerRepository = trainerRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateTrainerCommand request, CancellationToken cancellationToken)
            {
                var trainer = await _trainerRepository.GetAsync(t => t.Id == request.Id);
                if (trainer == null)
                    return new ErrorResult(Messages.Unknown);

                trainer.FirstName = request.FirstName;
                trainer.LastName = request.LastName;
                trainer.Email = request.Email;
                trainer.Phone = request.Phone;
                trainer.Specialization = request.Specialization;
                trainer.PhotoUrl = request.PhotoUrl;
                trainer.HireDate = request.HireDate;
                trainer.Status = request.Status;
                trainer.Notes = request.Notes;

                _trainerRepository.Update(trainer);
                await _trainerRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
