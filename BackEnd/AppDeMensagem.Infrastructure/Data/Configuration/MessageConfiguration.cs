
using AppDeMensagem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDeMensagem.Infrastructure.Data.Configuration;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("Messages");

        builder.HasKey(m => m.Message_ID);

        builder.Property(m => m.Text)
            .IsRequired()
            .HasMaxLength(3000);

        builder.Property(m => m.SendTime)
            .IsRequired()
            .HasColumnType("datetime2")
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(m => m.Status)
            .HasConversion<string>()
            .IsRequired();
        
        builder.HasOne(m => m.Chat)
            .WithMany(c => c.Messages)
            .HasForeignKey(m => m.Chat_ID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
