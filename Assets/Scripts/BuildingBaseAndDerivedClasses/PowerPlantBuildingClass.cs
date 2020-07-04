using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlantBuildingClass : BaseBuildingClass
{

    public PowerPlantBuildingClass() // Ortak özellikler constructor'da belirlendi.
    {
        buildingName = "Power Plant";
        buildingSprite = Resources.Load<Sprite>("BuildingSprites/PowerPlant");
        buildingSizeRow = 2;
        buildingSizeColumn = 3;
        buildTime = 7;
    }


    public override void giveInfoLog()
    {
        base.giveInfoLog();
        Debug.Log(buildingName);
    }
}
