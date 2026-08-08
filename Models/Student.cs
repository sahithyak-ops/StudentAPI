using System.ComponentModel.DataAnnotations;

namespace StudentAPI.Models;

public class Student
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = "";

    [Range(17, 30)]
    public int Age { get; set; }

    [Required]
    public string Department { get; set; } = "";

    [EmailAddress]
    public string Email { get; set; } = "";

    [Phone]
    public string Phone { get; set; } = "";
}