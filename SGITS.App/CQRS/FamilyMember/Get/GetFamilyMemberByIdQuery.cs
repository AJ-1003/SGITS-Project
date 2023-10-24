using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using SGITS.App.CQRS.FamilyMember.Update;
using SGITS.App.Exceptions;
using SGITS.App.Interfaces;
using ValidationException = SGITS.App.Exceptions.ValidationException;

namespace SGITS.App.CQRS.FamilyMember.Get;
public class GetFamilyMemberByIdQuery : BaseCommand, IRequest<FamilyMemberReadModel>
{
    public Guid Id { get; set; }

    public GetFamilyMemberByIdQuery(Guid id)
    {
        Id = id;
    }

    internal class GetFamilyMemberByIdQueryHandler : IRequestHandler<GetFamilyMemberByIdQuery, FamilyMemberReadModel>
    {
        private readonly IFamilyMemberRepository _familyMemberRepository;

        public GetFamilyMemberByIdQueryHandler(IFamilyMemberRepository familyMemberRepository)
        {
            _familyMemberRepository = familyMemberRepository;
        }

        public async Task<FamilyMemberReadModel> Handle(GetFamilyMemberByIdQuery query, CancellationToken cancellationToken)
        {
            var validator = new GetFamilyMemberByIdQueryValidator();
            var validationResult = await validator.ValidateAsync(query, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var familyMember = await _familyMemberRepository.GetByIdQueryAsync(query.Id, cancellationToken);

            if (familyMember is null)
            {
                throw new NotFoundException(nameof(Core.Entities.FamilyMember), query.Id);
            }

            return familyMember;
        }
    }
}
