

using Modules.User.Dto;
using Modules.User.Models;
using Modules.User.Repositories;

namespace Modules.User.Services;

public interface IUserService
{
  Task<UserModel> CreateUser(UserCreateDto userCreateDto);
  Task<List<UserModel>> GetUsers();
  Task<UserModel?> GetUser(int userId);
  Task<UserModel> UpdateUser(int userId, UserCreateDto userCreateDto);
  Task DeleteUser(int userId);
}

public class UserService : IUserService
{
  private readonly IUserRepository _userRepository;

  public UserService(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<UserModel> CreateUser(UserCreateDto userCreateDto)
  {
    var user = await _userRepository.Create(userCreateDto);
    return user;
  }

  public async Task<List<UserModel>> GetUsers()
  {
    var users = await _userRepository.GetAll();
    return users;
  }

  public async Task<UserModel?> GetUser(int userId)
  {
    var user = await _userRepository.GetById(userId);
    return user;
  }

  public async Task<UserModel> UpdateUser(int userId, UserCreateDto userCreateDto)
  {
    var user = await _userRepository.GetById(userId);

    if (user == null)
    {
      throw new Exception("User not found.");
    };

    user.Name = userCreateDto.Name;
    user.Lastname = userCreateDto.Lastname;

    return await _userRepository.Update(user);
  }

  public async Task DeleteUser(int userId)
  {
    var user = await _userRepository.GetById(userId);

    if (user == null)
    {
      throw new Exception("User not found.");
    };

    await _userRepository.Delete(user);
  }
}