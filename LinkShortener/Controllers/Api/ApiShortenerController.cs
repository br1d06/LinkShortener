using LinkShortener.Services;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class ApiShortenerController : ControllerBase
{
    private readonly ILinkShortenerService _linkShortenerService;

    public ApiShortenerController(ILinkShortenerService linkShortenerService)
    {
        _linkShortenerService = linkShortenerService;
    }
    [HttpPost]
    public async Task<IActionResult> CreateShortUrl([FromBody] string longUrl)
    {
        if (string.IsNullOrEmpty(longUrl) || !Uri.IsWellFormedUriString(longUrl, UriKind.Absolute))
            return BadRequest("Некорректный URl");

        var scheme = Request.Scheme;
        var host = Request.Host.Value;

        var shortUrl = await _linkShortenerService.CreateShortUrl(longUrl, scheme, host);

        return Ok(new { shortUrl });
    }
}

