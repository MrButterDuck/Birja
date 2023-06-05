using System.ComponentModel.DataAnnotations;

namespace FefuHobbies.Domain.Entities
{
    public class PeopleList
    {
        public PeopleList( string email, uint id = default)
        {
            Email = email;
            Id = id;
        }

        [System.ComponentModel.DataAnnotations.Required]
        public uint Id { get; set; }
        [Display(Name = "Список участников")]
        public string Email { get; set; }

    }
}
