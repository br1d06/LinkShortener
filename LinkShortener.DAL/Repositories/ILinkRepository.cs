using LinkShortener.Domain.Models;

namespace LinkShortener.DAL.Repositories;

public interface ILinkRepository
{
    Task<LinkInfo> GetLinkInfoAsync(string shortId);
    Task<LinkInfo> CreateAsync(LinkInfo link);
    void SaveAsync();
}

