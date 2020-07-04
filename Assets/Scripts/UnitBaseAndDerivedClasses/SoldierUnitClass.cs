using UnityEngine;

public class SoldierUnitClass : BaseUnitClass
{

    public SoldierUnitClass()
    {
        UnitName = "Soldier";
        UnitSprite = Resources.Load<Sprite>("UnitSprites/Soldier");
        UnitSizeRow = 1;
        UnitSizeColumn = 1;
        UnitSpeed = 3;
    }

}
