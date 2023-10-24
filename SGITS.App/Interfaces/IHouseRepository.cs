using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGITS.App.CQRS.House.Get;
using SGITS.Core.Entities;

namespace SGITS.App.Interfaces;
public interface IHouseRepository : IGenericRepository<House>
{
    Task<HouseReadModel?> GetByIdQueryAsync(Guid houseId, CancellationToken cancellationToken);
    Task<IReadOnlyList<HouseReadModel>?> GetAllAsync(CancellationToken cancellationToken);
    Task<HouseholdReportReadModel> GetHouseholdReportAsync(Guid ownerId, CancellationToken cancellationToken);
}
