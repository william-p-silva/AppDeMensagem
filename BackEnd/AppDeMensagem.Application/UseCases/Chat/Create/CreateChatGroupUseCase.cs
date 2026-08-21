

using AppDeMensagem.Application.DTOs.Chat.Request;
using AppDeMensagem.Application.Interfaces.Repositorys;
using AppDeMensagem.Domain.Entity;

namespace AppDeMensagem.Application.UseCases.Chat.Create;

public class CreateChatGroupUseCase(
    IChatRepository chatRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork
    )
{
    public async Task<Guid> ExecuteAsync(Guid userPrimary_id, RequestNewChatGroup request)
    {
        var usersIds = request.Users_IDs
                    .Distinct()
                    .ToList();

        var userPrimaryInListIds = usersIds.Contains(userPrimary_id );
        if (userPrimaryInListIds)
            throw new ArgumentException("The primary user cannot be included in Users_IDs.");

        var userPrimary = await userRepository.FindById(userPrimary_id);
        if (userPrimary is null)
            throw new ArgumentNullException(nameof(userPrimary), "On or more of the user was null. ");

        List<Usuario> users = new List<Usuario>();

        foreach (Guid id in usersIds)
        {
            var user = await userRepository.FindById(id);

            if (user is null)
                throw new ArgumentNullException(nameof(user), "On or more of the user was null. ");

            users.Add(user);
        }

        ChatGroup chatGroup = new ChatGroup(userPrimary: userPrimary,
                                            users: users, name: request.Name);

        await chatRepository.AddAsync(chatGroup);

        await unitOfWork.CommitAsync();

        return chatGroup.Chat_ID;
    }
}
