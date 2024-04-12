namespace infrastructure.datamodels;

public class CurrencyModel
{
    public DateTime Date { get; set; }
    public string Source { get; set; }
    public string Target { get; set; }
    public decimal Value { get; set; }
    public decimal Result { get; set; } 
    public bool Testing { get; set; }
}