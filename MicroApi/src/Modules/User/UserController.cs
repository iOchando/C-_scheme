using Microsoft.AspNetCore.Mvc;
using Modules.User.Dto;
using Modules.User.Models;
using Modules.User.Services;

namespace Modules.User.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
  private readonly IUserService _userService;

  public UserController(IUserService userService)
  {
    _userService = userService;
  }

  [HttpPost]
  public async Task<ActionResult<UserModel>> CreateUser(UserCreateDto userCreateDto)
  {
    var user = await _userService.CreateUser(userCreateDto);

    return Ok(user);
  }

  [HttpGet]
  public async Task<ActionResult<List<UserModel>>> GetUsers()
  {
    var users = await _userService.GetUsers();
    return Ok(users);
  }

  [HttpGet("{userId}")]
  public async Task<ActionResult<UserModel>> GetUser(int userId)
  {
    var user = await _userService.GetUser(userId);

    if (user == null)
      return NotFound("User not found.");

    return Ok(user);
  }

  [HttpPut("{userId}")]
  public async Task<ActionResult<UserModel>> UpdateUser([FromRoute] int userId, [FromBody] UserCreateDto userCreateDto)
  {
    var userModel = await _userService.UpdateUser(userId, userCreateDto);

    return Ok(userModel);

  }

  [HttpDelete("{userId}")]
  public async Task<ActionResult<UserModel>> DeleteUser(int userId)
  {
    await _userService.DeleteUser(userId);

    return NoContent();

  }

}