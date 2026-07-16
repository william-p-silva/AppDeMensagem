

using AppDeMensagem.Domain.Entity;
using AppDeMensagem.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDeMensagem.Infrastructure.Data.Configuration;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.User_ID);

        builder.Property(u => u.PasswordHash).IsRequired();

        builder.Property(u => u.EmailAddress)
            .HasConversion(
                emailVo => emailVo.Endereco,
                dbValue => Email.Create(dbValue))
            .HasColumnName("Email")
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(u => u.UserName)
            .HasConversion(
                nameVo => nameVo.TextName,
                dbValue => Name.Create(dbValue))
            .HasColumnName("Name")
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(u => u.UserProfile)
            .IsRequired()
            .HasConversion<string>();
    }
}
