using System.Collections.Generic;
using UnityEngine;

public class MapCreate : Singleton<MapCreate> // Haritayı kuran class
{
    public static int mapColumn = 30;
    public static int mapRow = 30;
    private Vector2 terrainPosition;
    public GameObject TerrainPrefab;
    public static GameObject[,] terrainLocations;
    public Vector2 midPoint;

    void Start()
    {
        midPoint = new Vector2(-14, -14);
        terrainLocations = new GameObject[mapColumn, mapRow];
        MapCreateFunc();
        transform.position = midPoint;
    }
    private void MapCreateFunc() // Grid sistemi oluşturuluyor. Gridler birçok amaç için kullanılacağı için, iki boyutlu bir array'de tutuluyor. 
                                 // Match-3 oyunu sistemine benzer
    {   
        for(int i = 0; i <= mapColumn - 1; i++)
        {
            for(int j = 0; j <= mapRow - 1; j++)
            {
                terrainPosition = new Vector2(i,j);
                GameObject terrain = Instantiate(TerrainPrefab, terrainPosition, Quaternion.identity) as GameObject;
                terrain.name = "(" + ( i ) + "," + ( j) + ")";
                terrainLocations[i, j] = terrain;
                terrain.transform.parent = this.transform;
                terrain.AddComponent<BoxCollider2D>().isTrigger = true; // trigger tıklanan collider'lı objenin terrain mi yoksa başka bir obje mi olduğunu anlamak için oluşturuldu.
                terrain.GetComponent<TerrainGrid>().column = i;
                terrain.GetComponent<TerrainGrid>().row = j;
            }
        }
    }
}
