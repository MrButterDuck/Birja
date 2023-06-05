using FefuHobbies.Domain.Entities;
using FefuHobbies.Domain.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FefuHobbies.Domain.Repositories.EntityFramework
{
	public class EFCardRepository: ICardRepositoriy
	{
		private readonly AppDbContex context;
		public EFCardRepository(AppDbContex context)
		{
			this.context = context;
		}

		public IQueryable<Card> GetCards()
		{
			return context.Cards;
		}

		public Card GetCardById(ulong id)
		{
			return context.Cards.FirstOrDefault(X => X.Id == id);
		}

		public void SaveCard(Card card)
		{
			if (card.Id == default)
				context.Entry(card).State = EntityState.Added;
			else
				context.Entry(card).State = EntityState.Modified;
			context.SaveChanges();
		}

		public void DeleteCard(ulong id)
		{
			context.Cards.Remove(new Card() { Id = id });
			context.SaveChanges();
		}

		public IQueryable<Card> FindCards(string keyWords, bool published = true)
		{
			if (keyWords == null) keyWords = " ";
			List<string> keys = keyWords.Split().ToList();
            IQueryable < Card > res = context.Cards.Where(x => x.Name.Contains(keys.First()) || x.Description.Contains(keys.First()));
            foreach (string key in keys)
			{
				res = res.Where(x => x.Name.Contains(keys.First()) || x.Description.Contains(keys.First()));
			}
			return res;
        }
		/*
        public IQueryable<Card> Last()
		{
			List<Card> r = context.Cards.AsEnumerable().OrderBy(x => x.Id).Where(x => x.IsPublished == true).ToList();
            return r.AsQueryable();
		}
        public IQueryable<Card> ByType(string type)
        {
            return context.Cards.Where(x => x.Type == type).Where(x => x.IsPublished == true);
        }
        public IQueryable<Card> excludeType(string type)
        {
            return context.Cards.Where(x => x.Type != type).Where(x => x.IsPublished == true);
        }*/

        //public List<PeopleList> getEmails(ulong id)
        //{
        //	return (List<PeopleList>)context.Cards.Where(x => x.Id == id).Select(y => y.people);
        //}

        //public void SaveEmail(PeopleList list)
        //{
        //    if (list.Id == default)
        //context.Entry(list).State = EntityState.Added;
        //   else
        //context.Entry(list).State = EntityState.Modified;
        //context.SaveChanges();
        //}

    }
}
