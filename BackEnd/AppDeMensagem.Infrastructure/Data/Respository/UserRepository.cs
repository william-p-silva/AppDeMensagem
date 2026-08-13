

using AppDeMensagem.Application.Interfaces.Repositorys;
using AppDeMensagem.Domain.Entity;
using AppDeMensagem.Domain.Enum;
using AppDeMensagem.Domain.ValueObjects;
using AppDeMensagem.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AppDeMensagem.Infrastructure.Data.Respository;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task AddAsync(Usuario user)
    {
        await context.Users.AddAsync(user);
    }

    public void Delete(Usuario user)
    {
        context.Users.Remove(user);
    }

    public async Task<List<Usuario>> ListAsync(PerfilUser? profile = null)
    {
        var query = context.Users.AsNoTracking().AsQueryable();

        if (profile is null)
            return await query.Where(u => u.UserProfile != PerfilUser.Deleted).ToListAsync();

        return await query.Where(u => u.UserProfile == profile).ToListAsync();

    }

    public async Task<Usuario?> FindById(Guid user_id)
    {
        return await context.Users
            .Include(u => u.UsersChat)
            .ThenInclude(uc => uc.Chat)
            .FirstOrDefaultAsync(x => x.User_ID == user_id);
    }

    public async Task<Usuario?> FindByEmail(string email)
    {
        var emailVo = Email.Create(email);

        return await context.Users
            .Include(u => u.UsersChat)
            .ThenInclude(uc => uc.Chat)
            .FirstOrDefaultAsync(x => x.EmailAddress == emailVo);
    }
}
