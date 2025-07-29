using Bookly.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookly.Data.Configurations
{
    internal class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasColumnType("varchar(50)").HasMaxLength(50);
            builder.Property(x => x.Surname).IsRequired().HasColumnType("varchar(50)").HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired().HasColumnType("varchar(50)").HasMaxLength(50);
            builder.Property(x => x.Phone).HasColumnType("varchar(15)").HasMaxLength(15);
            builder.Property(x => x.Password).IsRequired().HasColumnType("nvarchar(50)").HasMaxLength(50);
            builder.Property(x => x.UserName).HasColumnType("varchar(50)").HasMaxLength(50);

            builder.HasData(
                new AppUser
                {
                    Id = 1,
                    UserName = "Admin",
                    Email = "admin@etcaret.io",
                    IsActive = true,
                    IsAdmin = true,
                    Name = "Test",
                    Password = "123456*",
                    Surname = "User",
                    CreateDate = new DateTime(2024, 1, 1),
                    UserGuid = new Guid("11111111-1111-1111-1111-111111111111")
                }
            );
        }
    }
}
