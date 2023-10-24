using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGITS.Core.Entities;
public class HouseholdReport
{
    public string HouseAddress { get; set; }
    public string OwnerName { get; set; }
    public string ContactNumber { get; set; }
    public int NumberOfDependents { get; set; }
}
