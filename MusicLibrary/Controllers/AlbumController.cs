using System.Collections.Generic; using Microsoft.AspNetCore.Mvc; using MusicLibrary.Models; using System.Linq;  namespace MusicLibrary.Controllers {
	[Route("api/[controller]")]
	public class AlbumController : Controller
	{
		private readonly MusicContext _context;

		public AlbumController(MusicContext context)
		{
			_context = context;

			if (_context.AlbumModel.Count() == 0)
			{
				_context.AlbumModel.Add(new AlbumModel
				{
					AlbumId = 1,
					ArtistId = 1,
					AlbumName = "Cobra Juicy",
					Year = 2012,
					ImageUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/d/d9/BMSRCobraJuicy.jpg/220px-BMSRCobraJuicy.jpg"
				});
				_context.SaveChanges();
			}
		}

		[HttpPost]
		public IActionResult CreateAlbum([FromBody] AlbumModel album)
		{
			if (album == null)
			{
				return BadRequest();
			}

			_context.AlbumModel.Add(album);
			_context.SaveChanges();

			return CreatedAtRoute("GetById", new { id = album.AlbumId }, album);

		}

		[HttpGet]
		public IEnumerable<AlbumModel> GetAll()
		{
			return _context.AlbumModel.ToList();
		}

		[HttpGet("{AlbumId}", Name = "GetAlbumsById")]
		public IActionResult GetById(int id)
		{
			var albums = _context.AlbumModel.Where(albs => albs.ArtistId == id);

			if (albums == null)
			{
				return NotFound();
			}
			return new ObjectResult(albums);
		}
	} }  