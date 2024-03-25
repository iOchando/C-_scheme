using System.ComponentModel.DataAnnotations;

namespace Modules.User.Dto;

public class UserCreateDto
{
  [Required]
  [MaxLength(50)]
  public string Name { get; set; } = string.Empty;

  [Required]
  [MaxLength(50)]
  public string Lastname { get; set; } = string.Empty;
}