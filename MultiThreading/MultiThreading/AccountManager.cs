namespace MultiThreading;

internal class AccountManager
{
    public AccountManager(Account fromAccount, Account toAccount, decimal amount)
    {
        FromAccount = fromAccount;
        ToAccount = toAccount;
        Amount = amount;
    }

    public Account FromAccount { get; set; }
    public Account ToAccount { get; set; }
    public decimal Amount { get; set; }

    public void FundTransfer()
    {
        lock(FromAccount)
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} ishlayapti");
            Console.WriteLine($"{FromAccount.Name} is waiting {ToAccount.Name}");

            lock (ToAccount)
            {
                FromAccount.Balance -= Amount;
                ToAccount.Balance += Amount;
            }
        }
    }
}
