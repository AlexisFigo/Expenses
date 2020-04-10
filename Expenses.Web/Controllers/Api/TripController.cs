
using Expenses.Common.Model;
using Expenses.Web.Data;
using Expenses.Web.Data.Entities;
using Expenses.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Soccer.Common.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Expenses.Web.Controllers.Api
{
    [Route("api/[Controller]")]
    public class TripController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly ImageHelper _imageHelper;
        private readonly IUserHelper _userHelper;

        public TripController(
            DataContext dataContext)
        {
            _dataContext = dataContext;
            //_imageHelper = imageHelper;
            //_userHelper = userHelper;
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("GetTrips")]
        public async Task<IActionResult> GetTrips([FromBody] TripRequest modelRequest)
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

            CultureInfo cultureInfo = new CultureInfo(modelRequest.CultureInfo);
            //Resource.Culture = cultureInfo;

            List<TripsEntity> trips = await _dataContext.Trips
                .Include(t => t.User)
                .Include(t => t.City)
                .Include(t => t.TripDetails)
                .ThenInclude(td => td.ExpensesType)
                .Where(t => t.User.Id == modelRequest.Id)
                .ToListAsync();

            return Ok(trips);
        }
    }
}
