using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SGITS.App.Interfaces;

namespace SGITS.App.CQRS.House.Get;
public class GetHouseholdReportQuery : IRequest<HouseholdReportReadModel>
{
    public Guid OwnerId { get; set; }

    public GetHouseholdReportQuery(Guid ownerId)
    {
        OwnerId = ownerId;
    }

    internal class GetHouseholdReportQueryHandler : IRequestHandler<GetHouseholdReportQuery, HouseholdReportReadModel>
    {
        private readonly IHouseRepository _houseRepository;

        public GetHouseholdReportQueryHandler(IHouseRepository houseRepository)
        {
            _houseRepository = houseRepository;
        }

        public async Task<HouseholdReportReadModel> Handle(GetHouseholdReportQuery query, CancellationToken cancellationToken)
        {
            return await _houseRepository.GetHouseholdReportAsync(query.OwnerId, cancellationToken);
        }
    }
}
