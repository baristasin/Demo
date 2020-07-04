using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour // Her singleton kullanımında awake kullanmamak için generic type.Birkaç yerde kullanılacağı için
                                                                  // generic type olarak oluşturuldu.
{
    public static T Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = (T)FindObjectOfType(typeof(T));
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
