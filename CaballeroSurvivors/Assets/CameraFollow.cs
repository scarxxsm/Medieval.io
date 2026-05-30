using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform objetivo; // Aquí arrastraremos al Jugador
    public float suavizado = 5f; // Qué tan suave lo sigue
    public Vector3 desfase = new Vector3(0f, 0f, -10f); // Mantiene la cámara atrás para que no se pegue

    void LateUpdate()
    {
        if (objetivo != null)
        {
            // Calculamos la posición a la que debe ir la cámara
            Vector3 posicionDeseada = objetivo.position + desfase;
            // Movemos la cámara de forma fluida hacia allá
            Vector3 posicionSuave = Vector3.Lerp(transform.position, posicionDeseada, suavizado * Time.deltaTime);
            transform.position = posicionSuave;
        }
    }
}