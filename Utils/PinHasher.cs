using BCrypt.Net;

namespace BakeryBackend.Utils
{
    public static class PinHasher
    {
        public static string HashPin(string pin)
        {
            return BCrypt.Net.BCrypt.HashPassword(pin);
        }

        public static bool VerifyPin(string pin, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(pin, hash);
        }
    }
}
