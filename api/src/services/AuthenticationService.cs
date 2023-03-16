using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using twitter_clone.Models;

namespace twitter_clone.services;

public class AuthenticationService
{
    public static string HashString(string password)
    {
        SHA256 sha256Hash = SHA256.Create();

        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

        StringBuilder builder = new StringBuilder();
        foreach (byte b in bytes)
        {
            builder.Append(b.ToString("x2"));
        }

        return builder.ToString();
    }

    public static string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET")!);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(
                new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                }
            ),
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public static bool IsTokenValid(string token)
    {
        var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET")!);
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        TokenValidationParameters validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
        try
        {
            if (tokenHandler.CanReadToken(token))
            {
                var user = tokenHandler.ValidateToken(
                    token,
                    validationParameters,
                    out SecurityToken validatedToken
                );
                var identity = user.Identity;
                var role = user.FindFirst(ClaimTypes.Role);
                if (validatedToken.ValidTo < DateTime.Now)
                    return false;
                if (identity == null || identity.Name.IsNullOrEmpty())
                    return false;
                if (role == null || role.Value.IsNullOrEmpty())
                    return false;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
        return true;
    }
}
