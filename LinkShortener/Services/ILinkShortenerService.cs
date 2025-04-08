using LinkShortener.Domain.Models;

namespace LinkShortener.Services;
public interface ILinkShortenerService
{
    string GenerateShortId();
    Task<string> CreateShortUrl(string originalUrl, string scheme, string host);
    Task<string> GetLongUrlAsync(string id);
    Task<LinkInfo> GetLinkInfoAsync(string id);
    void IncrementClick(LinkInfo linkInfo);
}
