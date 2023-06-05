using FefuHobbies.Domain.Entities;

namespace FefuHobbies.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Card> Cards { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public string keyWords {get; set;}
    }
}
