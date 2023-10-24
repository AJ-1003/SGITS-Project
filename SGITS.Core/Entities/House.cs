using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGITS.Core.Constants;

namespace SGITS.Core.Entities;
public class House
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public bool HasOwner { get; set; } = false;

    public static House Create(
        string name,
        string address)
    {
        var house = new House
        {
            Name = name,
            Address = address,
        };

        return house;
    }

    public void Update(
        string name,
        string address)
    {
        Name = name;
        Address = address;
    }

    public void UpdateOwner()
    {
        HasOwner = true;
    }
}
