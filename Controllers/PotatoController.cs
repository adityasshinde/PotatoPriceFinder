using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PotatoController : Controller
    {
        private const string PotatoDataUrl = "http://eportal.aa-engineers.com/assessment/potatoQ12024.csv";
        private readonly HttpClient _httpClient;

        public PotatoController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? minAvailablePounds = null)
        {
            var potatoes = await DownloadAndProcessPotatoData();
            if (minAvailablePounds.HasValue)
            {
                potatoes = potatoes.Where(p => p.QuantityAvailable >= minAvailablePounds).ToList();
            }
            // Sort potatoes by price per pound in ascending order
            potatoes = potatoes.OrderBy(p => p.PricePerPound).ToList();

            // Take the top 3 or fewer cheapest potato sellers
            potatoes = potatoes.Take(3).ToList();

            return View("Index", potatoes);
        }

        private async Task<List<Potato>> DownloadAndProcessPotatoData()
        {
            var response = await _httpClient.GetAsync(PotatoDataUrl);
            response.EnsureSuccessStatusCode(); // Throw exception for non-200 status codes

            var content = await response.Content.ReadAsStringAsync();
            var lines = content.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var potatoes = new List<Potato>();
            for (int i = 1; i < lines.Length; i++) // Skip header row
            {
                var data = lines[i].Split(',');

                try
                {
                    potatoes.Add(new Potato
                    {
                        SupplierName = data[0].Trim(),
                        UnitWeight = double.Parse(data[1].Trim(), CultureInfo.InvariantCulture),
                        UnitPrice = double.Parse(data[2].Trim(), CultureInfo.InvariantCulture),
                        QuantityAvailable = int.Parse(data[3].Trim()),
                    });
                }
                catch (FormatException)
                {
                    // Handle parsing errors (e.g., log the error or skip the row)
                }
            }

            return potatoes;
        }
    }
}
