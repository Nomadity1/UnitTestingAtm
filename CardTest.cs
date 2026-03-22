using ATM;

namespace AtmTest
{
    public class CardTest
    {
        // Förutsätter att vi har ett konto
        // Börja med att instansiera ett kort för kontot som kan användas i testerna
        private Card card = new Card("1234-56789", "1234", new Account(5000));
        // Cardnumber, PinCode och Account(int Balance) måste skickas med

        // Testmetod för att kontrollera pin-kod
        [Theory] // Pga mer än 1 indata
        [InlineData("1234", true)] // Testresultatet blir godkänt om förväntat (angett) värde (här true) matchar det faktiska resultatet
        [InlineData("4321", false)] // Testresultatet blir godkänt om förväntat (angett) värde (här false) matchar det faktiska resultatet
        public void MatchesPinTest(string inputPin, bool expectedResult)
        {
            var result = card.MatchesPin(inputPin);
            Assert.Equal(expectedResult, result);
        }
    }
}
