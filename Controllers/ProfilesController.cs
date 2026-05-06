using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BakeryBackend.Data;
using BakeryBackend.Models;
using BakeryBackend.Dtos;
using BakeryBackend.Utils; 

namespace BakeryBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class ProfilesController : ControllerBase
    {
        private readonly BakeryContext _db;

        public ProfilesController(BakeryContext db)
        {
            _db = db;
        }

        /* ---------------------------------------------------------
           ✅ Get All Profiles
           GET: api/profiles
        --------------------------------------------------------- */
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var profiles = await _db.Profiles.ToListAsync();
            return Ok(profiles);
        }

        /* ---------------------------------------------------------
           🔍 FIND PROFILE (email or phone)
           GET: api/profiles/find?value=...
        --------------------------------------------------------- */
        [HttpGet("find")]
        public async Task<IActionResult> FindProfile([FromQuery] string value)
        {
            var profile = await _db.Profiles
                .FirstOrDefaultAsync(u => u.Phone == value || u.Email == value);

            if (profile == null) return NotFound();
            return Ok(profile);
        }

        /* ---------------------------------------------------------
           🔄 UPDATE PROFILE
           PUT: api/profiles/{id}
        --------------------------------------------------------- */
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile(Guid id, [FromBody] UserDto updatedUser)
        {
            var profile = await _db.Profiles.FindAsync(id);
            if (profile == null) return NotFound();

            profile.Name = updatedUser.Name;
            profile.Phone = updatedUser.Phone;
            profile.Address = updatedUser.Address;
            profile.City = updatedUser.City;
            profile.Zip = updatedUser.Zip;

            // Allow role updates
            if (!string.IsNullOrEmpty(updatedUser.Role))
                profile.Role = updatedUser.Role;

            await _db.SaveChangesAsync();
            return Ok(profile);
        }

        /* ---------------------------------------------------------
           ➕ CREATE CUSTOMER or EMPLOYEE
           POST: api/profiles
           - If dto.Pin is provided → create EMPLOYEE
           - If dto.Pin is null → create CUSTOMER
        --------------------------------------------------------- */
        [HttpPost]
        public async Task<IActionResult> CreateProfile([FromBody] UserDto dto)
        {
            try
            {
                string? pinHash = null;

                // If PIN is provided → hash it
                if (!string.IsNullOrEmpty(dto.PinHash))
                {
                    pinHash = PinHasher.HashPin(dto.PinHash);
                }

                var newProfile = new Profile
                {
                    Id = Guid.NewGuid(),
                    Name = dto.Name ?? "New Customer",
                    Phone = dto.Phone,
                    Email = dto.Email,

                    // If role not provided → default to customer
                    Role = dto.Role ?? "customer",

                    // Employee fields (only used if employee)
                    EmployeeId = dto.EmployeeId,
                    PinHash = pinHash,

                    LoyaltyPoints = 0
                };

                _db.Profiles.Add(newProfile);
                await _db.SaveChangesAsync();

                return Ok(newProfile);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(500, new { Error = "Database crash", Details = errorMessage });
            }
        }

        /* ---------------------------------------------------------
           ❌ DELETE PROFILE
           DELETE: api/profiles/{id}
        --------------------------------------------------------- */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(Guid id)
        {
            var profile = await _db.Profiles.FindAsync(id);
            if (profile == null) return NotFound(new { Message = "Profile not found." });

            _db.Profiles.Remove(profile);
            await _db.SaveChangesAsync();
            return Ok(new { Message = "Profile deleted successfully." });
        }
    }
}
