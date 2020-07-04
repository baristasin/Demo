using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;

public class BuildingHolder : MonoBehaviour,ISelectable // Tüm building türlerinin ana yapısı. Ait olduğu building'in class'ı, adıyla yaratıldığı butondan geliyor ve
                                                        // Start'ta oluşuturuluyor.
{
    BaseBuildingClass Building;
    WarningScript warningScript;
    public bool isSet;
    public int column;
    public int row;
    public GameObject InfoTextObject;
    public GameObject virtualBuildingPrefab;
    public GameObject spawnPoint;
    public GameObject InfoImageObject;
    public GameObject cantBuildPrefab;
    public string buildingType;
    public List<GameObject> BuildingPieces;
    public bool isSelected = false;

    public event Action buildingIsSet;

    private void Awake()
    {
        BuildingPieces = new List<GameObject>();
    }

    private void Start()
    {
        warningScript = WarningScript.Instance;
        Building = BuildingFactory.GetBuilding(buildingType); // Building kendi classının butondan gönderildiği üzere factory'den yaratıyor.
        InfoImageObject = GameObject.FindWithTag("InfoImageObject");
        InfoTextObject = GameObject.FindWithTag("InfoTextObject");
        ClickManager.NonClickableObject += BuildingSet; // VirtualBuilding kurulumu yapıldıktan sonra nonClickable olarak işaretlenmiş terrain'a tıklandığında 
                                                       // RealBuilding oluşturuluyor.
        CreateVirtualBuilding();
        Building.giveInfoLog(); // Bina yaratıldığında iki farklı class için farklı çalışan info fonksiyonu
    }
    private void Update()
    {
        if (!isSet) // Bina kurulu değilse, mouse imlecini takip etsin.
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2((int)cursorPos.x, (int)cursorPos.y);
        }
    }
    void CreateVirtualBuilding() // butona tıklandığı anda oluşan sanal building.
    {
        column = Building.BuildingSizeColumn;
        row = Building.BuildingSizeRow;
        // Binanın yaratılmasındaki fikir şu: Her bina satır ve sütun sayılarının 2'şer fazlası satır ve sütun olarak 
        // yaratılıyor. Bunun sebebi fazladan oluşan satır ve sütunu, SpawnPoint olarak kullanmak. Bu ayarlamaya göre
        // binanın orjinal satır ve sütun sayısı içerisinde kalan alan burada oluşturuluyor.
        for (int i = 0; i < column + 2; i++) 
        {
            for (int j = 0; j < row + 2; j++)
            {
                if (i > 0 && i < column + 1 && j > 0 && j < row + 1)
                {
                    Vector2 pos = new Vector2(transform.position.x + i, transform.position.y + j);
                    GameObject buildingPiece = Instantiate(virtualBuildingPrefab, pos, Quaternion.identity) as GameObject;
                    buildingPiece.transform.parent = transform; // mouse'un hareketine göre hareket etmesi için buildingHolder'un child objesi oldu. 
                    BuildingPieces.Add(buildingPiece);
                }       
            }
        }
    } 
    void BuildingSet() // Gerçek binanın kurulma aşaması
    {
        if (OccupiedControl())
        {
            isSet = true;
            CreateRealBuilding();
            ClickManager.NonClickableObject -= BuildingSet;// RealBuilding kurulumu yapıldıktan sonra unsubscribe
            buildingIsSet?.Invoke(); 
        }
    }
    bool OccupiedControl() // Binanın yaratılmak istendiği bölgenin uygun olup olmadığının kontrolü. 
    {

        for (int i = 0; i <= BuildingPieces.Count - 1; i++)
        {
            if (MapCreate.terrainLocations[(int)BuildingPieces[i].transform.position.x + 14, (int)BuildingPieces[i].transform.position.y + 14].GetComponent<TerrainGrid>().isOccupied == true)
            {
                warningScript.GiveWarning("CantBuildWarning");
                return false;
            }
        }
        return true;
    }


    void CreateRealBuilding() // Binanın kurulumuyla beraber map'in gerekli alanı occupiedLands'a ekleniyor ve binaya colliderlar dahil ediliyor.
    {
        for (int i = 0; i <= BuildingPieces.Count - 1; i++)
        {
            // Soldier pathfinding algoritmasının kullanımı için occupy edilen node'lar kendi classları içinde de dolu olarak işaretleniyor.
            MapCreate.terrainLocations[(int)BuildingPieces[i].transform.position.x + 14, (int)BuildingPieces[i].transform.position.y + 14].GetComponent<TerrainGrid>().isOccupied = true;
        }

        float colliderOffsetX = ((float)column - 1) / 2;
        float colliderOffsetY = ((float)row - 1) / 2;

        gameObject.AddComponent<BoxCollider2D>();
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(colliderOffsetX + 1 ,colliderOffsetY + 1);
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2((column), (row));
    }

    public void SendInformations() //ISelectable interface fonksiyonu
    {
        InfoImageObject.GetComponent<Image>().sprite = Building.BuildingSprite;
        InfoTextObject.GetComponent<Text>().text = Building.BuildingName;
    }

    public void isObjectSelected(bool selected) //ISelectable interface fonksiyonu
    {
        isSelected = selected;
    }
}
