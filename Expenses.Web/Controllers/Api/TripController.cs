
using Expenses.Common.Models;
using Expenses.Web.Data;
using Expenses.Web.Data.Entities;
using Expenses.Web.Helpers;
using Expenses.Web.Resources;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IImageHelper _imageHelper;
        private readonly IUserHelper _userHelper;
        private readonly IConverterHelper _converterHelper;

        public TripController(
            DataContext dataContext,
            IConverterHelper converterHelper,
            IUserHelper userHelper,
            IImageHelper imageHelper)
        {
            _dataContext = dataContext;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
            _userHelper = userHelper;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("GetTrips")]
        public async Task<IActionResult> GetTrips([FromBody] TripRequest modelRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState
                );
            }

            CultureInfo cultureInfo = new CultureInfo(modelRequest.CultureInfo);
            Resource.Culture = cultureInfo;

            List<TripsEntity> trips = await _dataContext.Trips
                .Include(t => t.City)
                .Include(t => t.TripDetails)
                .ThenInclude(td => td.ExpensesType)
                .Where(t => t.User.Id == modelRequest.Id)
                .ToListAsync();

            return Ok(_converterHelper.ToTripResponse(trips));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("CreateTrip")]
        public async Task<IActionResult> CreateTrip([FromBody] CreateTripRequest modelRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState
                );
            }

            CultureInfo cultureInfo = new CultureInfo(modelRequest.CultureInfo);
            Resource.Culture = cultureInfo;

            UserEntity user = await _userHelper.GetUserAsync(new System.Guid(modelRequest.UserId));
            if (user == null)
            {
                return BadRequest(Resource.UserDoesntExists
                );
            }

            TripsEntity tripEntity = new TripsEntity()
            {
                StartDate = modelRequest.StartDate,
                EndDate = modelRequest.EndDate,
                Description = modelRequest.Description,
                City = await _dataContext.Cities.FindAsync(modelRequest.CityId),
                User = user
            };

            _dataContext.Trips.Add(tripEntity);

            await _dataContext.SaveChangesAsync();
            return Ok(Resource.TripCreated);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("AddDetails")]
        public async Task<IActionResult> AddDetails([FromBody] AddDetailsRequest modelRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState
                );
            }

            CultureInfo cultureInfo = new CultureInfo(modelRequest.CultureInfo);
            Resource.Culture = cultureInfo;

            TripsEntity trip = await _dataContext.Trips.FindAsync(int.Parse(modelRequest.TripId));
            if (trip == null)
            {
                return BadRequest(Resource.TripIdIncorrect
                );
            }

            string picturePath = string.Empty;

            if (modelRequest.VoucherPath != null && modelRequest.VoucherPath.Length > 0)
            {
                picturePath = _imageHelper.UploadImage(modelRequest.VoucherPath, "Vouchers");
            }

            TripDetailsEntity tripDetail = new TripDetailsEntity()
            {
                ExpensesType = await _dataContext.ExpensesTypes.FindAsync(int.Parse(modelRequest.ExpensesTypeId)),
                Date = modelRequest.Date,
                Cost = modelRequest.Cost,
                VoucherPath = picturePath,
                Trip = trip
            };

            _dataContext.TripDetails.Add(tripDetail);

            await _dataContext.SaveChangesAsync();
            return Ok(Resource.DetailAdded);
        }

        [HttpGet]
        [Route("GetExpenesesType")]
        public async Task<IActionResult> GetExpenesesType()
        {
            IEnumerable<ExpensesTypeEntity> expenses = _dataContext.ExpensesTypes.AsEnumerable();
            if (expenses == null)
            {
                return BadRequest(ModelState
                );
            }

            IEnumerable<ExpensesTypeResponse> tripDetail = expenses.Select(e => new ExpensesTypeResponse
            {
                Id = e.Id,
                Name = e.Name
            });

            return Ok(tripDetail);
        }

        [HttpGet]
        [Route("GetCountries")]
        public async Task<IActionResult> GetCountries()
        {

            List<CountriesEntity> countries = await _dataContext.Countries.Include(c => c.Cities).ToListAsync();
            if (countries == null)
            {
                return BadRequest(ModelState
                );
            }

            IEnumerable<CounrtriesResponse> contriesResponse = countries.Select(c => new CounrtriesResponse
            {
                Id = c.Id,
                Name = c.Name,
                Cities = c.Cities.Select(ci => new CityResponse
                {
                    Id = ci.Id,
                    Name = ci.Name
                }).ToList()
            });

            return Ok(contriesResponse);
        }
    }
}
