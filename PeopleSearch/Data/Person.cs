using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeopleSearch.Data
{
    public partial class Person
    {
        public int Id { get; set; }
        [NotMapped]
        public string AvatarUrl { get; set; }
        public string Gender { get; set; }
        public string Nameset { get; set; }
        public string Title { get; set; }
        public string GivenName { get; set; }
        public string MiddleInitial { get; set; }
        public string Surname { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string StateFull { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string CountryFull { get; set; }
        public string EmailAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string BrowserUserAgent { get; set; }
        public string TelephoneNumber { get; set; }
        public int? TelephoneCountryCode { get; set; }
        public string MaidenName { get; set; }
        public DateTime? Birthday { get; set; }
        public int? Age { get; set; }
        public string TropicalZodiac { get; set; }
        public string CcType { get; set; }
        public string CcNumber { get; set; }
        public string Cvv2 { get; set; }
        public string CcExpires { get; set; }
        public string NationalId { get; set; }
        public string UpsTracking { get; set; }
        public string WesternUnionMtcn { get; set; }
        public string MoneygramMtcn { get; set; }
        public string Color { get; set; }
        public string Occupation { get; set; }
        public string Company { get; set; }
        public string Vehicle { get; set; }
        public string Domain { get; set; }
        public string Bloodtype { get; set; }
        public decimal? Pounds { get; set; }
        public decimal? Kilograms { get; set; }
        public string FeetInches { get; set; }
        public short? Centimeters { get; set; }
        public string Guid { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}
