using Expenses.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Expenses.Common.Services
{
    public class ApiService : IApiService
    {
        public Task<Response> Login(string urlBase, string servicePrefix, string controller, LoginRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
