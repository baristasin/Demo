using UnityEngine;
using UnityEngine.UI;

public class InformationPanel : MonoBehaviour
{
    public GameObject InfoTextObject;
    public GameObject InfoImagePanel;
    public GameObject ButtonList;
    private void Start()
    {
        ClickManager.NonClickableObject += RefreshPanel; // Haritadaki boşluğa tıklamak panelin refresh fonksiyonunu çalıştırıyor.
        ClickManager.ClickableObject += RefreshPanel; // Tıklanabilir objeye tıklamak önce refresh fonksiyonunu çalıştırıyor.
    }

    public void RefreshPanel()
    {
        InfoTextObject.GetComponent<Text>().text = null;
        InfoImagePanel.GetComponent<Image>().sprite = null;        
        
        for(int i = 0; i < 2; i++) // Bu kısım butonları temizlemek için. Mantığı çoğu strateji oyunundaki gibi. Soldier Spawn binalarında 
                                  //belirli sayıda buton var(Örn: Age Of Empires 2 'de 4 buton). Seçilen building'in üzerindeki SoldierSpawn'da kaç obje var ise
                                  //o sayıda buton açılıyor ve sırasıyla onClick eventleri dolduruluyor. Eğer yeni bir tıklama yapılırsa tüm butonlar yeniden inaktif
                                  // hale geliyor ve onClick eventleri de siliniyor.
        {
            ButtonList.transform.GetChild(i).gameObject.SetActive(false);
            ButtonList.transform.GetChild(i).gameObject.GetComponent<Button>().onClick.RemoveAllListeners(); 
        }
    }
}
 

