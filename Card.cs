namespace ATM;

public class Card
{
    public string CardNumber { get; }
    public string PinCode { get; }
    public Account Account { get; } // Kortet har en referens till det konto som det är kopplat till

    public Card(string cardNumber, string pinCode, Account account)
    {
        CardNumber = cardNumber;
        PinCode = pinCode;
        Account = account;
    }

    // Metod för att kontrollera om angiven pin-koden matchar kortets pin-kod
    public bool MatchesPin(string pinCode)
    {
        return PinCode == pinCode; // Returnerar true eller false
    }
}