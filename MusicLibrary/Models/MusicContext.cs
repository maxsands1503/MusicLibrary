using Microsoft.EntityFrameworkCore; namespace MusicLibrary.Models {
	public class MusicContext : DbContext
	{
		public MusicContext(DbContextOptions<MusicContext> options)
			: base(options)
		{

		}

		public DbSet<AlbumModel> AlbumModel { get; set; }
		public DbSet<ArtistModel> ArtistModel { get; set; }
		public DbSet<SongModel> SongModel { get; set; }
	} }