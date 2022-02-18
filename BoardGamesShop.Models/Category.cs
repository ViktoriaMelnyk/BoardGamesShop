using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BoardGames.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [DisplayName("Kolejność wyświetlania")]
        [Range(1,100, ErrorMessage = "Kolejność wyświetlania musi się mieścić mędzy 1 a 100.")]
        public int DisplayOrder { get; set; }
        public DateTime CreatedDate { get; set; }= DateTime.Now;
    }
}
