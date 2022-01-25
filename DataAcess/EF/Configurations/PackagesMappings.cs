using BlazorApp.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorApp.DataAcess.EF.Configurations
{
    public class PackagesMappings : IEntityTypeConfiguration<Packages>
    {
        public void Configure(EntityTypeBuilder<Packages> builder)
        {
            builder.ToTable("Packages");
        }
    }
}
