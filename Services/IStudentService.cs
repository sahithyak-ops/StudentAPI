using StudentAPI.DTOs;

namespace StudentAPI.Services;

public interface IStudentService
{
    Task<IEnumerable<StudentResponseDto>> GetAllStudentsAsync(
    int page,
    int pageSize);

    Task<StudentResponseDto?> GetStudentByIdAsync(int id);

    Task<IEnumerable<StudentResponseDto>> SearchStudentsAsync(
    string name,
    int page,
    int pageSize);

    Task<StudentResponseDto> AddStudentAsync(StudentDto dto);

    Task<bool> UpdateStudentAsync(int id, StudentDto dto);

    Task<bool> DeleteStudentAsync(int id);
}