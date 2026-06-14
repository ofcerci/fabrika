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

namespace Business.Handlers.Attendances.Queries
{
    public class GetAttendancesQuery : IRequest<IDataResult<IEnumerable<Attendance>>>
    {
        public class GetAttendancesQueryHandler : IRequestHandler<GetAttendancesQuery, IDataResult<IEnumerable<Attendance>>>
        {
            private readonly IAttendanceRepository _attendanceRepository;

            public GetAttendancesQueryHandler(IAttendanceRepository attendanceRepository)
            {
                _attendanceRepository = attendanceRepository;
            }

            [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<Attendance>>> Handle(GetAttendancesQuery request, CancellationToken cancellationToken)
            {
                var list = await _attendanceRepository.GetListAsync();
                return new SuccessDataResult<IEnumerable<Attendance>>(list);
            }
        }
    }
}
