

using AppDeMensagem.Application.DTOs.User.Response;
using AppDeMensagem.Application.Interfaces.Repositorys;

namespace AppDeMensagem.Application.UseCases.User.List;

public class ListAllUsers(IUserRepository userRepository)
{
    public async Task<List<ResponseUser>> ExecuteAsync()
    {
        var users = await userRepository.ListAsync();

        return users.Select(x => new ResponseUser
        {
            Email = x.EmailAddress.Endereco,
            Name = x.UserName.TextName,
            Profile = x.UserProfile.ToString(),
            User_ID = x.User_ID
        }).ToList();
    }
}
