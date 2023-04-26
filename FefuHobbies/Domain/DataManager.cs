using FefuHobbies.Domain.Repositories.Abstract;

namespace FefuHobbies.Domain
{
	public class DataManager
	{
		public ICardRepositoriy Cards { get; set; }

		public DataManager(ICardRepositoriy cardsRepository)
		{
			Cards = cardsRepository;
		}
	}
}
