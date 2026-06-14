using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.Reservations.Queries
{
    public class GetReservationsQuery : IRequest<IDataResult<IEnumerable<Reservation>>>
    {
        public class GetReservationsQueryHandler : IRequestHandler<GetReservationsQuery, IDataResult<IEnumerable<Reservation>>>
        {
            private readonly IReservationRepository _reservationRepository;

            public GetReservationsQueryHandler(IReservationRepository reservationRepository)
            {
                _reservationRepository = reservationRepository;
            }

            [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<Reservation>>> Handle(GetReservationsQuery request, CancellationToken cancellationToken)
            {
                var list = await _reservationRepository.GetListAsync();
                return new SuccessDataResult<IEnumerable<Reservation>>(list);
            }
        }
    }
}
