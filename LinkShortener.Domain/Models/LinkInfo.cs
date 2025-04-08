using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkShortener.Domain.Models;
public class LinkInfo
{
    public string Id { get; private set; }
    public string LongUrl { get; private set; }
    public DateTime DateCreated { get; }
    public int Clicks { get; private set; }

    public LinkInfo(string id, string longUrl)
    {
        Id = id;
        LongUrl = longUrl;
        DateCreated = DateTime.UtcNow;
    }

    public void IncrementClick() => Clicks++;
    
}
