using FefuHobbies.Domain.Entities;

namespace FefuHobbies.Domain.Repositories.Abstract
{
	public interface ICardRepositoriy
	{
		IQueryable<Card> GetCards();
		Card GetCardById(ulong id);
		void SaveCard(Card card);
		void DeleteCard(ulong id);
		IQueryable<Card> FindCards(string keyWords, bool published = true);
		public IQueryable<Card> Last();
		public IQueryable<Card> ByType(string typet);
		public IQueryable<Card> excludeType(string type);
		//public List<PeopleList> getEmails(ulong id);
		//public void SaveEmail(PeopleList list);
    }
}
