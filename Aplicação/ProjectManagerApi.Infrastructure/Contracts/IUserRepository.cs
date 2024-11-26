using ProjectManagerApi.Domain.Models.DataModels;

namespace ProjectManagerApi.Infrastructure.Contracts;

public interface IUserRepository
{
    Task<Usuario> GetUserById(int userId);
}