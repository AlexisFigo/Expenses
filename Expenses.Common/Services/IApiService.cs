using Expenses.Common.Models;
using System.Threading.Tasks;

namespace Expenses.Common.Services
{
    public interface IApiService
    {
        Task<Response> PostAsync(string urlBase, string servicePrefix, string controller, object request, string token);

        Task<Response> GetComboBox<T>(string urlBase, string servicePrefix, string controller);

        Task<Response> GetTrips<T>(string urlBase, string servicePrefix, string controller, TripRequest request,string token);

        Task<Response> Login(string urlBase, string servicePrefix, string controller, LoginRequest request);
    }
}
