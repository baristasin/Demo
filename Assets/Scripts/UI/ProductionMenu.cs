using UnityEngine;
using System;

public class ProductionMenu : MonoBehaviour
{
    // Bina üretmeyi sağlayan, sol kısımdaki production menüsü
    private int column = 1;
    public int row = 8;
    private Vector2 position;
    public float currentY;
    private int i = 0;
    private int j = 0;
    ObjectPooler objectPooler;
    public ProductionMenuCreateItems CreateItems;
    public static event Action AddItemsToBottom = delegate { };

    void Start()
    {
        RectTransform rect = GetComponent<RectTransform>();
        CreateItems = GetComponent<ProductionMenuCreateItems>();
        currentY = rect.localPosition.y; // yeni buton üretmesini sağlamak için pozisyon bilgisi tutuldu.
        objectPooler = ObjectPooler.Instance; // object pooler instance
        BuildingButtonSpawn(); // ilk menünün oluşturulduğu yer
    }
    
    void Update()
      {
          if(Mathf.Abs(currentY - GetComponent<RectTransform>().localPosition.y) > 75 && currentY < GetComponent<RectTransform>().localPosition.y)
          {
            currentY = GetComponent<RectTransform>().localPosition.y;
            AddItemsToBottom(); // eğer yaklaşık olarak bir buton boyutunda scroll yapıldıysa, event başlatılıyor. Bu evente ProductionMenuCreateItems class ı subscribe olacak.
          }
    }
      
      private void BuildingButtonSpawn()
      {
          for(i = 0; i <= column; i++)
          {
              for (j = 0; j <= row; j++)
              {
                GameObject BarracksButtonObj = objectPooler.SpawnFromPool("BarracksButton", transform.position, Quaternion.identity); // object pool'undan bir Barracks buton objesi alınıyor.
                GameObject PowerPlantButtonObj = objectPooler.SpawnFromPool("PowerPlantButton", transform.position, Quaternion.identity); // object pool'undan bir Power Plant buton objesi alınıyor.
                BarracksButtonObj.transform.SetParent(this.transform, false);
                PowerPlantButtonObj.transform.SetParent(this.transform, false);
                BarracksButtonObj.transform.localPosition = new Vector3(-40, 170 + j * -70, 0);
                PowerPlantButtonObj.transform.localPosition = new Vector3(40, 170 + j * -70, 0);
            }
        }
      }
}
