namespace StatisticsDashboard.Models
{
    public class ClientStatisticsViewModel
    {
        public string ClientName { get; set; } = null!;
        public int ItemCount { get; set; }
        public double TotalValue { get; set; }
    }

}
