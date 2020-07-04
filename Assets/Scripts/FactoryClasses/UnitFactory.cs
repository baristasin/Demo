public static class UnitFactory
{

    public static BaseUnitClass GetUnit(string unitName) // Factory'nin mantığı, dışardan oluşturulan bir building prefabını,public bir listeye koymak.
                                                         // Daha sonra bu building için oluşturulan butona, string olarak hangi building'in yaratılması isteniyorsa
                                                         // yaratılmış olan building'in ismi gönderilecek. O isme göre de class yaratılacak.
    {
        switch (unitName)
        {
            case "Soldier":
                return new SoldierUnitClass();
            default:
                return null;
        }
    }
}