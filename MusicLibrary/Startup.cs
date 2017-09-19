using Microsoft.AspNetCore.Builder; using Microsoft.EntityFrameworkCore; using Microsoft.Extensions.DependencyInjection; using MusicLibrary.Models;  namespace MusicLibrary {
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<MusicContext>(x => x.UseInMemoryDatabase("Library"));
			services.AddMvc();
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseMvc();
		}
	} }  
