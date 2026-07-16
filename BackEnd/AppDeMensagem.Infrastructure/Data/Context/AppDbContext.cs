
using AppDeMensagem.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace AppDeMensagem.Infrastructure.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Usuario> Users { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<UserChat> UsersChat { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<ChatGroup> ChatsGroup { get; set; }
    public DbSet<ChatPrivate> ChatsPrivate { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
    
}
