using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawnPointCreate : MonoBehaviour //Building Holder eklendikten sonra buildingin asker üretmesi isteniyorsa eklenmesi gereken class.
{
    public int column;
    public int row;
    public List<GameObject> BuildingSpawnPointLocations;
    BuildingHolder buildingHolderObject;

    void Start() 
    {
        buildingHolderObject = GetComponent<BuildingHolder>();
        column = buildingHolderObject.column;
        row = buildingHolderObject.row;
        buildingHolderObject.buildingIsSet += BuildingSpawnPoint; // BuildingHolder'da building tamamen kurulduktan sonra spawnPoint yaratma başlıyor.
    }

    void BuildingSpawnPoint() // Binanın yaratılmasındaki fikir şu: Her bina satır ve sütun sayılarının 2'şer fazlası satır ve sütun olarak 
                              // yaratılıyor. Bunun sebebi fazladan oluşan satır ve sütunu, SpawnPoint olarak kullanmak. Bu ayarlamaya göre
                              // binanın fazladan yaratılan satır ve sütunları burada spawnPoint olarak oluşturuluyor.
    {
        for (int i = 0; i < column + 2; i++)
        {
            for (int j = 0; j < row + 2; j++)
            {
                if (i > 0 && i < column + 1 && j > 0 && j < row + 1)
                {

                }
                else
                {
                    GameObject spawnPointPiecePos = MapCreate.terrainLocations[(int)transform.position.x + i + 14, (int)transform.position.y + j + 14];
                    BuildingSpawnPointLocations.Add(spawnPointPiecePos);
                }
            }
        }
        buildingHolderObject.buildingIsSet -= BuildingSpawnPoint;
    }
    
}




