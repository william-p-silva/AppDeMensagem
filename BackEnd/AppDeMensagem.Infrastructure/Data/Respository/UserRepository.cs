

using AppDeMensagem.Application.Interfaces.Repositorys;
using AppDeMensagem.Domain.Entity;
using AppDeMensagem.Domain.Enum;
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

    public async Task<List<Usuario>> ListAsync(PerfilUser? profile = PerfilUser.User)
    {
        var query = context.Users.AsNoTracking().AsQueryable();

        if (profile is null)
            query = query.Where(u => u.UserProfile != PerfilUser.Deleted);
        else if (profile == PerfilUser.User)
            query = query.Where(u => u.UserProfile != profile);
        else if (profile == PerfilUser.Admin)
            query = query.Where(u => u.UserProfile != profile);
        else if (profile == PerfilUser.Deleted)
            query = query.Where(u => u.UserProfile != profile);

        return await query.ToListAsync();
    }

    public async Task<Usuario?> FindById(Guid user_id)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.User_ID == user_id);
    }

    public async Task<Usuario?> FindByEmail(string email)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.EmailAddress.Endereco == email);
    }
}
