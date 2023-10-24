using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SGITS.App.Exceptions;
using SGITS.App.Interfaces;

namespace SGITS.App.CQRS.House.Create;
public class CreateHouseCommand : IRequest<BaseCommandResponse>
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    public CreateHouseCommand(string name, string address)
    {
        Name = name;
        Address = address;
    }

    internal class CreateHouseCommandHandler : IRequestHandler<CreateHouseCommand, BaseCommandResponse>
    {
        private readonly IHouseRepository _houseRepository;

        public CreateHouseCommandHandler(IHouseRepository houseRepository)
        {
            _houseRepository = houseRepository;
        }

        public async Task<BaseCommandResponse> Handle(CreateHouseCommand command, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateHouseCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var house = Core.Entities.House.Create(
                command.Name,
                command.Address);

            var createdHouse = await _houseRepository.CreateAsync(house, cancellationToken);

            if (createdHouse is null)
            {
                response.Success = false;
                response.Message = "House could not be created.";

                return response;
            }

            response.Id = createdHouse.Id;
            response.Success = true;
            response.Message = "House created successfully.";

            return response;
        }
    }
}
