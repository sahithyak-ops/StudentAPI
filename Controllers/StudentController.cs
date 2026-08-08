using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using StudentAPI.DTOs;
using StudentAPI.Services;

namespace StudentAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    // GET: api/Student
    [HttpGet]
[Authorize(Roles = "Student,Admin")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
public async Task<IActionResult> GetStudents(
    int page = 1,
    int pageSize = 10)
{
    if (page < 1)
        page = 1;

    if (pageSize < 1)
        pageSize = 10;

    if (pageSize > 100)
        pageSize = 100;

    var students = await _studentService
        .GetAllStudentsAsync(page, pageSize);

    return Ok(new ApiResponseDto<IEnumerable<StudentResponseDto>>
    {
        Success = true,
        Message = "Students retrieved successfully.",
        Data = students
    });
}

    // GET: api/Student/{id}
    [HttpGet("{id}")]
    [Authorize(Roles = "Student,Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetStudent(int id)
    {
        var student = await _studentService.GetStudentByIdAsync(id);

        if (student == null)
        {
            return NotFound(new ApiResponseDto<object>
            {
                Success = false,
                Message = "Student not found.",
                Data = null
            });
        }

        return Ok(new ApiResponseDto<StudentResponseDto>
        {
            Success = true,
            Message = "Student retrieved successfully.",
            Data = student
        });
    }

    // GET: api/Student/search?name=Priya
    [HttpGet("search")]
    [Authorize(Roles = "Student,Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> SearchStudents(
        string name,
        int page = 1,
        int pageSize = 10)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return BadRequest(new ApiResponseDto<object>
            {
                Success = false,
                Message = "Search name cannot be empty.",
                Data = null
            });
        }

        if (page < 1)
            page = 1;

        if (pageSize < 1)
            pageSize = 10;

        if (pageSize > 100)
            pageSize = 100;

        var students = await _studentService.SearchStudentsAsync(
            name,
            page,
            pageSize);

        return Ok(new ApiResponseDto<IEnumerable<StudentResponseDto>>
        {
            Success = true,
            Message = "Search completed successfully.",
            Data = students
        });
    }
    // POST: api/Student
    // Admin only
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> AddStudent(StudentDto dto)
    {
        var student = await _studentService.AddStudentAsync(dto);

        return Ok(new ApiResponseDto<StudentResponseDto>
        {
            Success = true,
            Message = "Student added successfully.",
            Data = student
        });
    }

    // PUT: api/Student/{id}
    // Admin only
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStudent(
        int id,
        StudentDto dto)
    {
        var updated = await _studentService.UpdateStudentAsync(id, dto);

        if (!updated)
        {
            return NotFound(new ApiResponseDto<object>
            {
                Success = false,
                Message = "Student not found.",
                Data = null
            });
        }

        return Ok(new ApiResponseDto<object>
        {
            Success = true,
            Message = "Student updated successfully.",
            Data = null
        });
    }

    // DELETE: api/Student/{id}
    // Admin only
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var deleted = await _studentService.DeleteStudentAsync(id);

        if (!deleted)
        {
            return NotFound(new ApiResponseDto<object>
            {
                Success = false,
                Message = "Student not found.",
                Data = null
            });
        }

        return Ok(new ApiResponseDto<object>
        {
            Success = true,
            Message = "Student deleted successfully.",
            Data = null
        });
    }

    // GET: api/Student/admin-test
    [HttpGet("admin-test")]
    [Authorize(Roles = "Admin")]
    public IActionResult AdminTest()
    {
        return Ok(new ApiResponseDto<object>
        {
            Success = true,
            Message = "You have Admin access!",
            Data = null
        });
    }
}