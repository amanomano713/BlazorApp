using BlazorApp.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorApp.DataAcess.EF.Configurations
{
    public class MovPackageMappings : IEntityTypeConfiguration<MovPackage>
    {
        public void Configure(EntityTypeBuilder<MovPackage> builder)
        {
            builder.ToTable("MovPackage");
        }
    }
}
