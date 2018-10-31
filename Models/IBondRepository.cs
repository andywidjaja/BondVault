using System;
using System.Collections.Generic;

namespace BondVault.Models
{
    public interface IBondRepository
    {
        void Add(Bond bond);

        Bond Load(string cusip);

        IEnumerable<Bond> LoadAll();

        void Remove(string cusip);

        void Update(string cusip, Bond bond);
    }
}