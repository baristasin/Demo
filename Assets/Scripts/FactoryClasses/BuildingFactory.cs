using System.Collections.Generic;
using UnityEngine;

public class BuildingFactory : Singleton<BuildingFactory> // Factory'nin mantığı, dışardan oluşturulan bir building prefabını,public bir listeye koymak.
                                                          // Daha sonra bu building için oluşturulan butona, string olarak hangi building'in yaratılması isteniyorsa
                                                          //o building'in ismini(Building.buildingType) yazmak. Daha sonra buton içerisinden buildingHolder'a,
                                                          // yaratılmış olan building'in ismi gönderilecek. O isme göre de class yaratılacak.
{
    [System.Serializable]
    public class Building // Yardımcı Building Class'ı
    {
        public string buildingType;
        public GameObject BuildingPrefab;        
    }
    Vector2 cursorPos;
    public List<Building> buildingTypes;

    private void Start()
    {
        cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
    }
    public static BaseBuildingClass GetBuilding(string buildingType) // BuildingHolder'ın kendi building'inin classını aldığı fonksiyon.
    {
        switch (buildingType)
        {
            case "Barracks":
                return new BarracksBuildingClass();
            case "PowerPlant":
                return new PowerPlantBuildingClass();            
            default:
                return null;
        }
    }

    public GameObject GiveMeBuilding(string buildingType) // Butonun üzerinde yazan string'e göre building'in prefabını aldığı fonksiyon. Eğer buton üzerine yazdığımız
                                                         // İsim, factory'de olan herhangi bir buildingle eşleşmezse null dönüyor.
    {
        
        for(int i = 0; i < buildingTypes.Count ; i++)
        {
           if(buildingType == buildingTypes[i].buildingType)
            {
                GameObject obj = Instantiate(buildingTypes[i].BuildingPrefab, cursorPos, Quaternion.identity);
                return obj;
            }
        }
        return null;
    }
}
