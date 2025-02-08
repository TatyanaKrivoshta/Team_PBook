using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneBook.Model;

namespace PhoneBook.DAL.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder
            .HasKey(p => p.Id);
        
        //builder.Property<>() 
        builder
            .HasMany(p => p.Phones)
            .WithOne(p => p.Person);
    }
}