namespace Shop.Payments;

public class PaymentProcessor
{
  // Simulated rule set for the exercise
  public void Charge(string cardNumber, decimal amount)
  {
    // TODO: throw ArgumentNullException/ArgumentException for invalid cardNumber
    if (string.IsNullOrEmpty(cardNumber))
    {
      throw new ArgumentException("cardNumber must be a non-empty string");
    }
    // TODO: throw ArgumentOutOfRangeException if amount <= 0
    // if (quantity <= 0) throw new ArgumentOutOfRangeException("amount must be greater than 0");
    ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);

    // Simulate business rules that trigger your custom exception
    // e.g., decline if amount > 1000m or card starts with "0000"
    // throw new PaymentDeclinedException("Payment declined", reason, amount);
    if (amount < 1000m)
    {
      throw new PaymentDeclinedException("Payment declined", "Charge is too small", amount);
    }
    if (cardNumber.StartsWith("0000"))
    {
      throw new PaymentDeclinedException("Payment declined", "Invalid card number", amount);
    }
  }
}