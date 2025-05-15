using System.ComponentModel.DataAnnotations;

namespace StatisticsDashboard.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public List<Item>? Items { get; set; }
    }
}
