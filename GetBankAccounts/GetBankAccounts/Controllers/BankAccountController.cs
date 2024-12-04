using GetBankAccounts.Models;
using Microsoft.AspNetCore.Mvc;

namespace GetBankAccounts.Controllers
{
    [Route("[controller]")]
    public class BankAccountController : Controller
    {
        private Random random = new Random();

        [HttpGet(Name = "GetBankAccounts")]
        public IEnumerable<BankAccount> Get()
        {
           //Thread.Sleep(100_000_000);
            return GenerateBankAccounts();
        }

        public static List<BankAccount> GenerateBankAccounts()
        {
            return new List<BankAccount>
        {
            new BankAccount {Number = 1314255246, Name = "Bekzod"},
            new BankAccount {Number = 764644334, Name = "Bobur"},
            new BankAccount {Number = 946456313, Name = "Ozodbek"},
            new BankAccount {Number = 0973287, Name = "Farrux"},
            new BankAccount {Number = 827344432, Name = "Bilol"},
            new BankAccount {Number = 43214455, Name = "Anvar"},
            new BankAccount {Number = 8565642654, Name = "Sobir"},
        };
        }

    }
}
