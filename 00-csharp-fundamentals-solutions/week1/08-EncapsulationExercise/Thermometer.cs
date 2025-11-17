public class Thermometer
{
  public double TemperatureCelsius { get; private set; }

  public Thermometer(double initialTemp)
  {
    TemperatureCelsius = initialTemp;
  }

  public void SetTemperature(double value)
  {
    if (value >= -50 && value <= 100)
    {
      TemperatureCelsius = value;
    }
    else
    {
      Console.WriteLine("Temperature must be between -50 and 100");
    }
  }

  public double GetFahrenheit()
  {
    return TemperatureCelsius * 1.8 + 32D;
  }
}

// with Primary Constructor and lambda
// public class Thermometer
// {
//   public double TemperatureCelsius { get; private set; } = 20.0;

//   public void SetTemperature(double value)
//   {
//     if (value < -50 || value > 100)
//     {
//       Console.WriteLine("Temperature out of range (-50..100). Ignored.");
//       return;
//     }
//     TemperatureCelsius = value;
//   }

//   public double GetFahrenheit() => (TemperatureCelsius * 9 / 5) + 32;
// }