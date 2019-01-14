using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using BondVault.Helper;

namespace BondVault.Models
{
    public class BondRepository : IBondRepository
    {
        private static int _maxBondId = 0;

        private int _currentMax = 10;

        private readonly Dictionary<int, Bond> _bondVault;

        public BondRepository()
        {
            _bondVault = new Dictionary<int, Bond>();
            Initialize(_bondVault);
        }

        public Bond Add(Bond bond)
        {
            var matchingCusip = _bondVault.Values.FirstOrDefault(b => b.Cusip == bond.Cusip);

            if (matchingCusip != null)
            {
                return null;
            }

            _currentMax++;
            bond.Id = ++_maxBondId;
            System.Console.WriteLine($"Added a new bond: {bond.Id}");
            if (_bondVault.TryAdd(bond.Id, bond))
            {
                return bond;
            }

            return null;
        }

        public Bond Load(int id)
        {
            Bond bond;
            if (_bondVault.TryGetValue(id, out bond))
            {
                return bond;
            }

            return null;
        }

        public IEnumerable<Bond> LoadAll()
        {
            return _bondVault.Values;
        }

        public void Remove(int id)
        {
        }

        public Bond Update(int id, Bond bond)
        {
            var matchingBond = Load(id);

            if (matchingBond != null)
            {
                matchingBond.MarketSectorDescription = bond.MarketSectorDescription;
            
                return matchingBond;
            }

            return null;
        }

        private void Initialize(Dictionary<int, Bond> bondVault)
        {
            var file = Path.Combine(System.Environment.CurrentDirectory, "Data", "CusipTrainingData.csv");

            Random rnd = new Random(DateTime.Now.Millisecond);

            using (var csvReader = new CsvReader(file, true))
            {
                foreach (var row in csvReader.RowEnumerator)
                {
                    var data = row as string[];
                    
                    var bond = new Bond
                    {
                        Id = ++_maxBondId,
                        Cusip = data[0],
                        AssetType = data[1],
                        IssuerIndustry = data[2],
                        MortgageType = data[4],
                        MortgagePrepayType = data[5],
                        SecurityType = data[6],
                        SecurityType2 = data[7],
                        CouponType = data[8],
                        MarketSectorDescription = data[9],
                        MortgageCollateralType = data[10],
                        TaxCode = data[11],
                        BankQualified = data[12],
                        CapitalPurpose = data[14]
                    };

                    if (!string.IsNullOrEmpty(data[3]))
                    {
                        bond.MortgageAmortizationTypeLevel = Convert.ToDouble(data[3]);
                    }

                    if (!string.IsNullOrEmpty(data[13]))
                    {
                        bond.DatedDate = Convert.ToDateTime(data[13]);
                    }

                    if (bondVault.Count() > 200)
                    {
                        break;
                    }

                    var random = rnd.Next(0, 100);
                    //Console.WriteLine(random);
                    if (random == 99)
                    {
                        bondVault.Add(bond.Id, bond);
                    }
                }
            }

            // using (var streamReader = File.OpenText(file))
            // {
            //     while (!streamReader.EndOfStream)
            //     {
            //         var line = streamReader.ReadLine();

            //         // If header is not read yet, skip the first line
            //         if (!headerRead)
            //         {
            //             headerRead = true;
            //             continue;
            //         }

            //         var data = line.Split(new[] {','});
            //         var bond = new Bond
            //         {
            //             Cusip = data[0],
            //             AssetType = data[1],
            //             IssuerIndustry = data[2],
            //             MortgageType = data[4],
            //             MortgagePrepayType = data[5],
            //             SecurityType = data[6],
            //             SecurityType2 = data[7],
            //             CouponType = data[8],
            //             MarketSectorDescription = data[9],
            //             MortgageCollateralType = data[10],
            //             TaxCode = data[11],
            //             CapitalPurpose = data[14]
            //         };

            //         if (!string.IsNullOrEmpty(data[3]))
            //         {
            //             bond.MortgageAmortizationTypeLevel = Convert.ToDouble(data[3]);
            //         }

            //         if (!string.IsNullOrEmpty(data[12]))
            //         {
            //             var bankQualifiedFlag = data[12];
            //             bond.BankQualified = bankQualifiedFlag == "Y";
            //         }

            //         if (!string.IsNullOrEmpty(data[13]))
            //         {
            //             bond.DatedDate = Convert.ToDateTime(data[13]);
            //         }

            //         bondVault.Add(bond.Cusip, bond);
            //     }
            // }
        }

        private Dictionary<string, Bond> Shuffle(IEnumerable<KeyValuePair<string, Bond>> list)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            var randomised = list.Select(item => new { item, order = rnd.Next() })
                .OrderBy(x => x.order)
                .Select(x => x.item)
                .ToDictionary(b => b.Key, b => b.Value);
        
            return randomised;
        }
    }
}