using Microsoft.EntityFrameworkCore;
using HotelApp.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelApp.DAL.EF.Configuration
{
    class TypeComfortConfiguration: IEntityTypeConfiguration<TypeComfort>
    {
        public void Configure(EntityTypeBuilder<TypeComfort> builder)
        {
            builder.HasData(
                new TypeComfort[]
                {
                    new TypeComfort {TypeComfortId = 1, Comfort = TypeComfortEnum.Standart},
                    new TypeComfort {TypeComfortId = 2, Comfort = TypeComfortEnum.Suite},
                    new TypeComfort {TypeComfortId = 3, Comfort = TypeComfortEnum.De_Luxe},
                    new TypeComfort {TypeComfortId = 4, Comfort = TypeComfortEnum.Duplex},
                    new TypeComfort {TypeComfortId = 5, Comfort = TypeComfortEnum.Family_Room},
                    new TypeComfort {TypeComfortId = 6, Comfort = TypeComfortEnum.Honeymoon_Room}
                });
            builder.Property(p => p.Comfort).HasConversion<string>();
        }
    }
}
