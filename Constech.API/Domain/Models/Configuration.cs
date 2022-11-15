using System.Collections.ObjectModel;
using Constech.API.Domain.Enums;

namespace Constech.API.Domain.Models;

public class Configuration
{
    public int Id { get; set; }
    public Locale CurrentLocale { get; set; } = Locale.en_US;
    public ICollection<Locale> SupportedLocales { get; set; } = new Collection<Locale> { Locale.en_US, Locale.es_ES };
    public Theme Theme { get; set; } = Theme.Light;
    public bool AcceptCookies { get; set; } = false;
    public bool AcceptTerms { get; set; } = false;
    public Guid UserId { get; set; }
    public User User { get; set; }
}