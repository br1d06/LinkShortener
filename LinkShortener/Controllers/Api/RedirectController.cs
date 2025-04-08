using LinkShortener.Services;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.Controllers.Api;

[ApiController]
[Route("")]
public class RedirectController : ControllerBase
{
    private readonly ILinkShortenerService _linkShortenerService;

    public RedirectController(ILinkShortenerService linkShortenerService)
    {
        _linkShortenerService = linkShortenerService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> RedirectByShortUrl(string id)
    {
        try
        {
            var linkInfo = await _linkShortenerService.GetLinkInfoAsync(id);

            if (linkInfo == null || string.IsNullOrEmpty(linkInfo.LongUrl) || !Uri.IsWellFormedUriString(linkInfo.LongUrl, UriKind.Absolute))
                return NotFound("Ссылка не найдена");

            _linkShortenerService.IncrementClick(linkInfo);

            return Redirect(linkInfo.LongUrl);
        }
        catch (Exception)
        {
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
}

