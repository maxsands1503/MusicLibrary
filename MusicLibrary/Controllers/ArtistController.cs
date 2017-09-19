using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MusicLibrary.Models;
using System.Linq;

namespace MusicLibrary.Controllers
{
	[Route("api/[controller]")]
	public class ArtistController : Controller
	{
		private readonly MusicContext _context;

		public ArtistController(MusicContext context)
		{
			_context = context;

			if (_context.ArtistModel.Count() == 0)
			{
				_context.ArtistModel.Add(new ArtistModel
				{					
					ArtistId = 1,
					Name = "Black Moth Super Rainbow",
					Year = 2012,
					ImageUrl = "https://www.google.com/url?sa=i&rct=j&q=&esrc=s&source=images&cd=&cad=rja&uact=8&ved=0ahUKEwjp1LmPzbHWAhWrw1QKHeLwBhAQjRwIBw&url=https%3A%2F%2Fwww.bbc.co.uk%2Fmusic%2Fartists%2F44e14e88-b17a-44d8-9e94-f3afa977239b&psig=AFQjCNHk7UwIljEqXQU79XdPvLSJFG1R9Q&ust=1505922616802360"
				});
				_context.SaveChanges();
			}
		}

		[HttpPost]
		public IActionResult CreateArtist([FromBody] ArtistModel artist)
		{
			if (artist == null)
			{
				return BadRequest();
			}

			_context.ArtistModel.Add(artist);
			_context.SaveChanges();

			return CreatedAtRoute("GetById", new { id = artist.ArtistId }, artist);

		}

		[HttpGet]
		public IEnumerable<ArtistModel> GetAll()
		{
			return _context.ArtistModel.ToList();
		}

		[HttpGet("{ArtistId}", Name = "GetArtistById")]
		public IActionResult GetById(int id)
		{
			var artist = _context.ArtistModel.FirstOrDefault(art => art.ArtistId == id);

			if (artist == null)
			{
				return NotFound();
			}
			return new ObjectResult(artist);
		}

        [HttpGet("{ArtistId}", Name = "GetAlbumbsByArtistId")]
		public IActionResult GetAlbumByArtistId(int id)
		{
			var albums = _context.AlbumModel.Where(album => album.ArtistId == id);

			if (albums == null)
			{
				return NotFound();
			}
			return new ObjectResult(albums);
		}
	}
}

