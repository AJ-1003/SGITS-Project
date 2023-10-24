using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SGITS.App.Exceptions;
using SGITS.App.Interfaces;

namespace SGITS.App.CQRS.House.Delete;
public class DeleteHouseCommand : BaseCommand, IRequest
{
    internal class DeleteHouseCommandHandler : IRequestHandler<DeleteHouseCommand>
    {
        private readonly IHouseRepository _houseRepository;

        public DeleteHouseCommandHandler(IHouseRepository houseRepository)
        {
            _houseRepository = houseRepository;
        }

        public async Task Handle(DeleteHouseCommand command, CancellationToken cancellationToken)
        {
            var validator = new DeleteHouseCommandValidator();
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
            
            await _houseRepository.DeleteAsync(house, cancellationToken);
        }
    }
}
