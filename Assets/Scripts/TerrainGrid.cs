using UnityEngine;

public class TerrainGrid : MonoBehaviour
{

    public int column;
    public int row;
    public bool isOccupied; 

    public int gCost;
    public int hCost;
    public GameObject parent;

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
}
