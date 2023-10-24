using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGITS.App.CQRS.House.Get;
public class HouseholdReportReadModel
{
    public string HouseAddress { get; init; }
    public string OwnerName { get; init; }
    public string ContactNumber { get; init; }
    public int NumberOfDependents { get; init; }
}
