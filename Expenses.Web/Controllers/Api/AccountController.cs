using Expenses.Common.Models;
using Expenses.Web.Data;
using Expenses.Web.Data.Entities;
using Expenses.Web.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Expenses.Common.Enums;
using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Expenses.Web.Controllers.Api
{
    [Route("api/[Controller]")]
    public class AccountController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IConfiguration _configuration;
        private readonly IMailHelper _mailHelper;

        public AccountController(
            DataContext dataContext,
            IUserHelper userHelper,
            IConfiguration configuration,
            IConverterHelper converterHelper,
            IMailHelper mailHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
            _configuration = configuration;
            _mailHelper = mailHelper;
            _converterHelper = converterHelper;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Authentication([FromBody] LoginRequest modelRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CultureInfo cultureInfo = new CultureInfo(modelRequest.CultureInfo);
            //Resource.Culture = cultureInfo;

            UserEntity user = await _userHelper.GetUserAsync(modelRequest.Username);
            if (user != null)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _userHelper.ValidatePasswordAsync(user, modelRequest.Password);

                if (result.Succeeded)
                {
                    Claim[] claims = new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                    SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                    SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    JwtSecurityToken token = new JwtSecurityToken(
                        _configuration["Tokens:Issuer"],
                        _configuration["Tokens:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddDays(15),
                        signingCredentials: credentials);
                  
                    var response = _converterHelper.ToUserResponse(user,
                        new JwtSecurityTokenHandler().WriteToken(token),
                        token.ValidTo);

                    return Accepted(response);
                }
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Bad request",
                    Result = ModelState
                });
            }

            CultureInfo cultureInfo = new CultureInfo(request.CultureInfo);
            //Todo agrega multi idioma
            //Resource.Culture = cultureInfo;

            UserEntity user = await _userHelper.GetUserAsync(request.Email);
            if (user != null)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "El usuario ya exite" //Resource.UserAlreadyExists
                });
            }



            user = new UserEntity
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email,
                UserType = UserType.User,
            };

            IdentityResult result = await _userHelper.AddUserAsync(user, request.Password);
            if (result != IdentityResult.Success)
            {
                return BadRequest(result.Errors.FirstOrDefault().Description);
            }

            UserEntity userNew = await _userHelper.GetUserAsync(request.Email);
            await _userHelper.AddUserToRoleAsync(userNew, user.UserType.ToString());

            string myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
            string tokenLink = Url.Action("ConfirmEmail", "Account", new
            {
                userid = user.Id,
                token = myToken
            }, protocol: HttpContext.Request.Scheme);

  //          _mailHelper.SendMail(request.Email, Resource.ConfirmEmail, $"<h1>{Resource.ConfirmEmail}</h1>" +
//                $"{Resource.ConfirmEmailSubject}</br></br><a href = \"{tokenLink}\">{Resource.ConfirmEmail}</a>");

            _mailHelper.SendMail(request.Email, "Connfirmar email", $"<h1> confirmar email </h1>" +
            $"confirma email</br></br><a href = \"{tokenLink}\">confirmar email</a>");


            return Ok(new Response
            {
                IsSuccess = true,
                Message = "comfirme email"//Resource.ConfirmEmailMessage
            });
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        [Route("EditUser")]
        public async Task<IActionResult> EditUser([FromBody] UserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CultureInfo cultureInfo = new CultureInfo(request.CultureInfo);
            //Resource.Culture = cultureInfo;

            UserEntity userEntity = await _userHelper.GetUserAsync(request.Email);
            if (userEntity == null)
            {
                return BadRequest("usuario no existe");//BadRequest(Resource.UserDoesntExists);
            }


            userEntity.FirstName = request.FirstName;
            userEntity.LastName = request.LastName;

            IdentityResult respose = await _userHelper.UpdateUserAsync(userEntity);
            if (!respose.Succeeded)
            {
                return BadRequest(respose.Errors.FirstOrDefault().Description);
            }

            return Ok(new Response
            {
                IsSuccess = true,
                Message = "se realizaron los cambio" //Resource.RecoverPasswordMessage
            }); ;
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Bad request",
                    Result = ModelState
                });
            }

            CultureInfo cultureInfo = new CultureInfo(request.CultureInfo);
            //Resource.Culture = cultureInfo;

            UserEntity user = await _userHelper.GetUserAsync(request.Email);
            if (user == null)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "el usuario no existe"//Resource.UserDoesntExists
                });
            }

            IdentityResult result = await _userHelper.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                string message = result.Errors.FirstOrDefault().Description;
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "clave incorrecta"//message.Contains("password") ? Resource.IncorrectCurrentPassword : message
                });
            }

            return Ok(new Response
            {
                IsSuccess = true,
                Message = "cambio exitoso"//Resource.PasswordChangedSuccess
            });
        }


        [HttpPost]
        [Route("RecoverPassword")]
        public async Task<IActionResult> RecoverPassword([FromBody] RecoverPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Bad request"
                });
            }

            CultureInfo cultureInfo = new CultureInfo(request.CultureInfo);
            //Resource.Culture = cultureInfo;

            UserEntity user = await _userHelper.GetUserAsync(request.Email);
            if (user == null)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    //Message = Resource.UserDoesntExists
                });
            }

            string myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);
            string link = Url.Action("ResetPassword", "Account", new { token = myToken }, protocol: HttpContext.Request.Scheme);
            //_mailHelper.SendMail(request.Email, Resource.RecoverPassword, $"<h1>{Resource.RecoverPassword}</h1>" +
            //  $"{Resource.RecoverPasswordSubject}:</br></br>" +
            //$"<a href = \"{link}\">{Resource.RecoverPassword}</a>");

            _mailHelper.SendMail(request.Email, "mensaje", $"<h1>mensaje</h1>" +
              $"mensaje:</br></br>" +
            $"<a href = \"{link}\">mensaje</a>");

            return Ok(new Response
            {
                IsSuccess = true,
                Message = "clave recuperada" //Resource.RecoverPasswordMessage
            });
        }

    }
}
