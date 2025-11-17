public class Membership(string memberName)
{
  public string MemberName { get; set; } = memberName;

  public virtual string GetBenefits() => "Newsletter";
}

public class StandardMembership(string memberName) : Membership(memberName)
{
  public override string GetBenefits() => "Newsletter + Standard support";
}
public class PremiumMembership(string memberName) : Membership(memberName)
{
  public override string GetBenefits() => "Newsletter + Premium support + cool stuff";
}
public sealed class LifetimeMembership(string memberName) : Membership(memberName)
{
  public override string GetBenefits() => "Newsletter + Lifetime support + cool stuff + hoodie";
}