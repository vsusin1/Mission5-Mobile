using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace SicilyLinesAPI.Controllers
{
    [ApiController]
    [Route("api/client")]
    public class ClientController : ControllerBase
    {
        [HttpGet("login/{login}/{password}")]
        public Client? Login([FromRoute] string login, [FromRoute] string password)
        {
            Client? c = DataProvider.Clients.Where(c => c.Login == login).FirstOrDefault();

            Console.WriteLine($"Got login request: {login} {password}");

            if (c == null)
            {
                Console.WriteLine("Error: client doesn't exist");
                return null;
            }

            if (c.Password != password)
            {
                Console.WriteLine($"Error: the two passwords don't match ('{c.Password}' vs '{password}'");
                return null;
            }

            Console.WriteLine("Login attempt successful!");
            return c;
        }

        [HttpGet("{id}/bookings")]
        public List<Booking> GetClientBookings([FromRoute] int id)
        {
            Console.WriteLine($"Got booking list request for client #{id}");
            List<Booking> bookings = DataProvider.Bookings.Where(b => b.Client.Id == id).ToList();
            Console.WriteLine($"Matched {bookings.Count} result(s):");
            foreach (Booking b in bookings)
            {
                Console.WriteLine($" - {b.Crossing.Name}");
            }
            return bookings;

        }

        [HttpPut("{id}/editProfile")]
        public StatusCodeResult EditProfile([FromRoute] int id, [FromBody] Client content)
        {
            Console.WriteLine($"Got edit profile request with id={id} and client={content}");

            List<Client> matchingClients = DataProvider.Clients.Where(c => c != null && c.Id == id).ToList();

            if (matchingClients.Count != 1)
                return StatusCode(404);

            Client oldClient = matchingClients.First();

            if (oldClient.Password != content.Password)
            {
                oldClient.Password = content.Password;
            }

            if (oldClient.BirthDate != content.BirthDate)
            {
                oldClient.BirthDate = content.BirthDate;
            }

            DataProvider.Clients.ForEach(
                c =>
                {
                    if (c.Id == id)
                        c = oldClient;
                });
        
            DataProvider.UpdateClient(oldClient);
            return StatusCode(200);

        }
    }
}
