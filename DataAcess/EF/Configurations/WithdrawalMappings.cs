
using BlazorApp.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorApp.DataAcess.EF.Configurations
{
    public class WithdrawalMappings : IEntityTypeConfiguration<Withdrawal>
    {
        public void Configure(EntityTypeBuilder<Withdrawal> builder)
        {
            builder.ToTable("Withdrawal");
        }
    }
}
