using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public float velocidadBala = 12f;
    public float tiempoVida = 2f; // Se destruye sola en 2 segundos si no le da a nada
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