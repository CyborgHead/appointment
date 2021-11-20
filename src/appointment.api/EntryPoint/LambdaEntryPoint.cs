using System;
using Amazon.Lambda.AspNetCoreServer;
using Amazon.Lambda.Core;
using Microsoft.AspNetCore.Hosting;

namespace appointment.api.EntryPoint
{
    public class LambdaEntryPoint : APIGatewayHttpApiV2ProxyFunction
    {
        protected override void Init(IWebHostBuilder builder)
        {
            try
            {
                LambdaLogger.Log("Lambda entry point starts...");

                builder
                    .UseStartup<Startup>();
            }
            catch (Exception ex)
            {
                LambdaLogger.Log(ex.Message);

            }
        }
    }
}
