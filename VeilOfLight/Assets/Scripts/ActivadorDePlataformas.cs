using UnityEngine;

public class ActivadorDePlataformas : MonoBehaviour
{
    public string tagPlataformas = "PlataformaSombra"; // Tag para identificar plataformas

    // Referencia al material que tendrán las plataformas activadas
    public Material materialNormal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject[] plataformas = GameObject.FindGameObjectsWithTag(tagPlataformas);

            foreach (GameObject plataforma in plataformas)
            {
                // Activa el collider
                Collider col = plataforma.GetComponent<Collider>();
                if (col != null)
                    col.enabled = true;

                // Cambia el material
                Renderer rend = plataforma.GetComponent<Renderer>();
                if (rend != null && materialNormal != null)
                    rend.material = materialNormal;
            }

            // Desactiva el recogible
            gameObject.SetActive(false);
        }
    }
}
