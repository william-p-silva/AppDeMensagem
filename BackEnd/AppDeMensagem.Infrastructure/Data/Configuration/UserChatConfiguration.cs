

using AppDeMensagem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDeMensagem.Infrastructure.Data.Configuration;

public class UserChatConfiguration : IEntityTypeConfiguration<UserChat>
{
    public void Configure(EntityTypeBuilder<UserChat> builder)
    {
        builder.ToTable("UsersChat");

        builder.HasKey(uc => uc.UserChat_ID);

        builder.Property(uc => uc.IsAdmin)
            .IsRequired();

        builder.HasOne(uc => uc.Usuario)
            .WithMany(u => u.UsersChat)
            .HasForeignKey(uc => uc.User_ID)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(uc => uc.Chat)
            .WithMany(c => c.UsersChat)
            .HasForeignKey(uc => uc.Chat_ID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
