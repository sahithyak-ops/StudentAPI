using Microsoft.EntityFrameworkCore;
using StudentAPI.Data;
using StudentAPI.DTOs;
using StudentAPI.Models;

namespace StudentAPI.Services;

public class StudentService : IStudentService
{
    private readonly AppDbContext _context;
    private readonly ILogger<StudentService> _logger;

    public StudentService(
        AppDbContext context,
        ILogger<StudentService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<StudentResponseDto>> GetAllStudentsAsync(
    int page,
    int pageSize)
{
    return await _context.Students
        .OrderBy(s => s.Name)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .Select(s => new StudentResponseDto
        {
            Id = s.Id,
            Name = s.Name,
            Age = s.Age,
            Department = s.Department,
            Email = s.Email,
            Phone = s.Phone
        })
        .ToListAsync();
}

    public async Task<StudentResponseDto?> GetStudentByIdAsync(int id)
    {
        return await _context.Students
            .Where(s => s.Id == id)
            .Select(s => new StudentResponseDto
            {
                Id = s.Id,
                Name = s.Name,
                Age = s.Age,
                Department = s.Department,
                Email = s.Email,
                Phone = s.Phone
            })
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<StudentResponseDto>> SearchStudentsAsync(
    string name,
    int page,
    int pageSize)
{
    return await _context.Students
        .Where(s => s.Name.Contains(name))
        .OrderBy(s => s.Name)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .Select(s => new StudentResponseDto
        {
            Id = s.Id,
            Name = s.Name,
            Age = s.Age,
            Department = s.Department,
            Email = s.Email,
            Phone = s.Phone
        })
        .ToListAsync();
}
    public async Task<StudentResponseDto> AddStudentAsync(StudentDto dto)
    {
        var student = new Student
        {
            Name = dto.Name,
            Age = dto.Age,
            Department = dto.Department,
            Email = dto.Email,
            Phone = dto.Phone
        };

        _context.Students.Add(student);

        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "Student added: {Name}, ID: {Id}",
            student.Name,
            student.Id);

        return new StudentResponseDto
        {
            Id = student.Id,
            Name = student.Name,
            Age = student.Age,
            Department = student.Department,
            Email = student.Email,
            Phone = student.Phone
        };
    }

    public async Task<bool> UpdateStudentAsync(int id, StudentDto dto)
    {
        var student = await _context.Students.FindAsync(id);

        if (student == null)
            return false;

        student.Name = dto.Name;
        student.Age = dto.Age;
        student.Department = dto.Department;
        student.Email = dto.Email;
        student.Phone = dto.Phone;

        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "Student updated: ID {Id}",
            id);

        return true;
    }

    public async Task<bool> DeleteStudentAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);

        if (student == null)
            return false;

        _context.Students.Remove(student);

        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "Student deleted: ID {Id}",
            id);

        return true;
    }
}