public class Equipment // Переделать под объект оружия
{
    public int Armor;
    public int MeleWeapon;
    public int DistanceWeapon;

    public Equipment()
    {
        Armor = 0;
        MeleWeapon = 0;
        DistanceWeapon = 0;
    }

    public Equipment(int armor, int meleWeapon, int distanceWeapon) 
    {
        Armor = armor;
        MeleWeapon = meleWeapon;
        DistanceWeapon = distanceWeapon;
    }
}
