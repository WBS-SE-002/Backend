namespace Shop.Payments;

public class PaymentDeclinedException : Exception
{
  public string Reason { get; }
  public decimal Amount { get; }

  public PaymentDeclinedException(string message, string reason, decimal amount)
      : base(message)
  {
    Reason = reason;
    Amount = amount;
  }
}