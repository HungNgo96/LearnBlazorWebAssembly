using Shared.Model.Charts;

namespace Application.ChartDataProvider
{
    public static class DataManager
    {
        public static List<ChartModel> GetData()
        {
            var r = new Random();
            return new List<ChartModel>()
        {
            new ChartModel { Value = r.Next(1, 40), Label = "Wall Clock" },
            new ChartModel { Value = r.Next(1, 40), Label = "Fitted T-Shirt" },
            new ChartModel { Value = r.Next(1, 40), Label = "Tall Mug" },
            new ChartModel { Value = r.Next(1, 40), Label = "Pullover Hoodie" }
        };
        }
    }
}
