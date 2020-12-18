using HotelApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelApp.DAL.EF.Configuration
{
    class HotelConfiguration: IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
                new Hotel[]
                {
                    new Hotel {HotelId = 1, Name = "Four Seasons", Address = "7890 Hettie Islands Apt. 306"},
                    new Hotel {HotelId = 2, Name = "The Pig at Combe", Address = "23387 Tanya Corners Apt. 143"},
                    new Hotel {HotelId = 3, Name = "Via", Address = "59469 Okuneva Station Apt. 543"}
                });
        }
    }
}
