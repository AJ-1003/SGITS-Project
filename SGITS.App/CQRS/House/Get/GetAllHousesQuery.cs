using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SGITS.App.Interfaces;

namespace SGITS.App.CQRS.House.Get;
public class GetAllHousesQuery : IRequest<IReadOnlyList<HouseReadModel>>
{
    internal class GetAllHousesQueryHandler : IRequestHandler<GetAllHousesQuery, IReadOnlyList<HouseReadModel>>
    {
        private readonly IHouseRepository _houseRepository;

        public GetAllHousesQueryHandler(IHouseRepository houseRepository)
        {
            _houseRepository = houseRepository;
        }

        public async Task<IReadOnlyList<HouseReadModel>> Handle(GetAllHousesQuery query, CancellationToken cancellationToken)
        {
            return await _houseRepository.GetAllAsync(cancellationToken);
        }
    }
}
