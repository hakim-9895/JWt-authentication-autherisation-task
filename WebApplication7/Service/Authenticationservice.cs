
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApplication7.Modal;
namespace WebApplication7.Service
{
    public class Authenticationservice
    {
        private readonly string _key = "YourSuperSecretKeyThatIsAtLeast32BytesLongHere123!";
        private readonly List<User> _user = new List<User>();
    public Authenticationservice() {
        _user.Add(new User { Name = "suhail", passsword ="hello", Role = "Admin" });
        _user.Add(new User { Name = "shahid", passsword="iamshahid", Role = "User" });
    
    }
        public string Authenticate(string name , string password)
        {
            var users =_user.FirstOrDefault(x => x.Name==name&&x.passsword==password);
            if (users == null) 
                return null;

            var claims = new[]
            {
                new Claim(ClaimTypes.Name,users.Name),
                new Claim(ClaimTypes.Role,users.Role)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "YourApp",
                audience: "YourAppUsers",
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
    }

