using HotelApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelApp.DAL.EF.Configuration
{
    class ActiveOrderConfiguration: IEntityTypeConfiguration<ActiveOrder>
    {
        public void Configure(EntityTypeBuilder<ActiveOrder> builder)
        {
            builder.Property(p => p.PaymentState).HasConversion<string>();
        }
    }
}
