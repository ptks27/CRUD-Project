using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace project.Models
{
    public class product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Brand { get; set; } = "";
        public string Category { get; set; } = "";
        public float Price { get; set; }
        public string Description { get; set; } = "";
        public string ImageFileName { get; set; } = "";

        public DateTime CreateAt { get; set; }
        public int Stock { get; set; }
    }
}
