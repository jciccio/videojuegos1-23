using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pad : MonoBehaviour
{
   
    [SerializeField] float ScreenSizeUnit = 17.78f;
    [SerializeField] float MinX = 0;
    [SerializeField] float MaxX = 17;

    // Update is called once per frame
    void Update()
    {
        // Obtener el punto en la pantalla del mouse
        // Debug.Log("Posición del mouse en X es: " + Input.mousePosition.x);

        // Cómo normalizar?
        // Debug.Log("Posicion relativa a la pantalla: " + Input.mousePosition.x/Screen.width * ScreenSizeUnit);

        float paddlePos = Input.mousePosition.x/Screen.width * ScreenSizeUnit;
        // transform.position = new Vector2(paddlePos, transform.position.y);

        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        position.x = Mathf.Clamp(paddlePos, MinX, MaxX);
        transform.position = position;
    }
}
