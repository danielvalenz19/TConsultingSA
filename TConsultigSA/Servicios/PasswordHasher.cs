using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace TConsultigSA.Servicios
{

	public class PasswordHasher
	{
		public static string HashPassword(string password)
		{
			byte[] salt = new byte[128 / 8];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(salt);
			}

			string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
				password: password,
				salt: salt,
				prf: KeyDerivationPrf.HMACSHA256,
				iterationCount: 10000,
				numBytesRequested: 256 / 8));

			return $"{Convert.ToBase64String(salt)}.{hashed}";
		}

		public static bool VerifyPassword(string hashedPassword, string password)
		{
			var parts = hashedPassword.Split('.');
			if (parts.Length != 2)
			{
				return false;
			}

			var salt = Convert.FromBase64String(parts[0]);
			var hashOfInput = Convert.ToBase64String(KeyDerivation.Pbkdf2(
				password: password,
				salt: salt,
				prf: KeyDerivationPrf.HMACSHA256,
				iterationCount: 10000,
				numBytesRequested: 256 / 8));

			return hashOfInput == parts[1];
		}
	}
}
