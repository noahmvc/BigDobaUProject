using BigDobaUProject.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Web;
using System.IO;
using OfficeOpenXml;
using BigDobaUProject.Data;


namespace BigDobaUProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DobaDbContext _db;

        public HomeController(ILogger<HomeController> logger, DobaDbContext db)
        {
            _db = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(); 
            
        }

        [HttpPost]
        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                await ImportToDatabase(path);
            }
            return RedirectToAction("Index");
        }


        private async Task ImportToDatabase(string path)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Assume you're using EPPlus or a similar library to read Excel files
            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Assuming the first worksheet
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    var music = new Music
                    {
                        Count = int.Parse(worksheet.Cells[row, 1].Value?.ToString().Trim() ?? "0"),
                        Artist = worksheet.Cells[row, 2].Value?.ToString().Trim() ?? "Unknown Artist",
                        Title = worksheet.Cells[row, 3].Value?.ToString().Trim()  // It's okay if this is null
                    };

                    
                    
                        _db.Musics.Add(music);
                        _db.SaveChanges();
                    
                }
            }
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
