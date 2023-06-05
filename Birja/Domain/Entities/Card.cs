using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace FefuHobbies.Domain.Entities
{
	public class Card
	{
        //public Card() => IsPublished = false;

		[System.ComponentModel.DataAnnotations.Required]
		public ulong Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage ="Укажите название")]
        [Display(Name = "Название (заголовок)")]
		public string Name { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; } = String.Empty;
        [Display(Name = "Требования")]
        public string requirements { get; set; } = String.Empty;
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Выберите вид работы")]
        [Display(Name = "Вид работы")]
        public short Type { get; set; }
        [Display(Name = "Дата начала")]
        [DataType(DataType.Date)]
        public string StartDate { get; set; }
        [Display(Name = "Дата окончания")]
        [DataType(DataType.Date)]
        public string EndDate { get; set; }
        [Display(Name = "Оплата от")]
        public int LowPay { get; set; }
        [Display(Name = "Оплата до")]
        public int HighPay { get; set; }
        [Display(Name = "Удаленная Работа")]
        public int HomeJob { get; set; }
        [Display(Name = "Специализация1")]
        public string Prof1 { get; set; } = String.Empty;
        [Display(Name = "Специализация2")]
        public string Prof2 { get; set; } = String.Empty;
        [Display(Name = "Специализация3")]
        public string Prof3 { get; set; } = String.Empty;
        [Display(Name = "Навыки1")]
        public string Skills1 { get; set; } = String.Empty;
        [Display(Name = "Навыки2")]
        public string Skills2 { get; set; } = String.Empty;
        [Display(Name = "Навыки3")]
        public string Skills3 { get; set; } = String.Empty;
        [Display(Name = "Картинка")]
        public string ImagePath { get; set; } = String.Empty;



    }
}
