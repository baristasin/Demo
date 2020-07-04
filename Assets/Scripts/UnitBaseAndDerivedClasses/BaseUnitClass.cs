using UnityEngine;

public class BaseUnitClass // Tüm child classların ortak kullanacağı özellikler
{
    protected string unitName;
    protected Sprite unitSprite;
    protected int unitSizeRow;
    protected int unitSizeColumn;
    protected int unitSpeed;

    // --- GETTERS AND SETTERS ---

    public string UnitName
    {
        get
        {
            return unitName;
        }

        set
        {
            unitName = value;
        }
    }
    public Sprite UnitSprite
    {
        get
        {
            return unitSprite;
        }

        set
        {
            unitSprite = value;
        }
    }
    public int UnitSizeRow
    {
        get
        {
            return unitSizeRow;
        }

        set
        {
            unitSizeRow = value;
        }
    }
    public int UnitSizeColumn
    {
        get
        {
            return unitSizeColumn;
        }

        set
        {
            unitSizeColumn = value;
        }
    }
    public int UnitSpeed
    {
        get
        {
            return unitSpeed;
        }

        set
        {
            unitSpeed = value;
        }
    }

}
