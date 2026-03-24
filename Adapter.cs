namespace OnlineShop
{
    // Цель: привести цену к нужному формату
    public interface ICurrencyAdapter
    {
        string GetConvertedPrice(double usdPrice);
    }

    public class EuroAdapter : ICurrencyAdapter
    {
        public string GetConvertedPrice(double usdPrice) => $"€{Math.Round(usdPrice * 0.92, 2)}";
    }

    public class LeuAdapter : ICurrencyAdapter
    {
        public string GetConvertedPrice(double usdPrice) => $"{Math.Round(usdPrice * 18.0, 2)} MDL";
    }
}