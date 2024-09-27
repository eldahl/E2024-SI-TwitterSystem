using System.ComponentModel.DataAnnotations;

namespace AccountService;

public class Account
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    public string Email { get; set; }
    public string Bio { get; set; }
    [Key]
    [Required]
    public int ID { get; set; }
    
}