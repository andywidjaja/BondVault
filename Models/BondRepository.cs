using System;
using System.Collections.Generic;
using System.IO;

using BondVault.Helper;

namespace BondVault.Models
{
    public class BondRepository : IBondRepository
    {
        private readonly Dictionary<string, Bond> _bondVault;

        public BondRepository()
        {
            _bondVault = new Dictionary<string, Bond>();
            Initialize(_bondVault);
        }

        public void Add(Bond bond)
        {
        }

        public Bond Load(string cusip)
        {
            Bond bond;
            if (_bondVault.TryGetValue(cusip, out bond))
            {
                return bond;
            }

            return null;
        }

        public IEnumerable<Bond> LoadAll()
        {
            return _bondVault.Values;
        }

        public void Remove(string cusip)
        {
        }

        public void Update(string cusip, Bond bond)
        {
        }

        private void Initialize(Dictionary<string, Bond> bondVault)
        {
            var file = Path.Combine(System.Environment.CurrentDirectory, "Data", "CusipTrainingData.csv");

            using (var csvReader = new CsvReader(file, true))
            {
                foreach (var row in csvReader.RowEnumerator)
                {
                    var data = row as string[];
                    
                    var bond = new Bond
                    {
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

                    bondVault.Add(bond.Cusip, bond);
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
    }
}