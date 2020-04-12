using Expenses.Common.Models;
using System.Threading.Tasks;

namespace Expenses.Common.Services
{
    public interface IApiService
    {
        Task<Response> AddTtripDetail(string urlBase, string servicePrefix, string controller, AddDetailsRequest request, string token);

        Task<Response> GetComboBox<T>(string urlBase, string servicePrefix, string controller);

        Task<Response> AddTtrip(string urlBase, string servicePrefix, string controller, CreateTripRequest request, string token);

        Task<Response> GetTrips<T>(string urlBase, string servicePrefix, string controller, TripRequest request,string token);

        Task<Response> Login(string urlBase, string servicePrefix, string controller, LoginRequest request);
    }
}
