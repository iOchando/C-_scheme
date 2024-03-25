using Config.Database;
using Microsoft.EntityFrameworkCore;
using Modules.User.Dto;
using Modules.User.Models;

namespace Modules.User.Repositories;

public interface IUserRepository
{
    Task<UserModel> Create(UserCreateDto userCreateDto);
    Task<UserModel?> GetById(int id);
    Task<List<UserModel>> GetAll();
    Task<UserModel> Update(UserModel user);
    Task Delete(UserModel user);
}

public class UserRepository : IUserRepository
{
    private readonly DatabaseService _db;

    public UserRepository()
    {
        _db = new DatabaseService();
    }

    public async Task<UserModel> Create(UserCreateDto userCreateDto)
    {
        var user = await _db.Users.AddAsync(new UserModel
        {
            Name = userCreateDto.Name,
            Lastname = userCreateDto.Lastname,
        });

        await _db.SaveChangesAsync();

        return user.Entity;
    }

    public async Task<UserModel?> GetById(int id)
    {
        return await _db.Users.FindAsync(id);
    }

    public async Task<List<UserModel>> GetAll()
    {
        return await _db.Users.ToListAsync();
    }

    public async Task<UserModel> Update(UserModel user)
    {
        _db.Entry(user).State = EntityState.Modified;
        await _db.SaveChangesAsync();

        return user;
    }

    public async Task Delete(UserModel user)
    {
        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
    }
}