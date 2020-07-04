using UnityEngine;

public class BarracksBuildingClass : BaseBuildingClass
{

    public BarracksBuildingClass() // Ortak özellikler constructor'da belirlendi.
    {
        buildingName = "Barracks";
        buildingSprite = Resources.Load<Sprite>("BuildingSprites/Barracks");
        buildingSizeRow = 4;
        buildingSizeColumn = 4;
        buildTime = 10;
    }

    public override void giveInfoLog()
    {
        base.giveInfoLog();
        Debug.Log(buildingName);
    }
}
