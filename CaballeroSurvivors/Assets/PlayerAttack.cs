using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject balaPrefab;
    public float tiempoEntreAtaques = 1.5f; // Dispara cada segundo y medio
    private float cronometroAtaque = 0f;

    void Update()
    {
        cronometroAtaque += Time.deltaTime;

        if (cronometroAtaque >= tiempoEntreAtaques)
        {
            AtacarAlMasCercano();
            cronometroAtaque = 0f;
        }
    }

    void AtacarAlMasCercano()
    {
        if (balaPrefab == null) return;

        // Buscamos todos los objetos que tengan el Tag "Enemigo"
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemigo");
        if (enemigos.Length == 0) return; 

        // Encontrar cuál es el enemigo que está más cerca del jugador
        GameObject enemigoMasCercano = null;
        float distanciaMinima = Mathf.Infinity;

        foreach (GameObject enemigo in enemigos)
        {
            float distancia = Vector2.Distance(transform.position, enemigo.transform.position);
            if (distancia < distanciaMinima)
            {
                distanciaMinima = distancia;
                enemigoMasCercano = enemigo;
            }
        }

        // Si encontramos uno, le disparamos
        if (enemigoMasCercano != null)
        {
            GameObject balaObj = Instantiate(balaPrefab, transform.position, Quaternion.identity);
            Vector2 direccion = (enemigoMasCercano.transform.position - transform.position).normalized;

            Proyectil scriptBala = balaObj.GetComponent<Proyectil>();
            if (scriptBala != null)
            {
                scriptBala.SetDireccion(direccion);
            }
        }
    }
}