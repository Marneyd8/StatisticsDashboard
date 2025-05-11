namespace StatisticsDashboard.Models
{
    public class ClientStatisticsDto
    {
        public string ClientName { get; set; } = null!;
        public int ItemCount { get; set; }
        public double TotalValue { get; set; }
    }

}
