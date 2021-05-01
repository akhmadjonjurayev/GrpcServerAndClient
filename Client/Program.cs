using Grpc.Net.Client;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using UserService;
namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Calling a Grpc server");
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var httpClient = new HttpClient(httpClientHandler);
            var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { HttpClient = httpClient });
            var client = new Users.UsersClient(channel);
            try
            {
                UserRequest request = new UserRequest() { CompanyId = 1 };
                using(var call=client.GetUsers(request))
                {
                    while(await call.ResponseStream.MoveNext(CancellationToken.None))
                    {
                        var currentUser = call.ResponseStream.Current;
                        Console.WriteLine("{0}  {1}  is being fetch from the service", currentUser.FirstName, currentUser.LastName);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}
