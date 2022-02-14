using BlazorApp.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorApp.DataAcess.EF.Configurations
{
    public class PujaMappings : IEntityTypeConfiguration<Puja>
    {
        public void Configure(EntityTypeBuilder<Puja> builder)
        {
            builder.ToTable("Puja");
        }
    }
}
