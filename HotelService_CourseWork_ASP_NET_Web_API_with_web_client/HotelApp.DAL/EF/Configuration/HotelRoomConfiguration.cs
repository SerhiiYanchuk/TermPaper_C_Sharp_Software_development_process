using HotelApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelApp.DAL.EF.Configuration
{
    class HotelRoomConfiguration: IEntityTypeConfiguration<HotelRoom>
    {
        public void Configure(EntityTypeBuilder<HotelRoom> builder)
        {
            builder.HasMany(t => t.Clients).WithMany(t => t.HotelRooms)
                .UsingEntity<ActiveOrder>(
                    t => t.HasOne(p => p.Client).WithMany(p => p.ActiveOrders).HasForeignKey(p => p.ClientId),
                    t => t.HasOne(p => p.HotelRoom).WithMany(p => p.ActiveOrders).HasForeignKey(p => p.HotelRoomId),
                    t =>
                    {
                        t.Property(p => p.DateRegistration).HasDefaultValueSql("CURRENT_TIMESTAMP");                        
                    });
            //builder.HasAlternateKey(p => new { p.HotelId, p.Number});

            builder.HasData(
                new HotelRoom[]
                {
                    new HotelRoom {HotelId = 1, HotelRoomId = 1, Number = "10", PricePerDay = 100, TypeComfortId = 1, TypeSizeId = 1},
                    new HotelRoom {HotelId = 1, HotelRoomId = 2, Number = "11", PricePerDay = 100, TypeComfortId = 1, TypeSizeId = 1},
                    new HotelRoom {HotelId = 1, HotelRoomId = 3, Number = "12", PricePerDay = 200, TypeComfortId = 2, TypeSizeId = 1},
                    new HotelRoom {HotelId = 1, HotelRoomId = 4, Number = "13", PricePerDay = 200, TypeComfortId = 2, TypeSizeId = 2},
                    new HotelRoom {HotelId = 1, HotelRoomId = 5, Number = "20", PricePerDay = 250, TypeComfortId = 2, TypeSizeId = 3},
                    new HotelRoom {HotelId = 1, HotelRoomId = 6, Number = "21", PricePerDay = 250, TypeComfortId = 2, TypeSizeId = 5},
                    new HotelRoom {HotelId = 1, HotelRoomId = 7, Number = "22", PricePerDay = 300, TypeComfortId = 3, TypeSizeId = 1},
                    new HotelRoom {HotelId = 1, HotelRoomId = 8, Number = "30", PricePerDay = 400, TypeComfortId = 3, TypeSizeId = 2},
                    new HotelRoom {HotelId = 1, HotelRoomId = 9, Number = "31", PricePerDay = 400, TypeComfortId = 4, TypeSizeId = 3},
                    new HotelRoom {HotelId = 1, HotelRoomId = 10, Number = "40", PricePerDay = 600, TypeComfortId = 5, TypeSizeId = 6},
                    new HotelRoom {HotelId = 1, HotelRoomId = 11, Number = "50", PricePerDay = 800, TypeComfortId = 6, TypeSizeId = 2},

                    new HotelRoom {HotelId = 2, HotelRoomId = 12, Number = "10", PricePerDay = 100, TypeComfortId = 1, TypeSizeId = 1},
                    new HotelRoom {HotelId = 2, HotelRoomId = 13, Number = "11", PricePerDay = 100, TypeComfortId = 1, TypeSizeId = 1},
                    new HotelRoom {HotelId = 2, HotelRoomId = 14, Number = "12", PricePerDay = 200, TypeComfortId = 2, TypeSizeId = 1},
                    new HotelRoom {HotelId = 2, HotelRoomId = 15, Number = "13", PricePerDay = 200, TypeComfortId = 2, TypeSizeId = 2},
                    new HotelRoom {HotelId = 2, HotelRoomId = 16, Number = "20", PricePerDay = 250, TypeComfortId = 2, TypeSizeId = 3},
                    new HotelRoom {HotelId = 2, HotelRoomId = 17, Number = "21", PricePerDay = 250, TypeComfortId = 2, TypeSizeId = 5},
                    new HotelRoom {HotelId = 2, HotelRoomId = 18, Number = "22", PricePerDay = 300, TypeComfortId = 3, TypeSizeId = 1},
                    new HotelRoom {HotelId = 2, HotelRoomId = 19, Number = "30", PricePerDay = 400, TypeComfortId = 3, TypeSizeId = 2},
                    new HotelRoom {HotelId = 2, HotelRoomId = 20, Number = "31", PricePerDay = 400, TypeComfortId = 4, TypeSizeId = 3},
                    new HotelRoom {HotelId = 2, HotelRoomId = 21, Number = "40", PricePerDay = 600, TypeComfortId = 5, TypeSizeId = 6},
                    new HotelRoom {HotelId = 2, HotelRoomId = 22, Number = "50", PricePerDay = 800, TypeComfortId = 6, TypeSizeId = 2},

                    new HotelRoom {HotelId = 3, HotelRoomId = 23, Number = "10", PricePerDay = 100, TypeComfortId = 1, TypeSizeId = 1},
                    new HotelRoom {HotelId = 3, HotelRoomId = 24, Number = "11", PricePerDay = 100, TypeComfortId = 1, TypeSizeId = 1},
                    new HotelRoom {HotelId = 3, HotelRoomId = 25, Number = "12", PricePerDay = 200, TypeComfortId = 2, TypeSizeId = 1},
                    new HotelRoom {HotelId = 3, HotelRoomId = 26, Number = "13", PricePerDay = 200, TypeComfortId = 2, TypeSizeId = 2},
                    new HotelRoom {HotelId = 3, HotelRoomId = 27, Number = "20", PricePerDay = 250, TypeComfortId = 2, TypeSizeId = 3},
                    new HotelRoom {HotelId = 3, HotelRoomId = 28, Number = "21", PricePerDay = 250, TypeComfortId = 2, TypeSizeId = 5},
                    new HotelRoom {HotelId = 3, HotelRoomId = 29, Number = "22", PricePerDay = 300, TypeComfortId = 3, TypeSizeId = 1},
                    new HotelRoom {HotelId = 3, HotelRoomId = 30, Number = "30", PricePerDay = 400, TypeComfortId = 3, TypeSizeId = 2},
                    new HotelRoom {HotelId = 3, HotelRoomId = 31, Number = "31", PricePerDay = 400, TypeComfortId = 4, TypeSizeId = 3},
                    new HotelRoom {HotelId = 3, HotelRoomId = 32, Number = "40", PricePerDay = 600, TypeComfortId = 5, TypeSizeId = 6},
                    new HotelRoom {HotelId = 3, HotelRoomId = 33, Number = "50", PricePerDay = 800, TypeComfortId = 6, TypeSizeId = 2}
                });
        }
    }
}
