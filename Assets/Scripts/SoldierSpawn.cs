using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierSpawn : MonoBehaviour, ISelectable // Hangi binadan hangi askerleri basmak istediğimize göre değişen bir fonksiyon
{
    public List<GameObject> SoldierTypes; 
    public GameObject ButtonList;
    public bool isSelected;
    private BuildingSpawnPointCreate buildingSpawnPointCreate;

    private void Start()
    {

        buildingSpawnPointCreate = GetComponent<BuildingSpawnPointCreate>();
        ButtonList = GameObject.FindWithTag("ButtonList");
    }


    public void SpawnSoldier(int i)
    {
        if (GetComponent<BuildingHolder>().isSelected == true) // Eğer ki building seçiliyse
        {

            GameObject usualSpawnPoint = buildingSpawnPointCreate.BuildingSpawnPointLocations[0]; // ilk olarak normal pozisyona spawn'ı dene
            if (SpawnPointLocationControl(usualSpawnPoint))
            {
                Instantiate(SoldierTypes[i], usualSpawnPoint.transform.position, Quaternion.identity);
                SoldierTypes[i].gameObject.transform.SetParent(null);
            }

            else // eğer normal spawn doluysa, 
            {
                int validSpawnPoint = FindValidSpawnPoint(); // daha önceden 6x6 kurduğumuz building'in spawnpoint olarak işaretlenmiş noktalarında boş yer ara.
                Instantiate(SoldierTypes[i], buildingSpawnPointCreate.BuildingSpawnPointLocations[validSpawnPoint].transform.position, Quaternion.identity);
                SoldierTypes[i].gameObject.transform.SetParent(null);
            }
        }
    }

    public bool SpawnPointLocationControl(GameObject testingLocation)
    {
        if(testingLocation.GetComponent<TerrainGrid>().isOccupied == false)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    private int FindValidSpawnPoint() // daha önceden 6x6 kurduğumuz building'in spawnpoint olarak işaretlenmiş noktalarında boş yer arayan fonksiyon.
    {
        for (int j = 1; j < buildingSpawnPointCreate.BuildingSpawnPointLocations.Count; j++)
        {
            if (SpawnPointLocationControl(buildingSpawnPointCreate.BuildingSpawnPointLocations[j]))
            {
                return j;
            }
        }
        return 0;
    }
    public void InitializeButtons() // Bu kısım buton oluşturmak için. Mantığı çoğu strateji oyunundaki gibi. Soldier Spawn binalarında 
                                   //belirli sayıda buton var(Örn: Age Of Empires 2 'de 4 buton). Seçilen building'in üzerindeki SoldierSpawn'da kaç obje var ise
                                   //o sayıda buton açılıyor ve sırasıyla onClick eventleri dolduruluyor. 
    {
        for (int i = 0; i < SoldierTypes.Count; i++)
        {
            ButtonList.transform.GetChild(i).gameObject.SetActive(true);
            ButtonList.transform.GetChild(i).gameObject.GetComponent<Button>().onClick.AddListener(() => SpawnSoldier(i - 1));
        }
    }

    public void SendInformations()
    {
        InitializeButtons(); // tıklandığında butonları oluşturması için bu class da ISelectable'a dahil edildi.
    }

    public void isObjectSelected(bool selected)
    {

    }

}
