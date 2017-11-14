using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PeopleSearch.Data
{
    public partial class PeopleSearchContext : DbContext
    {
        public PeopleSearchContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Birthday)
                    .HasColumnName("birthday")
                    .HasColumnType("datetime");

                entity.Property(e => e.Bloodtype)
                    .HasColumnName("bloodtype")
                    .HasMaxLength(3);

                entity.Property(e => e.BrowserUserAgent)
                    .HasColumnName("browseruseragent")
                    .HasMaxLength(255);

                entity.Property(e => e.CcExpires)
                    .HasColumnName("ccexpires")
                    .HasMaxLength(10);

                entity.Property(e => e.CcNumber)
                    .HasColumnName("ccnumber")
                    .HasMaxLength(16);

                entity.Property(e => e.CcType)
                    .HasColumnName("cctype")
                    .HasMaxLength(10);

                entity.Property(e => e.Centimeters).HasColumnName("centimeters");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(100);

                entity.Property(e => e.Color)
                    .HasColumnName("color")
                    .HasMaxLength(6);

                entity.Property(e => e.Company)
                    .HasColumnName("company")
                    .HasMaxLength(70);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(2);

                entity.Property(e => e.CountryFull)
                    .HasColumnName("countryfull")
                    .HasMaxLength(100);

                entity.Property(e => e.Cvv2)
                    .HasColumnName("cvv2")
                    .HasMaxLength(3);

                entity.Property(e => e.Domain)
                    .HasColumnName("domain")
                    .HasMaxLength(70);

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasColumnName("emailaddress")
                    .HasMaxLength(100);

                entity.Property(e => e.FeetInches)
                    .HasColumnName("feetinches")
                    .HasMaxLength(6);

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasMaxLength(6);

                entity.Property(e => e.GivenName)
                    .IsRequired()
                    .HasColumnName("givenname")
                    .HasMaxLength(20);

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasMaxLength(36);

                entity.Property(e => e.Kilograms)
                    .HasColumnName("kilograms")
                    .HasColumnType("decimal(5, 1)");

                entity.Property(e => e.Latitude)
                    .HasColumnName("latitude")
                    .HasColumnType("numeric(10, 6)");

                entity.Property(e => e.Longitude)
                    .HasColumnName("longitude")
                    .HasColumnType("numeric(10, 6)");

                entity.Property(e => e.MaidenName)
                    .HasColumnName("maidenname")
                    .HasMaxLength(23);

                entity.Property(e => e.MiddleInitial)
                    .HasColumnName("middleinitial")
                    .HasMaxLength(1);

                entity.Property(e => e.MoneygramMtcn)
                    .HasColumnName("moneygrammtcn")
                    .HasColumnType("nchar(8)");

                entity.Property(e => e.Nameset)
                    .HasColumnName("nameset")
                    .HasMaxLength(25);

                entity.Property(e => e.NationalId)
                    .HasColumnName("nationalid")
                    .HasMaxLength(20);

                entity.Property(e => e.Occupation)
                    .HasColumnName("occupation")
                    .HasMaxLength(70);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(25);

                entity.Property(e => e.Pounds)
                    .HasColumnName("pounds")
                    .HasColumnType("decimal(5, 1)");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasMaxLength(22);

                entity.Property(e => e.StateFull)
                    .HasColumnName("statefull")
                    .HasMaxLength(100);

                entity.Property(e => e.StreetAddress)
                    .HasColumnName("streetaddress")
                    .HasMaxLength(100);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasMaxLength(23);

                entity.Property(e => e.TelephoneCountryCode).HasColumnName("telephonecountrycode");

                entity.Property(e => e.TelephoneNumber)
                    .HasColumnName("telephonenumber")
                    .HasMaxLength(25);

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(6);

                entity.Property(e => e.TropicalZodiac)
                    .HasColumnName("tropicalzodiac")
                    .HasMaxLength(11);

                entity.Property(e => e.UpsTracking)
                    .HasColumnName("upstracking")
                    .HasMaxLength(24);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Vehicle)
                    .HasColumnName("vehicle")
                    .HasMaxLength(255);

                entity.Property(e => e.WesternUnionMtcn)
                    .HasColumnName("westernunionmtcn")
                    .HasColumnType("nchar(10)");

                entity.Property(e => e.ZipCode)
                    .HasColumnName("zipcode")
                    .HasMaxLength(15);
            });
        }
    }
}
