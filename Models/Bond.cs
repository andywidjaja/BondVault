using System;

namespace BondVault.Models
{
    public class Bond
    {
        public string Cusip {get; set;}

        public string AssetType {get;set;}

        public string IssuerIndustry {get;set;}

        public double? MortgageAmortizationTypeLevel {get;set;}

        public string MortgageType {get;set;}

        public string MortgagePrepayType {get;set;}

        public string SecurityType {get;set;}

        public string SecurityType2 {get;set;}

        public string CouponType {get;set;}

        public string MarketSectorDescription {get;set;}

        public string MortgageCollateralType {get;set;}

        public string TaxCode {get;set;}

        public string BankQualified {get;set;}

        public DateTime? DatedDate {get;set;}

        public string CapitalPurpose {get;set;}
    }
}