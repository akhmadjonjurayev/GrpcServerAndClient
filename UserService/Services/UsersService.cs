using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
namespace UserService
{
    public class UsersService : Users.UsersBase
    {
        private readonly ILogger<UsersService> _logger;
        public UsersService(ILogger<UsersService> logger)
        {
            _logger = logger;
        }
        List<UserResponse> GetUsersFromDb(int companyId)
        {
            return new List<UserResponse>
            {
                new UserResponse() { FirstName = "first1", LastName = "last1", Address = "adres1", Email = "email1" },
                new UserResponse() { FirstName = "first2", LastName = "last2", Address = "adres2", Email = "email2" },
                new UserResponse() { FirstName = "first3", LastName = "last3", Address = "adres3", Email = "email3" },
                new UserResponse() { FirstName = "first4", LastName = "last4", Address = "adres4", Email = "email4" },
                new UserResponse() { FirstName = "first5", LastName = "last5", Address = "adres5", Email = "email5" }
            };
        }
        public override async Task GetUsers(UserRequest request,IServerStreamWriter<UserResponse> responseStream, ServerCallContext context)
        {
            var users = GetUsersFromDb(request.CompanyId);
            foreach(UserResponse user in users)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(user);
            }
        }
    }
}
