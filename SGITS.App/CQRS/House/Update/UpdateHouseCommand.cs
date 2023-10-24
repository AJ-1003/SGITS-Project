using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SGITS.App.Exceptions;
using SGITS.App.Interfaces;

namespace SGITS.App.CQRS.House.Update;
public class UpdateHouseCommand : BaseCommand, IRequest<BaseCommandResponse>
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    public UpdateHouseCommand(string name, string address)
    {
        Name = name;
        Address = address;
    }

    internal class UpdateHouseCommandHandler : IRequestHandler<UpdateHouseCommand, BaseCommandResponse>
    {
        private readonly IHouseRepository _houseRepository;

        public UpdateHouseCommandHandler(IHouseRepository houseRepository)
        {
            _houseRepository = houseRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateHouseCommand command, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateHouseCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var house = await _houseRepository.GetByIdAsync(command.Id, cancellationToken);

            if (house is null)
            {
                throw new NotFoundException(nameof(Core.Entities.House), command.Id);
            }

            house.Update(
                command.Name,
                command.Address);

            await _houseRepository.UpdateAsync(house, cancellationToken);

            response.Id = house.Id;
            response.Success = true;
            response.Message = "House updated successfully.";

            return response;
        }
    }
}
