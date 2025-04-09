using LinkShortener.DAL.Data;
using LinkShortener.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.DAL.Repositories
{
    public class LinkRepository : ILinkRepository
    {
        private readonly LinksDbContext _context;

        public LinkRepository(LinksDbContext context)
        {
            _context = context;
        }

        public async Task<LinkInfo> CreateAsync(LinkInfo link)
        {
            if (link == null || !Uri.IsWellFormedUriString(link.LongUrl, UriKind.Absolute) || string.IsNullOrEmpty(link.Id))
                throw new InvalidDataException();

            _context.Links.Add(link);

            await _context.SaveChangesAsync();

            return link;
        }

        public async Task<LinkInfo> GetLinkInfoAsync(string id)
        {
            var linkInfo = await _context.Links.FirstOrDefaultAsync((l) => l.Id == id);

            return linkInfo;
        }

        public void SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
