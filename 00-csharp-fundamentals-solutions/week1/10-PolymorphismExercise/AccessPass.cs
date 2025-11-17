namespace Transport;

public class AccessPass
{
  public string Holder { get; }

  public AccessPass(string holder)
  {
    Holder = holder;
  }

  public virtual bool Validate()
  {
    Console.WriteLine($"Validating generic access for {Holder}...");
    return true;
  }
}

public class MetroPass : AccessPass
{
  public int Zones { get; }

  public MetroPass(string holder, int zones) : base(holder)
  {
    Zones = zones;
  }

  public override bool Validate()
  {
    Console.WriteLine($"{Holder}: Metro zones = {Zones}");
    return Zones >= 1;
  }
}

public class BikeSharePass : AccessPass
{
  public int MinutesRemaining { get; private set; }

  public BikeSharePass(string holder, int minutes) : base(holder)
  {
    MinutesRemaining = minutes;
  }

  public override bool Validate()
  {
    Console.WriteLine($"{Holder}: Bike‑share minutes = {MinutesRemaining}");
    return MinutesRemaining > 0;
  }
}

public sealed class FerryPass : AccessPass
{
  public DateTime Expiry { get; }

  public FerryPass(string holder, DateTime expiry) : base(holder)
  {
    Expiry = expiry;
  }

  public sealed override bool Validate()
  {
    bool ok = DateTime.UtcNow <= Expiry.ToUniversalTime();
    Console.WriteLine($"{Holder}: Ferry pass valid? {ok} (expires {Expiry:yyyy-MM-dd})");
    return ok;
  }
}