using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace FefuHobbies.Domain.Entities
{
	public class Card
	{
        public Card() => IsPublished = false;

		[System.ComponentModel.DataAnnotations.Required]
		public ulong Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage ="Укажите название")]
        [Display(Name = "Название (заголовок)")]
		public string Name { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; } = String.Empty;
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Выберите вид мероприятия")]
        [Display(Name = "Вид Мероприятия")]
        public string Type { get; set; }
        [Display(Name = "Доступ")]
        public int access { get; set; } = 0;
        [Display(Name = "Дата")]
        [DataType(DataType.Date)]
        public string Date { get; set; }
        [Display(Name = "Начало мероприятия")]
        [DataType(DataType.Time)]
        public string startTime { get; set; }
        [Display(Name = "Начало мероприятия")]
        [DataType(DataType.Time)]
        public string endTime { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Укажите место")]
        [Display(Name = "Место")]
        public string Location { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Выберите тег")]
        [Display(Name = "Тег")]
        public string Tag { get; set; }
        [Display(Name = "Обложка")]
        public string ImagePath { get; set; }
        [Display(Name = "Принято")]
        public bool IsPublished { get; set; }
        //[Display(Name = "Колличество участников")]
        //public uint Count { get; set; }
        //[Display(Name = "Участники")]
        //public List<PeopleList> people { get; set; }
        

    }
}
