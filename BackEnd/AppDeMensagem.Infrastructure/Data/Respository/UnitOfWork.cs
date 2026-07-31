

using AppDeMensagem.Application.Interfaces.Repositorys;
using AppDeMensagem.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AppDeMensagem.Infrastructure.Data.Respository;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CommitAsync()
    {
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    Console.WriteLine($"Entidade em conflito: {entry.Entity.GetType().Name}");
                    Console.WriteLine($"Estado: {entry.State}");
                }
                throw;
            }
        }
    }
}
