using ATM;

namespace AtmTest;

public class AtmServiceTest
{
    // Instansiera en atmservice, ett kort, ett konto i konstruktorn
    AtmService atmService;
    Account account;
    Card card;
    public AtmServiceTest() 
    {
        atmService = new AtmService();
        account = new Account(5000);
        card = new Card("1234-56789", "1234", account);
    }
    // EnterPin GetBalance Deposit Withdraw EnsureAuthenticated
    // Testmetod för att ta emot och läsa kort 
    [Fact]
    public void InsertCardTest()
    {
        // Har jag rätt starttillstånd?
        Assert.False(atmService.HasCardInserted);
        // test
        atmService.InsertCard(card);
        Assert.True(atmService.HasCardInserted);
    }

    // Testmetod för att mata ut kort
    [Fact]
    public void EjectCardTest()
    {
        // Kolla tillstånd
        Assert.False(atmService.HasCardInserted); // Finns eg om kortfacket är tomt
        // Ange starttillstånd
        atmService.InsertCard(card);
        // Testa
        atmService.EjectCard(); // Anropar metoden
        Assert.False(atmService.HasCardInserted); // Kollar att kortet är ute
    }

    [Fact]
    public void EnsureAuthenticatedTest()
    {
        // Ange starttillstånd
        atmService.InsertCard(card);
        // ...och kontrollera
        Assert.True(atmService.HasCardInserted);
        // Testa
        Assert.False(atmService.IsAuthenticated); // Ska inte vara autentiserad innan pin-kod matas in
    }

    [Theory]
    [InlineData("4321")] // Ogiltig pin-kod - ska ge false G
    [InlineData("1234")] // Giltig pin-kod - ska ge true G
    public void EnterPinTest(string pin)
    {
        // Anger starttillstånd
        atmService.InsertCard(card); 
        // Testa innan pin-kod matas in
        Assert.False(atmService.IsAuthenticated);
        // Testa giltig pin-kod
        atmService.EnterPin(pin);
        Assert.Equal(pin == "1234", atmService.IsAuthenticated); // Här borde en kunna automatisera testet så att det hämtar pin för olika konton och kort? Eller är det onödigt?
    }

    // Hjälpmetod för test nedan
    private void SetUpForWithdrawalTest(int balance)
    {
        account = new Account(balance);
        card = new Card("1234-56789", "1234", account);
        atmService.InsertCard(card);
        atmService.EnterPin("1234");
    }

    [Theory]
    // Lagt till bool för att test om uttaget kunde göras eller inte
    [InlineData(1000, 375, false)] // inte hundratalsbelopp - ska inte funka - ska ge false X
    [InlineData(4000, 5000, false)] // överskrider saldo - ska inte funka - ska ge false X
    [InlineData(0, 100, false)] // överskrider saldo - ska inte funka - ska ge false X
    [InlineData(1000, -1000, false)] // negativt uttag - ska inte funka - ska ge false G!
    [InlineData(-1000, 1000, false)] // negativt saldo - ska inte funka - ska ge false X
    [InlineData(5000, 4000, true)] // giltigt uttag - ska funka - ska ge true G
    [InlineData(5000, 400, true)] // giltigt uttag - ska funka - ska ge true G
    [InlineData(1000, 0, false)] // nolluttag - ska inte funka - ska ge false X
    public void WithdrawSuccessTest(int balance, int withdrawal, bool expectedSuccess)
    {
        SetUpForWithdrawalTest(balance); // Anropar hjälpmetod
        bool result = atmService.Withdraw(withdrawal);
        Assert.Equal(expectedSuccess, result);  // Kollar om uttaget lyckades/misslyckades som förväntat
        Assert.Equal(atmService.GetBalance(), atmService.GetBalance()); // Här kommer förväntade (även felaktiga) indata att provas mot programmets
    }

    [Theory]
    [InlineData(1000, 375, 1000)] // Ogiltgi inmatning - Inget uttag görs = saldo oförändrat
    [InlineData(5000, 4000, 1000)] // Giltigt uttag - Nytt saldo 1000 
    [InlineData(4000, 5000, 4000)] // Ogiltigt uttag - Inget uttag görs = saldo oförändrat
    [InlineData(0, 100, 0)] // Ogiltigt uttag - Inget uttag görs = saldo oförändrat
    [InlineData(1000, 0, 1000)] // Ogiltigt uttag - Inget uttag görs = saldo oförändrat
    [InlineData(1000, 1000, 0)] // Giltigt uttag - Nytt saldo 0
    [InlineData(-1000, 1000, -1000)] // Ogiltigt saldo - Inget uttag görs = saldo oförändrat
    public void WithdrawAndNewBalanceTest(int balance, int withdrawal, int newBalance)
    {
        SetUpForWithdrawalTest(balance); // Anropar hjälpmetod
        atmService.Withdraw(withdrawal);
        Assert.Equal(newBalance, atmService.GetBalance());
    }
}
