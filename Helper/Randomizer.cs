using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using BondVault.Models;

namespace BondVault.Helper
{
    public static class Randomizer
    {
        public static IList<Bond> Shuffle(this IEnumerable<Bond> list)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            var randomised = list.Select(item => new { item, order = rnd.Next() })
                .OrderBy(x => x.order)
                .Select(x => x.item)
                .ToList();
        
            return randomised;
        }
    }
}