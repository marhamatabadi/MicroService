﻿using Microsoft.Extensions.Configuration;
using PlatformService.Dtos;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task SendPlatformToCommand(PlatformReadDto dto)
        {
            var httpContent = new StringContent(
               JsonSerializer.Serialize(dto),
               Encoding.UTF8,
               "application/json"
                );
            var url = $"{_configuration["CommandServiceUrl"]}/api/command/Platforms";
            var response = await _httpClient.PostAsync(url, httpContent);
            if (response.IsSuccessStatusCode)
                Console.WriteLine("Sync POST to CommandService was OK!");
            else
                Console.WriteLine("Sync POST to CommandService was NOT OK!");
        }
    }
}
