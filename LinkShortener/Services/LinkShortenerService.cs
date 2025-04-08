using LinkShortener.DAL.Repositories;
using LinkShortener.Domain.Models;

namespace LinkShortener.Services
{
    public class LinkShortenerService : ILinkShortenerService
    {
        private readonly ILinkRepository _repository;

        public LinkShortenerService(ILinkRepository repository) 
        {
            _repository = repository;
        }

        public async Task<string> CreateShortUrl(string originalUrl, string scheme, string host)
        {
            var id = GenerateShortId();      

            while (await _repository.GetLinkInfoAsync(id) != null)
                id = GenerateShortId();

            var linkInfo = new LinkInfo(id, originalUrl);

            await _repository.CreateAsync(linkInfo);

            var shortUrl = $"{scheme}://{host}/{id}";

            return shortUrl;
        }

        public string GenerateShortId() => Guid.NewGuid().ToString("N")[..6];     

        public async Task<string> GetLongUrlAsync(string shortId)
        {
            var linkInfo = await _repository.GetLinkInfoAsync(shortId);

            return linkInfo?.LongUrl;
        }

        public async Task<LinkInfo> GetLinkInfoAsync(string id)
        {
            try
            {
                var linkInfo = await _repository.GetLinkInfoAsync(id);

                return linkInfo;
            }
            catch(Exception)
            {
                return null;        
            }
        }

        public void IncrementClick(LinkInfo linkInfo)
        {
            linkInfo.IncrementClick();

            _repository.SaveAsync();
        }
    }
}
