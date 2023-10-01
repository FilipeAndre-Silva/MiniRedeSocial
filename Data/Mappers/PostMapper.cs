using AnySocialNetwork.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnySocialNetwork.Data.Mappers
{
    public class PostMapper : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            // Nome da tabela
            builder.ToTable("Posts");

            // Chave primária
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Content).IsRequired().HasMaxLength(300);
            builder.Property(p => p.CreationDate);
            builder.Property(p => p.ModificationDate);
            builder.Property(p => p.UserId);

            builder.HasOne(p => p.User)
                   .WithMany(u => u.Posts)
                   .HasForeignKey(p => p.UserId);
        }
    }
}