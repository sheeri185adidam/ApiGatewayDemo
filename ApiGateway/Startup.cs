using CacheManager.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;

namespace ApiGateway
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder();
			builder.SetBasePath(Environment.CurrentDirectory)
				   .AddJsonFile("configuration.json", optional: false, reloadOnChange: true)
				   .AddEnvironmentVariables();

			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; private set; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			Action<ConfigurationBuilderCachePart> settings = (x) =>
			{
				x.WithMicrosoftLogging(log =>
				{
					log.AddConsole(LogLevel.Debug);

				}).WithDictionaryHandle();
			};
			services.AddOcelot();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			await app.UseOcelot();
		}
	}
}
