using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using RealEstateWebApplication.Models;

namespace RealEstateWebApplication.Controllers
{
    // Controller for generating chart data related to real estate properties.
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly RealEstateDbContext _context;

        // Controller for generating chart data related to real estate properties.
        public ChartController(RealEstateDbContext context)
        {
            _context = context;
        }

        [HttpGet("TypesData")]
        // Retrieves data for generating a chart representing the number of real estate properties by type.
        public JsonResult ModelsData()
        {
            var types = _context.Types.ToList();
            var realties = _context.Realties;

            List<object> data = new()
            {
                new object[] { "Тип нерухомості", "Кількість" }
            };
            foreach (var type in types)
            {
                // Iterate over each real estate type and count the number of properties for each type
                data.Add(new object[] { type.Name, realties.Where(realty => realty.TypeId == type.Id).ToList().Count });
            }
            return new JsonResult(data);
        }
        [HttpGet("RoomsData")]

        // Retrieves data for generating a chart representing the number of real estate properties by room count.
        public JsonResult RoomsData()
        {
            var realties = _context.Realties.ToList();

            HashSet<int> rooms = new();

            // Extract the distinct room counts from the real estate properties
            realties.ForEach(realty =>
            {
                rooms.Add(realty.RoomsNumber);
            });

            var roomCounts = rooms.Select(room =>
                new
                {
                    Room = room,
                    Count = realties.Count(realty => realty.RoomsNumber == room)
                }
            );

            List<object> data = new()
            {
                new object[] { "Кількість кімнат", "Кількість об'єктів" }
            };

            // Iterate over each distinct room count and count the number of properties for each room count
            foreach (var room in roomCounts)
            {
                data.Add(new object[] { room.Room, room.Count});
            }

            return new JsonResult(data);
        }
    }
}
