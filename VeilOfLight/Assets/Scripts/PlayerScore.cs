using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private int score = 0; // Puntuación inicial
    public int Score => score; // Propiedad para acceder a la puntuación

    void Start()
    {
        // Puedes inicializar la puntuación si es necesario
        score = 0;
    }

    // Detecta cuando el jugador recoge una esfera
    private void OnTriggerEnter(Collider other)
    {
        // Si la esfera tiene el tag "Collectible"
        if (other.CompareTag("Collectible"))
        {
            AddScore(); // Sumar un punto
            Destroy(other.gameObject); // Destruir la esfera recogida
        }
    }

    // Función para sumar un punto
    void AddScore()
    {
        score++; // Aumentar la puntuación en 1
        Debug.Log("Puntuación: " + score); // Mostrar la puntuación en la consola (opcional)
    }
}
