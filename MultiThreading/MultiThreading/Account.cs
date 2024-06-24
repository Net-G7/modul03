namespace MultiThreading;

internal class Account
{
    public int Id { get; set; }
    public decimal Balance { get; set; }

    public string Name { get; set; }    

    public void WithDraw(decimal amount)
    {
        this.Balance -= amount;
    }

    public void Deposit(decimal amount)
    {
        this.Balance += amount;
    }
}
