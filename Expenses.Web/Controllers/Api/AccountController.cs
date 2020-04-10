using Expenses.Common.Model;
using Expenses.Web.Data;
using Expenses.Web.Data.Entities;
using Expenses.Web.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Soccer.Common.Enums;
using Soccer.Common.Models;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;


namespace Expenses.Web.Controllers.Api
{
    [Route("api/[Controller]")]
    public class AccountController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;
        //private readonly IConverterHelper _converterHelper;

        public AccountController(
            DataContext dataContext,
            IUserHelper userHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
            //_converterHelper = converterHelper;
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
                }) ;
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

            //string myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
            string tokenLink = Url.Action("ConfirmEmail", "Account", new
            {
                userid = user.Id,
                //token = myToken
            }, protocol: HttpContext.Request.Scheme);

            //_mailHelper.SendMail(request.Email, Resource.ConfirmEmail, $"<h1>{Resource.ConfirmEmail}</h1>" +
            //    $"{Resource.ConfirmEmailSubject}</br></br><a href = \"{tokenLink}\">{Resource.ConfirmEmail}</a>");

            return Ok(new Response
            {
                IsSuccess = true,
                Message = "comfirme email"//Resource.ConfirmEmailMessage
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
            //string link = Url.Action("ResetPassword", "Account", new { token = myToken }, protocol: HttpContext.Request.Scheme);
            //_mailHelper.SendMail(request.Email, Resource.RecoverPassword, $"<h1>{Resource.RecoverPassword}</h1>" +
              //  $"{Resource.RecoverPasswordSubject}:</br></br>" +
                //$"<a href = \"{link}\">{Resource.RecoverPassword}</a>");

            return Ok(new Response
            {
                IsSuccess = true,
                Message = "clave recuperada" //Resource.RecoverPasswordMessage
            });
        }

    }
}
