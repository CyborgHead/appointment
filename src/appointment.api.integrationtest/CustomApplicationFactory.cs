#region namespaces

using System;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

#endregion namespaces

namespace appointment.api.integrationtest
{
	public class CustomApplicationFactory : WebApplicationFactory<Startup>
	{
		protected override IWebHostBuilder CreateWebHostBuilder()
		{
			return WebHost.CreateDefaultBuilder()
				.UseStartup<Startup>();
		}
	}
}
