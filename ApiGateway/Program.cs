﻿using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ApiGateway
{
	public class Program
	{
		public static void Main(string[] args)
		{
			IWebHostBuilder builder = new WebHostBuilder();
			builder.ConfigureServices(s =>
			{
				s.AddSingleton(builder);
			});
			builder.UseKestrel()
				   .UseContentRoot(Directory.GetCurrentDirectory())
				   .UseStartup<Startup>()
				   .UseUrls("http://localhost:9000");

			var host = builder.Build();
			host.Run();
		}

		//public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
		//	WebHost.CreateDefaultBuilder(args)
		//		.UseStartup<Startup>();
	}
}
