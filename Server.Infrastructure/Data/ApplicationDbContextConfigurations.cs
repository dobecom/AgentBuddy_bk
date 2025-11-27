using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server.Core.Entities.Records;
using System.Reflection.Emit;

namespace Server.Infrastructure.Data
{
    public class ApplicationDbContextConfigurations
    {
        /// <summary>
        /// Configures the database model.
        /// </summary>
        /// <param name="builder">The ModelBuilder instance used to build the model.</param>
        public static void Configure(ModelBuilder builder)
        {
            builder.Entity<CaseResolution>(entity =>
            {
                entity.ToTable("case_resolutions");

                entity.Property(e => e.Content)
                    .HasColumnType("json"); // <-- 명시 필요

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            builder.Entity<CaseStatement>(entity =>
            {
                entity.ToTable("case_statements");

                entity.Property(e => e.Environments)
                    .HasColumnType("json"); // <-- 명시 필요

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
            // Configure custom entities
            // 예시: Product 엔티티에 대한 테이블 이름 설정 또는 추가 설정
            // builder.Entity<Product>().ToTable("Products"); 

            // Product 엔티티에 대한 추가적인 Fluent API 설정을 여기에 추가할 수 있습니다.
            // 예:
            // builder.Entity<Product>().Property(p => p.Name).HasMaxLength(250).IsRequired();
            // builder.Entity<Product>().HasKey(p => p.Id);
            // builder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18,2)");
        }
    }
}
