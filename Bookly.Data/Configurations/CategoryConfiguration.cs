using Bookly.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookly.Data.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Image).HasMaxLength(50);
            builder.HasData(
                new Category
                {
                    Id = 1,
                    Name = "Şiir",
                    IsActive = true,
                    IsTopMenu = true,
                    ParentId = 0,
                    OrderNo = 1,
                },
                new Category
                {
                    Id = 2,
                    Name = "Bilim Kurgu",
                    IsActive = true,
                    IsTopMenu = true,
                    ParentId = 0,
                    OrderNo = 2,
                },
                new Category
                {
                    Id = 3,
                    Name = "Roman",
                    IsActive = true,
                    IsTopMenu = true,
                    ParentId = 0,
                    OrderNo = 3,
                },
                new Category
                {
                    Id = 4,
                    Name = "Felsefe",
                    IsActive = true,
                    IsTopMenu = true,
                    ParentId = 0,
                    OrderNo = 4,
                }
            );
        }
    }
}
