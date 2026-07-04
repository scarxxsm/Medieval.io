using UnityEngine;

public class Proyectil : MonoBehaviour
{
    // ¡Subimos la velocidad de 12 a 30 para que vuelen!
    public float velocidadBala = 50f; 
    public float tiempoVida = 2f; 
    private Vector2 direccionBala;

    public void SetDireccion(Vector2 nuevaDireccion)
    {
        direccionBala = nuevaDireccion.normalized;
    }

    void Start()
    {
        Destroy(gameObject, tiempoVida);
    }

    void FixedUpdate()
    {
        transform.Translate(direccionBala * velocidadBala * Time.fixedDeltaTime);
    }


}