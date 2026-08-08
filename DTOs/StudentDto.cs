using System.ComponentModel.DataAnnotations;

namespace StudentAPI.DTOs;

public class StudentDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = "";

    [Range(5, 100)]
    public int Age { get; set; }

    [Required]
    [StringLength(50)]
    public string Department { get; set; } = "";

    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";

    [Required]
    [Phone]
    public string Phone { get; set; } = "";
}