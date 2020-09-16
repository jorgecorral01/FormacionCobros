using System;
using System.IO;
using System.Net.Http;
using Charges.Controllers.Test.mocks;
using Cobros.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Charges.Controllers.Test {

    [SetUpFixture]
    public class TestFixture {
        public static IConfigurationRoot Configuration;
        public static HttpClient HttpClient;        

        [OneTimeSetUp]
        public void SetUp() {
            Configuration = PrepareConfiguration(new ConfigurationBuilder()).Build();
            var server = InitializeApiServer(Configuration["HostUri"]);
            InitializeClient(server, Configuration["HostUri"]);
        }

        private static IConfigurationBuilder PrepareConfiguration(IConfigurationBuilder configurationBuilder) {
            return configurationBuilder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);
        }

        private static TestServer InitializeApiServer(string baseAddress) {
            var webHostBuilder = new WebHostBuilder()
                .UseEnvironment("Test")
                .UseUrls(baseAddress)
                .ConfigureAppConfiguration((builderContext, configBuilder) => { PrepareConfiguration(configBuilder); })
                .ConfigureTestServices(services => {
                    //services.Configure<IdentityServerConfig>(config => {
                    //    config.Authority = identityServer.ServerUrl;
                    //    config.ApiName = identityServer.ApiName;
                    //    config.RequireHttpsMetadata =
                    //        Convert.ToBoolean(Configuration["IdentityServerConfig:RequireHttpsMetadata"]);
                    //});                    
                    //services.AddSingleton(PersistenceFactoryMock.Instance);
                    services.AddSingleton(ActionsFactoryMock.Instance);
                })
                .UseStartup<Startup>();

            return new TestServer(webHostBuilder);
        }

        private static void InitializeClient(TestServer testServer, string baseAddress) {
            HttpClient = CreateClient(testServer, baseAddress);
        }

        private static HttpClient CreateClient(TestServer server, string baseAddress) {
            var client = server.CreateClient();
            client.BaseAddress = new Uri(baseAddress);            
            return client;
        }
    }
}
