using BlazorApp.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorApp.DataAcess.EF.Configurations
{
    public class UserDataMappings : IEntityTypeConfiguration<UserData>
    {
        public void Configure(EntityTypeBuilder<UserData> builder)
        {
            builder.ToTable("UserData");
            builder.HasKey(x => x.Id);
        }
    }
}
