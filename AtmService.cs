namespace ATM;

public class AtmService
{
    private Card? _currentCard;
    private bool _isAuthenticated;

    public bool HasCardInserted => _currentCard != null;
    public bool IsAuthenticated => _isAuthenticated;
    
    //public int NewBalance { get; set; } // Det är inte ATM-Service som ska hålla koll på saldo
    //public AtmService(int Balance)
    //{
    //    NewBalance = Balance;
    //}

    public void InsertCard(Card card)
    {
        _currentCard = card;
        _isAuthenticated = false; // Måste ange pin för att autentisera
    }

    public void EjectCard()
    {
        _currentCard = null;
        _isAuthenticated = false; // Ska absolut inte vara autentiserad när kortet är ute
    }

    public bool EnterPin(string pinCode)
    {
        //  Kolla först om kort finns
        if (_currentCard == null)
        {
            return false;
        }
        // Anropar metod för autentisering
        _isAuthenticated = _currentCard.MatchesPin(pinCode);
        // Anger om autentiseringen lyckades eller inte
        return _isAuthenticated; // Returnerar true eller false 
    }

    public int GetBalance()
    {
        // Säkerställer att autentiering skett
        EnsureAuthenticated();
        // Returnerar saldot
        return _currentCard!.Account.GetBalance();
    }

    public bool Withdraw(int amount)
    {
        EnsureAuthenticated(); // Tagit bort tilldelning av NewBalance -= amount;
        return _currentCard!.Account.Withdraw(amount);
    }
    
    public bool Deposit(int amount)
    {
        EnsureAuthenticated(); // Tagit bort tilldelning av NewBalance += amount;
        return _currentCard!.Account.Deposit(amount);
    }
    
    private void EnsureAuthenticated()
    {
        if (_currentCard == null || !_isAuthenticated)
        {
            throw new InvalidOperationException("Ingen autentiserad session.");
        }
    }
}