using System.ComponentModel.DataAnnotations;

namespace StatisticsDashboard.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public List<ItemClient>? ItemClients { get; set; }
    }
}
