

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
        try
        {
            var users = await httpService.GetAsync<List<UserModel>>("user/get/all");

            return users.Data ?? new List<UserModel>();
        }
        catch (Exception ex)
        {
            ErrorDashboard.Add(ex.Message);
            return new List<UserModel>();
        }

    }
}
