public class ShieldAbility : Ability
{
    public override void Cast()
    {
        throw new System.NotImplementedException();
    }

    public override int GetIndexAbility() =>
        (int)IndexAbility.Shield;
}