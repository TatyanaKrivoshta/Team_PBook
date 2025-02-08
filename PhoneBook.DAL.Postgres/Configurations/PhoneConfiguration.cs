using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneBook.Model;

namespace PhoneBook.DAL.Configurations;

public class PhoneConfiguration : IEntityTypeConfiguration<Phone>
{
    public void Configure(EntityTypeBuilder<Phone> builder)
    {
        builder
            .HasKey(a => a.Id);

        builder
            .HasOne(a => a.Person)
            .WithMany(p => p.Phones)
            .HasForeignKey(p =>p.PersonId);

    }
}

