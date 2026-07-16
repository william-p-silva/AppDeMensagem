

using AppDeMensagem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDeMensagem.Infrastructure.Data.Configuration;

public class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.ToTable("Chats");

        builder.HasKey(c => c.Chat_ID);

        builder.HasDiscriminator<string>("ChatType")
            .HasValue<ChatPrivate>("Private")
            .HasValue<ChatGroup>("Group");

        builder.Navigation(c => c.Messages)
            .HasField("_messages")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Navigation(c => c.UsersChat)
            .HasField("_usersChat")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
