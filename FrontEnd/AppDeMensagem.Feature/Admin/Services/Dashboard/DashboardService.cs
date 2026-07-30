

using AppDeMensagem.Core.Http;
using AppDeMensagem.Feature.Admin.Models.User;

namespace AppDeMensagem.Feature.Admin.Services.Dashboard;

public class DashboardService(HttpService httpService)
{
    public List<string> ErrorDashboard { get; private set; } = new List<string>();
    public bool IsLoading { get; set; } = true;
    public List<UserModel> Users { get; private set; } = new List<UserModel>();

    public async Task GetAllUsers()
    {
        var result = await ListAllUser();

        Users = result ?? new List<UserModel>();

        IsLoading = false;
    }

    private async Task<List<UserModel>> ListAllUser()
    {
        var users = await httpService.GetAsync<List<UserModel>>("user/get/all");
        if (users?.Data is null || !users.Success)
        {
            if (httpService.Error?.Any() == true)
            {
                ErrorDashboard.Add(httpService.Error.Last());
            }
            else
            {
                ErrorDashboard.Add("Erro ao carregar a lista de usuários.");
            }
            return null;
        }
        return users.Data;
    }
}
