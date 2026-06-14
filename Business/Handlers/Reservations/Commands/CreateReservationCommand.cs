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

namespace Business.Handlers.Reservations.Commands
{
    public class CreateReservationCommand : IRequest<IResult>
    {
        public int MemberId { get; set; }
        public int LessonId { get; set; }

        public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, IResult>
        {
            private readonly IReservationRepository _reservationRepository;
            private readonly ILessonRepository _lessonRepository;

            public CreateReservationCommandHandler(IReservationRepository reservationRepository, ILessonRepository lessonRepository)
            {
                _reservationRepository = reservationRepository;
                _lessonRepository = lessonRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
            {
                var lesson = await _lessonRepository.GetAsync(l => l.Id == request.LessonId);
                if (lesson == null)
                    return new ErrorResult(Messages.Unknown);

                var reservationCount = await _reservationRepository.GetLessonReservationCount(request.LessonId);
                if (reservationCount >= lesson.Capacity)
                    return new ErrorResult("LessonFull");

                var existing = await _reservationRepository.GetAsync(r => r.MemberId == request.MemberId && r.LessonId == request.LessonId && r.Status != 2);
                if (existing != null)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var reservation = new Reservation
                {
                    MemberId = request.MemberId,
                    LessonId = request.LessonId,
                    ReservedAt = DateTime.Now,
                    Status = 1 // Confirmed
                };

                _reservationRepository.Add(reservation);
                await _reservationRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}
