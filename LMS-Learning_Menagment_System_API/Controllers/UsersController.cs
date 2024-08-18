using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LMS_Learning_Menagment_System_API.Models;
using LMS_Learning_Menagment_System_API.Helpers;
using LMS_Learning_Menagment_System_API.DTO;
using Microsoft.AspNetCore.Authorization;

namespace LMS_Learning_Menagment_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly LmsContext _context;
        private readonly AuthHelpers _authHelpers;

        public UsersController(LmsContext context, AuthHelpers authHelpers)
        {
            _context = context;
            _authHelpers = authHelpers;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            // Check for a user with the provided username and password
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == loginModel.UserName && u.Password == loginModel.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            // Generate JWT token
            var token = _authHelpers.GenerateJWTToken(user);

            return Ok(new { Token = token });
        }
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserModel>> GetUser(int id)
        {
            // Fetch user data along with related entities
            var user = await _context.Users
                .Include(u => u.City) // Include related data
                .Include(u => u.Country)
                .Include(u => u.Role)
                .Include(u => u.Class) // Include class information
                .Include(u => u.Grades) // Include grades to get the associated exams
                    .ThenInclude(g => g.Exam) // Include exam details
                .FirstOrDefaultAsync(u => u.Iduser == id);

            if (user == null)
            {
                return NotFound();
            }

            // Map to GetUserModel
            var userModel = new GetUserModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password, // Be cautious with including password in the response
                CountryName = user.Country?.Name,
                CityName = user.City?.Name,
                RoleName = user.Role?.RoleName,
                ClassName = user.Class?.ClassName,
                Exams = user.Grades.Select(g => new ExamDto
                {
                    ExamName = g.Exam.ExamName,
                    ExamDate = g.Exam.ExamDate, // Ensure this property is of type DateTime
                    Grade = g.Grade1 // Adjust if the property name is different
                }).ToList()
            };

            return Ok(userModel);
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            if (updateUserDto.FirstName != null)
            {
                user.FirstName = updateUserDto.FirstName;
            }
            if (updateUserDto.LastName != null)
            {
                user.LastName = updateUserDto.LastName;
            }
            if (updateUserDto.Email != null)
            {
                user.Email = updateUserDto.Email;
            }
            if (updateUserDto.Password != null)
            {
                user.Password = updateUserDto.Password;
            }
            if (updateUserDto.RoleId.HasValue)
            {
                user.RoleId = updateUserDto.RoleId.Value;
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Users.Any(e => e.Iduser == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> Register(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Iduser }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Iduser == id);
        }
    }
}
