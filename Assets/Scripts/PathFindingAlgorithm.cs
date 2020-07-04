using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingAlgorithm : MonoBehaviour
{

    private GameObject currentLocation;
    private GameObject updatedLocation;
    public UnitHolder unitHolder;
    private bool isMoving;

    private void Start()
    {
        unitHolder = GetComponent<UnitHolder>();
        unitHolder.ProceedMoveOrder += FindShortestPath; // Eğer Clickable'dan bir moveOrder geldiyse, bu unit isSelected kontrolü yapacak. Eğer kontrol true ise
                                                         // Emri yerine getirecek.
    }

    private void Update()
    {
        if (isMoving == true) // Moving sistemi şu şekilde çalışıyor: ShortestPath işlemi son evrede coroutine olarak MoveCo fonksiyonuna
                              // son listeyi gönderiyor. Bu listedeki ilk obje askerin hareketinin başlangıç pozisyonu, son evreyse bitiş pozisyonu. Liste gönderildiği
                              // anda moving true oluyor ve updatedLocation sırasıyla move listesindeki pozisyonlar oluyor. bir önceki pozisyonun isOccupied değişkeni
                              // false'a çevrilirken, askerin starting grid'i de her seferinde yeni pozisyona çevriliyor. Update'de lerp hareketi yapılıyor.
        {
            transform.position = Vector2.Lerp(transform.position, updatedLocation.transform.position, 0.1f * unitHolder.Unit.UnitSpeed);
        }
    }

    void FindShortestPath(GameObject StartingGrid, GameObject EndingGrid) // A* algoritmasının mantığı, ilk noktayla son nokta arasındaki en kısa yolu bulmak.
                                                                          // mantığı ise, iki liste oluşturup, o anda g (başlangıç noktasından olan uzaklık), 
                                                                          // h(son noktadan olan uzaklık) ve Fcost (g + h)'ları hesaplanan noktaları openSet'e 
                                                                          //atamak, bir sonraki gidilecek nokta olarak en düşük Fcost'u, eğer ki Fcostları eşit olan
                                                                          // noktalar varsa h cost'u düşük olanı seçmek. O anki noktayı da seçim yapıldıktan sonra 
                                                                          // closedSet'e atmak. Bu işlem sırasında komşu noktaların isOccupied kontrolünü yapan, eğer
                                                                          // doğruysa o adımın atlanmasını sağlayan bir kontrol de var.
    {
        List<GameObject> openSet = new List<GameObject>();
        List<GameObject> closedSet = new List<GameObject>();
        openSet.Add(StartingGrid);

        while(openSet.Count > 0) // openset'te nokta olduğu sürece aramaya devam.
        {
            GameObject currentGrid = openSet[0]; // ilk obje aynı zamanda openSet'in ilk noktası.

            for(int i = 1; i < openSet.Count; i++) // openSet'teki en düşük fCostlu noktayı, eğer fCostları eşitse en düşük hcost'a sahip noktayı seç.
            {
                if (openSet[i].GetComponent<TerrainGrid>().fCost < currentGrid.GetComponent<TerrainGrid>().fCost ||
                    openSet[i].GetComponent<TerrainGrid>().fCost == currentGrid.GetComponent<TerrainGrid>().fCost &&
                    openSet[i].GetComponent<TerrainGrid>().hCost < currentGrid.GetComponent<TerrainGrid>().hCost)
                {                   
                        currentGrid = openSet[i]; // seçilen noktayı  en düşük fCostlu noktaya, eğer fCostları eşitse en düşük hcost'a sahip noktaya eşitle.
                }
            }

            openSet.Remove(currentGrid);
            closedSet.Add(currentGrid);

            if(currentGrid == EndingGrid) // İşlem tamamlanmış demektir.
            {
                List<GameObject> finalList = RetracePath(StartingGrid, EndingGrid); // yolu parent düzenine göre oluştur.
                StartCoroutine(MoveCo(finalList)); // hareketi başlat.
            }

            foreach (GameObject neighbour in FindAllNeighbourValues(currentGrid)) // Tüm komşular içinde
            {

                if (neighbour.GetComponent<TerrainGrid>().isOccupied == true || closedSet.Contains(neighbour)) // isOccupied doğruysa adımı atla.
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentGrid.gameObject.GetComponent<TerrainGrid>().gCost + GetDistance(currentGrid, neighbour); // komşu noktanın yeni 
                                                                                  // gCost'(başlangıç noktasından olan uzaklığı)unu hesaplamak için o anki noktanın gCostu ile 
                                                                                  // neighbour'un gCost'u toplamı
                if(newMovementCostToNeighbour < neighbour.gameObject.GetComponent<TerrainGrid>().gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gameObject.GetComponent<TerrainGrid>().gCost = newMovementCostToNeighbour;
                    neighbour.gameObject.GetComponent<TerrainGrid>().hCost = GetDistance(neighbour, EndingGrid);
                    neighbour.gameObject.GetComponent<TerrainGrid>().parent = currentGrid;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                 }
            }
        }
    }

    private IEnumerator MoveCo(List<GameObject> finalList) // Coroutine olması için IEnumerator türünde hareket.
    {
        isMoving = true; 
        for (int i = 0; i < finalList.Count; i++) // parent düzeninden dolayı ters çevrilmiş olan liste.
        {
            UnitHolder MovingUnitsHolder = GetComponent<UnitHolder>();
            updatedLocation = finalList[i]; // updatelocation'ı güncelle.
            MovingUnitsHolder.StartingGrid.GetComponent<TerrainGrid>().isOccupied = false; // eski noktanın isOccupied'ını değiştir.
            MovingUnitsHolder.StartingGrid = finalList[i]; // yeni startingGrid'i oluştur.
            finalList[i].GetComponent<TerrainGrid>().isOccupied = true; // o anki yeni noktayı occupy et

            if(i > 0) // eğer ilk noktada değilsek
            {
                finalList[i - 1].GetComponent<TerrainGrid>().isOccupied = false; // bir önceki noktanın isOccupied'ını güncelle.
            }
            yield return new WaitForSeconds(1.0f); // bir saniye bekle
        }
        isMoving = false;
    }

    List<GameObject> RetracePath(GameObject startGrid, GameObject endGrid) // TerrainGrid'deki parent düzenine göre liste oluşturuluyor.
    {
        List<GameObject> path = new List<GameObject>();
        GameObject currentGrid = endGrid;

        while(currentGrid != startGrid)
        {
            path.Add(currentGrid);
            currentGrid = currentGrid.gameObject.GetComponent<TerrainGrid>().parent;
        }
        path.Reverse(); // Parent düzeni sondan başa olduğu için, liste ters çevriliyor.
        return path;

    }

    private List<GameObject> FindAllNeighbourValues(GameObject currentGrid) // 8 tane komşu bulunuyor.
    {
        List<GameObject> AllNeighbours = new List<GameObject>();
        TerrainGrid terrainGrid = currentGrid.GetComponent<TerrainGrid>();

       for (int x = -1; x<=1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;
                int gridX = terrainGrid.column + x;
                int gridY = terrainGrid.row + y;

                if(gridX >= 0 && gridX < MapCreate.mapColumn && gridY >= 0 && gridY < MapCreate.mapRow)
                {
                    AllNeighbours.Add(MapCreate.terrainLocations[gridX,gridY]);
                }
            }
        }
        return AllNeighbours;
    }

    public int GetDistance(GameObject first,GameObject second) // iki nokta arasındaki düz uzaklığın ölçüldüğü fonksiyon. eğer yukarı, aşağı veya sol,sağ yapıldıysa
                                                               // grid cell boyutu 1x1 olduğu için uzaklık 1 olacaktır. çapraz hareket uzaklığı ise 1.4
    {
        TerrainGrid gridA = first.GetComponent<TerrainGrid>();
        TerrainGrid gridB = second.GetComponent<TerrainGrid>();


        int distX = Mathf.Abs(gridA.column - gridB.column); // Mathf.Abs mutlak değer fonksiyonu
        int distY = Mathf.Abs(gridA.row - gridB.row);

        if (distX > distY) // Bu işlemde kullanılan bir mantık var. bir noktanın diğer noktaya olan uzaklığı sadece x ve y eksenlerindeki hareket sayısı olarak düşünüldüğünde,
                           // hareket sayısı az olan yön, her zaman çapraz gidilmesi gereken uzaklığı veriyor. Çapraz gidilen mesafe iki eksende de gidilen mesafe olduğu için,
                           // hareket sayısı çok olan yönün hareket sayısından, hareket sayısı az olan yönün hareket sayısı çıkarıldığında, o yönde gidilmesi gereken kalan mesafe
                           // bulunur.
        {

            // 14 ve 10 la çarpılmasının amacı çapraz mesafe ilerleyiş değeri olan 1.4 sayısından kurtulmak
            return 14 * distY + 10 * (distX - distY);
        }
        return 14 * distX + 10 * (distY - distX);

    }
}
