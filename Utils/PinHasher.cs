using BCrypt.Net;

namespace BakeryBackend.Utils
{
    public static class PinHasher
    {
        public static string Hash(string pin)
        {
            return BCrypt.Net.BCrypt.HashPassword(pin);
        }

        public static bool Verify(string pin, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(pin, hash);
        }
    }
}
