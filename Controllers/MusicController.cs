using BigDobaUProject.Data;
using BigDobaUProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BigDobaUProject.Controllers
{
    public class MusicController : Controller
    {
        private DobaDbContext _db;
        public MusicController(DobaDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(string search)
        {
            var musicList = string.IsNullOrEmpty(search) ?
                _db.Musics.ToList() :
                _db.Musics.Where(m => m.Title.Contains(search) || m.Artist.Contains(search)).ToList();

            return View(musicList);
        }

    }
}
