using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MuranoTest.Models
{
    public enum SiteState
    {
        Yandex,
        Google,
        Bing
    }
    public class UrlObject
    {
        [Key]
        public int Id { get; set; }
        public string UrlText { get; set; }
        public string Description { get; set; }
        public SiteState SiteState { get; set; }
        public int WordId { get; set; }
    }
}
