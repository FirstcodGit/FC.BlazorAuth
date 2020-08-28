using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FC.BlazorAuth.Shared;
using FC.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FC.BlazorAuth.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SignInViewModel model)
        {
            try
            {
                CustomerViewModel customerView = new CustomerViewModel();

                var result = await _unitOfWork.Customer.CustomerControl(model.CustomerEmailAddress, GetPasswordHash(model.CustomerPassword));

                if(result == null)
                {
                    customerView.Auth = false;

                    return BadRequest(customerView);
                }

                var claim = new Claim[]
                {
                    new Claim(ClaimTypes.Name, result.CustomerEmailAddress),
                    new Claim(ClaimTypes.Role, result.Roles)
                };

                var date = DateTime.Now.ToUniversalTime().AddMinutes(2);

                var token = new JwtSecurityToken(
                    null,
                    null,
                    claim,
                    expires: date,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Startup.AppKey)), SecurityAlgorithms.HmacSha256Signature));

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                customerView = new CustomerViewModel()
                {
                    Auth = true,
                    Expired = date.ToString(),
                    Token = jwt
                };

                return Ok(customerView);
            }
            catch
            {
                return NotFound();
            }
        }

        private byte[] GetPasswordHash(string password)
        {
            MD5 md5 = MD5.Create();
            byte[] byt = ASCIIEncoding.ASCII.GetBytes(password);
            byte[] passWord = md5.ComputeHash(byt);

            return passWord;
        }
    }
}