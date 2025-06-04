using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 respawnPoint; // Último punto de respawn donde el jugador reaparecerá
    private GameObject lastCheckpoint; // El checkpoint donde el jugador reaparecerá

    void Start()
    {
        // Inicializamos el respawn en la posición inicial del jugador
        respawnPoint = transform.position;
    }

    void Update()
    {
        // Opcional: Para probar el respawn manualmente (cuando presionas una tecla, por ejemplo)
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            Respawn();  // Esto puede ser útil para hacer pruebas durante el desarrollo
        }
    }

    // Detecta cuando el jugador pisa un checkpoint o cae en una zona peligrosa
    private void OnTriggerEnter(Collider other)
    {
        // Si el jugador entra en un checkpoint, actualiza el punto de respawn
        if (other.CompareTag("Checkpoint"))
        {
            lastCheckpoint = other.gameObject; // Guarda el objeto del checkpoint (no es estrictamente necesario)
            respawnPoint = other.transform.position; // Actualiza el punto de respawn con la posición del checkpoint
        }

        // Si el jugador entra en una zona peligrosa (agua, caídas, etc.), se reinicia
        if (other.CompareTag("DangerZone"))
        {
            Respawn();  // Reaparece al último checkpoint registrado
        }
    }

    // Función que respawnea al jugador en el último checkpoint
    private void Respawn()
    {
        if (lastCheckpoint != null)
        {
            transform.position = respawnPoint; // Mueve al jugador al punto de respawn
            // Aquí también puedes reiniciar otros parámetros como la salud, animaciones, etc.
        }
    }
}