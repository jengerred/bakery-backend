using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BakeryBackend.Data;          // DbContext
using BakeryBackend.Models;        // Profile model
using BakeryBackend.Utils;         // PinHasher
using BakeryBackend.Services;      // JwtService
using BakeryBackend.Dtos;

namespace BakeryBackend.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly BakeryContext _context;
        private readonly JwtService _jwtService;

        public AuthController(BakeryContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        // ---------------------------------------------------------
        // EMPLOYEE LOGIN (employeeId + PIN)
        // ---------------------------------------------------------
        [HttpPost("pin-login")]
        public async Task<IActionResult> PinLogin([FromBody] PinLoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.EmployeeId) || string.IsNullOrWhiteSpace(request.Pin))
                return BadRequest(new { error = "Employee ID and PIN required" });

            // Find employee by REAL employeeId column
            var user = await _context.Profiles
                .Where(u => u.EmployeeId == request.EmployeeId && u.Role != "customer")
                .FirstOrDefaultAsync();

            if (user == null)
                return Unauthorized(new { error = "Employee not found" });

            // Check null before verifying pin
            if (string.IsNullOrEmpty(user.PinHash))
            return Unauthorized(new { error = "PIN not set for this employee" });

            // Verify PIN
            if (!PinHasher.VerifyPin(request.Pin, user.PinHash))
                return Unauthorized(new { error = "Invalid PIN" });

            // Create JWT
            var token = _jwtService.GenerateToken(user);

            return Ok(new
            {
                id = user.Id,
                name = user.Name,
                role = user.Role,
                employeeId = user.EmployeeId,  
                token
            });
        }

        // ---------------------------------------------------------
        // GET CURRENT USER (requires JWT)
        // ---------------------------------------------------------
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var userId = User.FindFirst("id")?.Value;

            if (userId == null)
                return Unauthorized();

            var user = await _context.Profiles.FindAsync(Guid.Parse(userId));

            if (user == null)
                return Unauthorized();

            return Ok(new
            {
                id = user.Id,
                name = user.Name,
                role = user.Role,
                employeeId = user.EmployeeId   
            });
        }

        // ---------------------------------------------------------
        // SET PIN (Manager/Admin Only)
        // ---------------------------------------------------------
        [Authorize(Roles = "manager,admin")]
        [HttpPost("set-pin")]
        public async Task<IActionResult> SetPin([FromBody] SetPinRequest request)
        {
            var user = await _context.Profiles.FindAsync(request.UserId);

            if (user == null)
                return NotFound(new { error = "User not found" });

            // Validate PIN
            if (string.IsNullOrWhiteSpace(request.Pin))
                return BadRequest(new { error = "PIN is required" });

            if (request.Pin.Length != 4)
                return BadRequest(new { error = "PIN must be 4 digits" });

            // Hash and save
            user.PinHash = PinHasher.HashPin(request.Pin);

            await _context.SaveChangesAsync();

            return Ok(new { success = true });
        }


       // ---------------------------------------------------------
        // CHANGE PIN (Manager/Admin Only)
        // ---------------------------------------------------------
        [Authorize(Roles = "manager,admin")]
        [HttpPost("change-pin")]
        public async Task<IActionResult> ChangePin([FromBody] ChangePinDto dto)
        {
            if (string.IsNullOrEmpty(dto.EmployeeId) || string.IsNullOrEmpty(dto.NewPin))
                return BadRequest(new { error = "EmployeeId and NewPin are required." });

            var profile = await _context.Profiles
                .FirstOrDefaultAsync(p => p.EmployeeId == dto.EmployeeId);

            if (profile == null)
                return NotFound(new { error = "Employee not found." });

            if (dto.NewPin.Length != 4)
                return BadRequest(new { error = "PIN must be 4 digits" });

            profile.PinHash = PinHasher.HashPin(dto.NewPin);

            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "PIN updated successfully." });
        }
    }
    

    // ---------------------------------------------------------
    // REQUEST MODELS
    // ---------------------------------------------------------
    public class PinLoginRequest
    {
        public string? EmployeeId { get; set; }   // REAL employeeId
        public string? Pin { get; set; }
    }

    public class SetPinRequest
    {
        public Guid UserId { get; set; }
        public string? Pin { get; set; }
    }
}
