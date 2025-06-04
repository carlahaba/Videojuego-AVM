using UnityEngine;

public class RotarAleatorio : MonoBehaviour
{
    private Vector3 rotationSpeed; // Velocidad de rotación aleatoria

    void Start()
    {
        // Asignamos una velocidad de rotación aleatoria en cada eje.
        rotationSpeed = new Vector3(
            Random.Range(-50f, 50f), // Rotación aleatoria en el eje X
            Random.Range(-50f, 50f), // Rotación aleatoria en el eje Y
            Random.Range(-50f, 50f)  // Rotación aleatoria en el eje Z
        );
    }

    void Update()
    {
        // Aplica la rotación constantemente
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
