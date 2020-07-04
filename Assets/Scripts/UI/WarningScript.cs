using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningScript : Singleton<WarningScript>
{
    public GameObject WarningTextObject;
    
    public void GiveWarning(string warningType) // Oyunda alınabilecek hatalar için yaratıldı. Daha ileri bir evrede kullanım kolaylığı açısından
                                                // warning stringleri List'e atanabilir
    {
        if (warningType == "CantBuildWarning")
        {
            StartCoroutine(CantBuildThereWarning());
        }
    }

    private IEnumerator CantBuildThereWarning()
    {
        WarningTextObject.gameObject.SetActive(true);
        WarningTextObject.GetComponent<Text>().text = "--- You Can't Build There !!!";
        yield return new WaitForSeconds(1.0f);
        WarningTextObject.gameObject.SetActive(false);
    }

}
