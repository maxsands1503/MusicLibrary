using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MusicLibrary.Models;
using System.Linq;

namespace MusicLibrary.Controllers
{
    [Route("api/[controller]")]
    public class SongController : Controller
    {
        private readonly MusicContext _context;

        public SongController(MusicContext context)
        {
            _context = context;

            if (_context.SongModel.Count() == 0)
            {
                _context.SongModel.Add(new SongModel
                {
                    SongId = 1,
                    AlbumId = 1,
                    Name = "Windshield Smasher",

                });
                _context.SaveChanges();
            }
        }

        [HttpPost]
        public IActionResult CreateSong([FromBody] SongModel song)
        {
            if (song == null)
            {
                return BadRequest();
            }

            _context.SongModel.Add(song);
            _context.SaveChanges();

            return CreatedAtRoute("GetById", new { id = song.AlbumId }, song);

        }
        [HttpGet]
		public IEnumerable<SongModel> GetAll()
		{
			return _context.SongModel.ToList();
		}

		[HttpGet("{AlbumId}", Name = "GetSongsById")]
		public IActionResult GetById(int id)
		{
			var songs = _context.SongModel.Where(song => song.AlbumId == id);

			if (songs == null)
			{
				return NotFound();
			}
			return new ObjectResult(songs);
		}

	}
}