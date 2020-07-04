using UnityEngine;
using System;

public class ButtonScript : MonoBehaviour // Bu class, gameObject'in üzerinde component olarak eklenen, herhangi bir BaseBuildingClass'ının, map üzerindeki uygunluk kontrolünün yapılabilmesi için
{                                         // oluşturulucak olan virtualBuilding'in oluşturulması için yaratıldı.
    public GameObject BuildingPrefab; // İstenen building public olarak alındı.
    public String buildingType;
    BuildingFactory buildingFactory;

    private void Start()
    {
        buildingFactory = BuildingFactory.Instance;
    }
    public void CreateVirtualBuilding() // Building instantiate edildi.
    {
        GameObject obj = buildingFactory.GiveMeBuilding(buildingType); // Her butonun üzerindeki bu script, building type değerine göre factory'den istenen building prefabını alıyor.
        obj.gameObject.GetComponent<BuildingHolder>().buildingType = buildingType; // yaratılan building'in holder kısmında hangi class'ın yaratılacağına yine butona girilmiş olan buildingType değeri karar veriyor.
    }
}
