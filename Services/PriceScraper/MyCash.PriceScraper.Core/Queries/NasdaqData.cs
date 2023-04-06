namespace MyCash.PriceScraper.Core.Queries;

public class Data
{
    public Headers headers { get; set; }
    public List<Row> rows { get; set; }
}

public class Headers
{
    public string symbol { get; set; }
    public string name { get; set; }
    public string lastsale { get; set; }
    public string netchange { get; set; }
    public string pctchange { get; set; }
    public string marketCap { get; set; }
    public string country { get; set; }
    public string ipoyear { get; set; }
    public string volume { get; set; } 
    public string sector { get; set; }
    public string industry { get; set; }
    public string url { get; set; }
}

public class Root
{
    public Data data { get; set; }
    public object message { get; set; }
    public Status status { get; set; }
}

public class Row
{
    public string symbol { get; set; }
    public string name { get; set; }
    public string lastsale { get; set; }
    public string netchange { get; set; }
    public string pctchange { get; set; }
    public string volume { get; set; }
    public string marketCap { get; set; }
    public string country { get; set; }
    public string ipoyear { get; set; }
    public string industry { get; set; }
    public string sector { get; set; }
    public string url { get; set; }
}

public class Status
{
    public int rCode { get; set; }
    public object bCodeMessage { get; set; }
    public object developerMessage { get; set; }
}

