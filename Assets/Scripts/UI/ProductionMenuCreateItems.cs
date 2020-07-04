using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionMenuCreateItems : MonoBehaviour
{
    ObjectPooler objectPooler; 
    public ProductionMenu ProductionMenu;
    public GameObject BarracksButtonPrefab;
    public GameObject PowerPlantButtonPrefab;
    private int j;

    void Start()
    {
        ProductionMenu = GetComponent<ProductionMenu>();
        ProductionMenu.AddItemsToBottom += newButtonSpawnBottom;
        objectPooler = ObjectPooler.Instance;
        j = ProductionMenu.row;
    }
    public void newButtonSpawnBottom()
    {
        GameObject BarracksButtonObj = objectPooler.SpawnFromPool("BarracksButton", transform.position, Quaternion.identity); // scroll yapıldığı için yeni itemlar pool'dan alınıyor.
        GameObject PowerPlantButtonObj = objectPooler.SpawnFromPool("PowerPlantButton", transform.position, Quaternion.identity); // scroll yapıldığı için yeni itemlar pool'dan alınıyor.

        BarracksButtonObj.transform.SetParent(this.transform, false);
        BarracksButtonObj.transform.localPosition = new Vector3(-40, 170 + j * -70, 0);
        PowerPlantButtonObj.transform.SetParent(this.transform, false);
        PowerPlantButtonObj.transform.localPosition = new Vector3(40, 170 + j * -70, 0);
        j++;
    }
}
