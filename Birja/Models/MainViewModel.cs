using FefuHobbies.Domain.Entities;

namespace FefuHobbies.Models
{
    public class MainViewModel
    {
        public IQueryable<Card> last { get; set; }
        public IQueryable<Card> second { get; set; }
    }
}
