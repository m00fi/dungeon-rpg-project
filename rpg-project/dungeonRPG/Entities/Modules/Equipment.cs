using dungeonRPG.Items.Weapons;

namespace dungeonRPG.Entities.Modules;

public class Equipment
{
    public Weapon? LeftHand { get; private set; }
    public Weapon? RightHand { get; private set; }
    
    public List<Weapon> EquipOneHanded(Weapon weapon)
    {
        var unequipped = new List<Weapon>();

        if (LeftHand != null && LeftHand == RightHand)
        {
            unequipped.Add(LeftHand);
            LeftHand = null;
            RightHand = null;
        }

        if (RightHand == null)
        {
            RightHand = weapon;
        }
        else if (LeftHand == null)
        {
            LeftHand = weapon;
        }
        else
        {

            unequipped.Add(RightHand);
            RightHand = weapon;
        }

        return unequipped;
    }

    public List<Weapon> EquipTwoHanded(Weapon weapon)
    {
        var unequipped = new List<Weapon>();

        if (LeftHand != null && LeftHand == RightHand)
        {
            unequipped.Add(LeftHand);
        }
        else
        {
            if (LeftHand != null) unequipped.Add(LeftHand);
            if (RightHand != null) unequipped.Add(RightHand);
        }
        LeftHand = weapon;
        RightHand = weapon;

        return unequipped;
    }

    public string GetLeftHandName() => LeftHand?.Name ?? "~empty";
    public string GetRightHandName() => RightHand?.Name ?? "~empty";
}