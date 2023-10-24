using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SGITS.App.Exceptions;
using SGITS.App.Interfaces;

namespace SGITS.App.CQRS.House.Get;
public class GetHouseByIdQuery : BaseCommand, IRequest<HouseReadModel>
{
    public GetHouseByIdQuery(Guid id)
    {
        Id = id;
    }

    internal class GetHouseByIdQueryHandler : IRequestHandler<GetHouseByIdQuery, HouseReadModel>
    {
        private readonly IHouseRepository _houseRepository;

        public GetHouseByIdQueryHandler(IHouseRepository houseRepository)
        {
            _houseRepository = houseRepository;
        }

        public async Task<HouseReadModel> Handle(GetHouseByIdQuery query, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new GetHouseByIdQueryValidator();
            var validationResult = await validator.ValidateAsync(query, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var house = await _houseRepository.GetByIdQueryAsync(query.Id, cancellationToken);

            if (house is null)
            {
                throw new NotFoundException(nameof(Core.Entities.House), query.Id);
            }

            return house;
        }
    }
}
