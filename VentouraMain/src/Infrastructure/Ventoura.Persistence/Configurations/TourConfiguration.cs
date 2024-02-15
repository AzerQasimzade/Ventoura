using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Domain.Entities;

namespace Ventoura.Persistence.Configurations
{
    public class TourConfiguration : IEntityTypeConfiguration<Tour>
    {
        public void Configure(EntityTypeBuilder<Tour> builder)
        {
            builder.Property(t => t.Name).IsRequired().HasMaxLength(50);
            builder.Property(t => t.Price).IsRequired().HasColumnType("decimal(6,2)");
            builder.Property(t => t.StartDate).IsRequired();
            builder.Property(t => t.Description).IsRequired().HasColumnType("text");
        }
    }
}
