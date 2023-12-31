﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blog.Web.Models.Configurations;
using Microsoft.Extensions.Configuration;
using RESTFulSense.Clients;

namespace Blog.Web.Brokers.Apis
{
    public partial class ApiBroker : IApiBroker
    {
        private readonly HttpClient httpClient;
        private readonly IRESTFulApiFactoryClient apiClient;

        public ApiBroker(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.apiClient = GetApiClient(configuration);
        }

        private async ValueTask<T> PostAsync<T>(string realtiveUrl, T content) =>
            await this.apiClient.PostContentAsync<T>(realtiveUrl, content);

        private async ValueTask<T> GetAsync<T>(string realtiveUrl) =>
            await this.apiClient.GetContentAsync<T>(realtiveUrl);

        private async ValueTask<T> PutAsync<T>(string relativeUrl, T content) =>
            await this.apiClient.PutContentAsync<T>(relativeUrl, content);

        private async ValueTask<T> DeleteAsync<T>(string relativeUrl) =>
            await this.apiClient.DeleteContentAsync<T>(relativeUrl);

        private IRESTFulApiFactoryClient GetApiClient(IConfiguration configuration)
        {
            LocalConfigurations localConfigurations =
                configuration.Get<LocalConfigurations>();

            string apiUrl = localConfigurations.ApiConfigurations.Url;
            this.httpClient.BaseAddress = new Uri(apiUrl);

            return new RESTFulApiFactoryClient(this.httpClient);
        }
    }
}
