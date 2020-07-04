using UnityEngine;


public class BaseBuildingClass // Tüm child classların ortak kullanacağı özellikler
{
    protected string buildingName;
    protected Sprite buildingSprite;
    protected int buildingSizeRow;
    protected int buildingSizeColumn;
    protected int buildTime;

    // --- GETTERS AND SETTERS ---

        public virtual void giveInfoLog()
    {
        Debug.Log("building name" + " ");
    }


    public Sprite BuildingSprite
    {
        get
        {
            return buildingSprite;
        }
        set
        {
            buildingSprite = value;
        }
    }

    public string BuildingName
    {
        get
        {
            return buildingName;
        }

        set
        {
            buildingName = value;
        }
    }

        public int BuildingSizeRow
    {
        get
        {
            return buildingSizeRow;
        }

        set
        {
            buildingSizeRow = value;
        }
    }

    public int BuildingSizeColumn
    {
        get
        {
            return buildingSizeColumn;
        }

        set
        {
            buildingSizeColumn = value;
        }
    }

    public int BuildTime
    {
        get
        {
            return buildTime;
        }

        set
        {
            buildTime = value;
        }
    }

}
