using Microsoft.EntityFrameworkCore;
using HotelApp.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelApp.DAL.EF.Configuration
{
    class TypeSizeConfiguration: IEntityTypeConfiguration<TypeSize>
    {
        public void Configure(EntityTypeBuilder<TypeSize> builder)
        {
            builder.HasData(
                new TypeSize[]
                {
                    new TypeSize {TypeSizeId = 1, Size = TypeSizeEnum.SGL},
                    new TypeSize {TypeSizeId = 2, Size = TypeSizeEnum.DBL},
                    new TypeSize {TypeSizeId = 3, Size = TypeSizeEnum.DBL_TWN},
                    new TypeSize {TypeSizeId = 4, Size = TypeSizeEnum.TRPL},
                    new TypeSize {TypeSizeId = 5, Size = TypeSizeEnum.DBL_EXB},
                    new TypeSize {TypeSizeId = 6, Size = TypeSizeEnum.TRPL_EXB}
                });
            builder.Property(p => p.Size).HasConversion<string>();
        }
    }
}
