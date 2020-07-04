using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHolder : MonoBehaviour, ISelectable // Tüm unit türlerinin ana yapısı. Ait olduğu building'in class'ı, adıyla yaratıldığı butondan geliyor ve
                                                    // Start'ta oluşturuluyor.
{
    public BaseUnitClass Unit;
    public GameObject InfoTextObject;
    public GameObject InfoImageObject;
    public GameObject StartingGrid;
    public bool isSelected = false;
    public bool isSpawned;
    public string unitType;

    public List<GameObject> myPath = new List<GameObject>();


    public event Action<GameObject, GameObject> ProceedMoveOrder; // gameObject üzerindeki pathfinding'i triggerlayan event

    void Start()
    {
        StartingGrid = MapCreate.terrainLocations[(int)transform.position.x + 14, (int)transform.position.y + 14];
        Unit = UnitFactory.GetUnit(unitType);
        InfoImageObject = GameObject.FindWithTag("InfoImageObject");
        InfoTextObject = GameObject.FindWithTag("InfoTextObject");
        gameObject.AddComponent<BoxCollider2D>();
        ClickManager.MoveOrder += GetMoveOrder;
        StartingGrid.GetComponent<TerrainGrid>().isOccupied = true;
    }


    public void SendInformations()
    {
        InfoTextObject.GetComponent<Text>().text = Unit.UnitName;
        InfoImageObject.GetComponent<Image>().sprite = Unit.UnitSprite;      
    }

    public void isObjectSelected(bool selected)
    {
        isSelected = selected;
    }

    public void GetMoveOrder(GameObject EndingGrid)// Ending Point click manager'dan gelen, unitin gitmesi gereken grid
    {
        if (isSelected == true)
        {
            ProceedMoveOrder?.Invoke(StartingGrid,EndingGrid);
        }
    }
}





