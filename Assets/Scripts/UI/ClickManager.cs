using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class ClickManager : MonoBehaviour
{
    public Camera cam;

    public static event Action NonClickableObject; // Bina, asker veya UI olmayan her obje
    public static event Action ClickableObject; // Bina ve asker
    public static event Action<GameObject> MoveOrder; // Askere hareket komutunun verildiği event. Argüman olarak hareket edilecek olan kareyi gönderiyor.
                                                      // Bu gönderimden sonra, isSelected'ı true olan ve component olarak pathfinding taşıyan her objeye o noktaya
                                                      // hareket edecektir, çoklu seçim için de uygun.
    public static List<GameObject> SelectedObjects;

    private void Start()
    {
        SelectedObjects = new List<GameObject>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject()) // Click'in herhangi bir UI elementine yapılmadığının kontrolü
            {
                Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

                if (hit) // Collider'ı olan bir objeye tıkladıysak
                {
                    ClickableObject(); // InformationPanel'in yazıyı görüntüyü ve eğer varsa spawn butonunu temizlemesinde kullanılan event
                    if (hit.collider.isTrigger == true)// Hiçbir özelliği olmayan, terrain'e tıklandıysa
                    {
                        NonClickableObject(); // InformationPanel Refresh ve buildingHolder'ın CreateRealBuilding fonksiyonunu trigger eden event

                        if (SelectedObjects.Count != 0)
                        {
                            ISelectable[] clickable = SelectedObjects[0].GetComponents<ISelectable>(); // Seçilmiş olan tüm class'ları serbest bırakan kısım
                            for (int i = 0; i < clickable.Length; i++)
                            {
                                clickable[i]?.isObjectSelected(false);
                            }
                        }
                    }

                    else // trigger true değilse, gerçek bir objeye tıklanmıştır.
                    {
                        if (SelectedObjects.Count != 0) // Daha önce seçilmiş olan objeler varsa
                        {
                            ISelectable[] clickable2 = SelectedObjects[0].GetComponents<ISelectable>();
                            for (int i = 0; i < clickable2.Length; i++)
                            {
                                clickable2[i].isObjectSelected(false); // Daha önce seçilmiş olanların selected'ları false yapılıyor.

                            }
                        }

                        SelectedObjects.Clear(); // Seçilmiş listesi temizleniyor.
                        ISelectable[] clickable = hit.collider.GetComponents<ISelectable>(); //  Bilgisi gösterilecek obje var mı diye bak (buildings ve soldiers)
                        for (int i = 0; i < clickable.Length; i++)
                        {
                            clickable[i]?.SendInformations(); // object varsa infosunu panele yolla
                            clickable[i]?.isObjectSelected(true); //  tıklanan objeyi selected olarak seç
                        }

                        SelectedObjects.Add(hit.collider.gameObject); //o anki seçilmiş listesine ekle
                    }
                }

                else // Collider'ı olmayan bir objeye tıkladıysan
                {
                    NonClickableObject(); // Bu event'in iki işi var. Birincisi bina kurulma aşamasındayken, sol tıkla binayı yerleştirmesi,
                                          // İkincisi haritada herhangi bir yere tıklandığında, information panelin restartlanması.
                }
            }
        }

        if (Input.GetMouseButtonDown(1)) // Eğer mouse right click kullanıldıysa
        {
            if (!EventSystem.current.IsPointerOverGameObject()) // UI element değilse
            {
                Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

                if (hit) 
                {
                    if(hit.collider.isTrigger == true) // right click'in yapıldığı obje terrain ise
                    {
                        if (MoveOrder != null)
                        {
                            MoveOrder(hit.collider.gameObject); // MoveOrder event'ini başlat. Seçili olan asker var ise bu emri yerine getiricek.
                            Debug.Log("eğer seçili olan asker varsa şu pozisyona gitsin" + hit.collider.gameObject.name);
                        }
                    }
                }
            }
        }
    }
}
