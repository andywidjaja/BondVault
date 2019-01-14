using System;
using System.Collections.Generic;

namespace BondVault.Models
{
    public interface IBondRepository
    {
        Bond Add(Bond bond);

        Bond Load(int id);

        IEnumerable<Bond> LoadAll();

        void Remove(int id);

        Bond Update(int id, Bond bond);
    }
}