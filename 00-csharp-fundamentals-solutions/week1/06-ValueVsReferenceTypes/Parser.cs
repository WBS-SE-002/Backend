public class Parser
{
  public static bool TryParsePrice(string text, out decimal price)
  {

    string cleaned = text.Trim();
    cleaned = cleaned.Replace("£", "").Replace("€", "").Replace("$", "");
    cleaned = cleaned.Replace(',', '.');

    return decimal.TryParse(
        cleaned,
        out price
    );
  }

}