using Base.Exceptions.ExceptionModels;
using System.Security.Cryptography;
using System.Text;

namespace Base.Security.Services
{
	public class HashService
	{
		public static string Encrypt(string plainText, string key)
		{
			using (Aes aesAlg = Aes.Create())
			{
				aesAlg.Key = GetKeyBytes(key);
				aesAlg.GenerateIV();

				using (var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
				{
					using (var msEncrypt = new MemoryStream())
					{
						msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);

						using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
						{
							using (var swEncrypt = new StreamWriter(csEncrypt))
							{
								swEncrypt.Write(plainText);
							}

							return Convert.ToBase64String(msEncrypt.ToArray());
						}
					}
				}
			}
		}

		public static string Decrypt(string encryptedText, string key)
		{
			try
			{
				var cipherBytes = Convert.FromBase64String(encryptedText);

				using (Aes aesAlg = Aes.Create())
				{
					aesAlg.Key = GetKeyBytes(key);

					var iv = new byte[aesAlg.BlockSize / 8];
					Array.Copy(cipherBytes, 0, iv, 0, iv.Length);
					aesAlg.IV = iv;

					using (var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
					{
						using (var msDecrypt = new MemoryStream(cipherBytes, iv.Length, cipherBytes.Length - iv.Length))
						{
							using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
							{
								using (var srDecrypt = new StreamReader(csDecrypt))
								{
									return srDecrypt.ReadToEnd();
								}
							}
						}
					}
				}
			}
			catch
			{
				throw new AuthorizationException();
			}
		}

		#region Hash

		public static string Hash(string plainText)
		{
			byte[] salt = RandomNumberGenerator.GetBytes(16);
			byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
				Encoding.UTF8.GetBytes(plainText),
				salt,
				iterations: 100000,
				hashAlgorithm: HashAlgorithmName.SHA256,
				outputLength: 32);

			return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
		}

		public static bool VerifyHash(string plainText, string hashedText)
		{
			var parts = hashedText.Split(':');
			if (parts.Length != 2)
				return false;

			byte[] salt = Convert.FromBase64String(parts[0]);
			byte[] expectedHash = Convert.FromBase64String(parts[1]);

			byte[] actualHash = Rfc2898DeriveBytes.Pbkdf2(
				Encoding.UTF8.GetBytes(plainText),
				salt,
				iterations: 100000,
				hashAlgorithm: HashAlgorithmName.SHA256,
				outputLength: 32);

			return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
		}

		#endregion

		#region Behind the Scenes

		private static byte[] GetKeyBytes(string key)
		{
			using (SHA256 sha256 = SHA256.Create())
			{
				return sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
			}
		}

		#endregion
	}
}
