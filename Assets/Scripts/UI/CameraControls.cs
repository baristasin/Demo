using UnityEngine;

public class CameraControls : MonoBehaviour // Mouse scroll'u ile kamera hareketi
{
    void Update() 
    {
            if (Input.GetAxis("Mouse ScrollWheel") > 0 && Camera.main.orthographicSize > 1)
            {
                Camera.main.orthographicSize--;
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0 && Camera.main.orthographicSize < 14)
            {
                Camera.main.orthographicSize++;
            }       
    }
}
