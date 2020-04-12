using Expenses.Common.Models;
using System.Threading.Tasks;

namespace Expenses.Common.Services
{
    public interface IApiService
    {
        Task<Response> AddTtrip(string urlBase, string servicePrefix, string controller, CreateTripRequest request, string token);

        Task<Response> GetTrips<T>(string urlBase, string servicePrefix, string controller, TripRequest request,string token);

        Task<Response> Login(string urlBase, string servicePrefix, string controller, LoginRequest request);
    }
}
