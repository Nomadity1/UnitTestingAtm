using ATM;

namespace AtmTest
{
    public class AccountTest
    {
        // Börja med att instansiera ett konto som kan användas i testerna
        private Account account = new Account(5000); // Starta med 5000 på kontot

        // Testmetod för att hämta saldo
        [Fact]
        public void GetBalanceTest()
        {
            account.GetBalance(); // Hämta startbalansen 
            Assert.Equal(5000, account.GetBalance()); // Kontrollera att GetBalance returnerar rätt värde (5000 = startbalansen)
        }

        // Testmetod för insättning
        [Fact]
        public void DepositTest()
        {
            account.Deposit(5000); // Ange insättningsbelopp
            Assert.Equal(10000, account.GetBalance()); // Kontrollera att GetBalance returnerar rätt värde (10000 = 5000 start + 5000 insättning)
        }

        // Testmetod för uttag
        [Fact]
        public void WithdrawTest()
        {
            account.Withdraw(5000); // Ange uttagsbelopp
            Assert.Equal(0, account.GetBalance()); // Kontrollera att GetBalance returnerar rätt värde (0 = 5000 start - 5000 uttag)
        }

        // Testmetod för att kontrollera att överuttag inte kan göras
        [Fact]
        public void OverdraftTest()
        {
            account.Withdraw(6000); // Anger uttagsbelopp som överstiger saldo
            Assert.Equal(5000, account.GetBalance()); // Kontrollera att saldo inte förändras
        }
    }
    // ALLA TESTMETODERNA BLEV GODKÄNDA U FÖRUTSÄTTNING ATT UTTAG INTE ÖVERSTEG SALDO (se sista metoden)
}
