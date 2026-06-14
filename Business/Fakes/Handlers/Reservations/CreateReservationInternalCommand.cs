using System;
using System.Threading;
using System.Threading.Tasks;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Fakes.Handlers.Reservations
{
    public class CreateReservationInternalCommand : IRequest<IResult>
    {
        public int MemberId { get; set; }
        public int LessonId { get; set; }

        public class CreateReservationInternalCommandHandler : IRequestHandler<CreateReservationInternalCommand, IResult>
        {
            private readonly IReservationRepository _reservationRepository;

            public CreateReservationInternalCommandHandler(IReservationRepository reservationRepository)
            {
                _reservationRepository = reservationRepository;
            }

            [CacheRemoveAspect()]
            public async Task<IResult> Handle(CreateReservationInternalCommand request, CancellationToken cancellationToken)
            {
                var reservation = new Reservation
                {
                    MemberId = request.MemberId,
                    LessonId = request.LessonId,
                    ReservedAt = DateTime.Now,
                    Status = 1
                };

                _reservationRepository.Add(reservation);
                await _reservationRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}
