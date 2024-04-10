namespace infrastructure.datamodels;

public class CurrencyModel
{
    public DateTime date { get; set; }
    public string source { get; set; }
    public string target { get; set; }
    public decimal value { get; set; }
    public decimal result { get; set; } 
}