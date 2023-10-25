using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SGITS.App.CQRS.FamilyMember.Create;
using SGITS.App.Interfaces;
using SGITS.Persistence.Repositories;
using NSubstitute;
using Bogus;
using MediatR;

namespace SGITS.App.Tests.CQRS.FamilyMember.Create;

[TestFixture]
[TestOf(typeof(CreateFamilyMemberCommand))]
public class CreateFamilyMemberCommandTests
{
    private static readonly Randomizer Rng = new();

    IServiceCollection _services;
    IServiceProvider _serviceProvider;
    private IFamilyMemberRepository _familyMemberRepository;
    private IHouseRepository _houseRepository;

    [SetUp]
    public void Setup()
    {
        _services = new ServiceCollection();
        _serviceProvider = Substitute.For<IServiceProvider>();
        _familyMemberRepository = Substitute.For<IFamilyMemberRepository>();
        _houseRepository = Substitute.For<IHouseRepository>();
    }

    [Test]
    public async Task CreateFamilyMember_ShouldCallRepository_ReturnNewFamilyMember()
    {
        // Arrange
        var createdFamilyMember = new Core.Entities.FamilyMember
        {
            Id = Guid.NewGuid(),
            FirstName = "Member Name",
            LastName = "Member Lastname",
            FullName = "Member Name Member Lastname",
            ContactNumber = "011 234 5678",
            MemberAssignment = Core.Constants.MemberAssignment.Member,
            HouseId = null
        };

        var familyMember = new Core.Entities.FamilyMember()
        {
            FirstName = "Member Name",
            LastName = "Member Lastname",
            ContactNumber = "011 234 5678"
        };

        _familyMemberRepository.CreateAsync(familyMember, CancellationToken.None).Returns(createdFamilyMember);

        var command = new CreateFamilyMemberCommand
        (
            "Member Name",
            "Member Lastname",
            "011 234 5678"
        );

        // Act
        var mediator = _serviceProvider.GetRequiredService<IMediator>();

        var result = await mediator.Send(command);

        // Assertt
        Assert.NotNull(result);
    }
}
