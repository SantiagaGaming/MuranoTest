using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MuranoTest.Models
{
    public class Word
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
