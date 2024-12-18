using Microsoft.AspNetCore.Mvc;
using Shortly.API.Models.Url;
using Shortly.Application.Features.V1.Urls.Dependencies;
using Shortly.Contract.Dependencies.Services;

namespace Shortly.API.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IUrlServices _services;

        private readonly ILoggerManager _logger;

        public UrlController(IUrlServices service, ILoggerManager logger) 
        { 
            _services = service;
            _logger = logger;
        }

        [HttpPost]
        public async Task<string> CreateShortUrlAsync(CreateShortenUrlModel model)
        {
            _logger.LogInfo($"Start shortening url {model.OriginalUrl}");

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Model invalid");
                throw new ArgumentException();
            }

            var result = await _services.CreateShortenUrlAsync(model.OriginalUrl);

            return result;
        }
    }
}
