﻿

var msft = new Stock() { Symbol = "MSFT" };

var sarah = new User() { Name = "Sarah" };
var bob = new User() { Name = "Bob" };

msft.PriceChanged += sarah.PriceListener;
msft.PriceChanged += bob.PriceListener;

msft.Price = 350m;
// msft.NotifyPriceChange();

msft.PriceChanged -= sarah.PriceListener;

msft.Price = 500m;
// msft.NotifyPriceChange();

public class User
{
    public string Name { get; set; }

    public void PriceListener(Stock stock)
    {
        Console.WriteLine($"{Name} - Notification : Stock : {stock.Symbol} Price: {stock.Price:C2}");
    }
}


public class Stock
{
    public string Symbol { get; set; }

    decimal _price;
    public decimal Price
    {
        get
        {
            return _price;
        }
        set
        {
            _price = value;
            NotifyPriceChange();
        }
    }

    public event Action<Stock> PriceChanged;


    void NotifyPriceChange()
    {
        PriceChanged?.Invoke(this);
    }
}