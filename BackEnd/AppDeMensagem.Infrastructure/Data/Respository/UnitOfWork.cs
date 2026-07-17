

using AppDeMensagem.Application.Interfaces.Repositorys;
using AppDeMensagem.Infrastructure.Data.Context;

namespace AppDeMensagem.Infrastructure.Data.Respository;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CommitAsync()
    {
        await context.SaveChangesAsync();
    }
}
