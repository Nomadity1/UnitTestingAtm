namespace ATM;

public class Account
{
    public int Balance { get; private set; } // Denna bör väl egentligen vara private readonly? 

    public Account(int initialBalance)
    {
        Balance = initialBalance; // Detta gör att vi hela tiden börjar om med 5000 kr på kontot i testerna
            // Hur ska detta se ut i kod för en riktig bank? Hur koda för ett mera dynamiskt (verkligt) saldo? Mot databas?!
    }

    public int GetBalance()
    {
        return Balance;
    }

    public bool Withdraw(int amount) // Lagt till villkor för uttag
    {
        if (amount < 0)
        {
            return false;
            throw new Exception("Uttag med minusbelopp kan inte genomföras.");
        }
        else if (amount == 0)
        {
            return false;
            throw new Exception("Uttag på 0 kronor kan inte genomföras.");
        }
        else if (Balance < amount)
        {
            return false;
            throw new Exception("Uttag som är större än saldot kan inte genomföras.");            
        }
        else if (amount % 100 != 0)
        {
            return false;
            throw new Exception("Uttag kan endast göras för jämnt 100-tal kronor.");
        }
        else Balance -= amount;
        return true;
    }

    public bool Deposit(int amount) // Lagt till villkor för insättning
    {
        if (amount <= 0)
        {
            return false;
            throw new Exception("Insättning måste vara ett positivt heltal.");
        }
        if (amount % 100 != 0)
        {
            return false;
            throw new Exception("Insättning kan endast göras för jämnt 100-tal kronor.");
        }

        Balance += amount;
        return true;
    }
}