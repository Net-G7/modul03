namespace Asynchronous;

internal class Bankaccount
{
    public decimal Number { get; set; }
    public string Name { get; set; }

    public static List<Bankaccount> GenerateBankAccounts()
    {
        return new List<Bankaccount>
        {
            new Bankaccount {Number = 1314255246, Name = "Bekzod"},
            new Bankaccount {Number = 764644334, Name = "Bobur"},
            new Bankaccount {Number = 946456313, Name = "Ozodbek"},
            new Bankaccount {Number = 0973287, Name = "Farrux"},
            new Bankaccount {Number = 827344432, Name = "Bilol"},
            new Bankaccount {Number = 43214455, Name = "Anvar"},
            new Bankaccount {Number = 8565642654, Name = "Sobir"},
        };
    }
}
