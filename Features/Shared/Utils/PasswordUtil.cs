using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace SaldoFlex.API.Features.Shared.Utils;

public class PasswordUtil
{
    public static string Hash(string pwd)
    {
        byte[] salt = [128 / 8];
        string pwdHashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: pwd,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8
        ));

        return pwdHashed;
    }
}
