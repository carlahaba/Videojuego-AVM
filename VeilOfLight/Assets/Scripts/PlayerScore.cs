using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public SombraUIManager sombraUIManager; // Referencia al SombraUIManager

    private void OnTriggerEnter(Collider other)
    {
        // Si la esfera tiene el tag "Collectible" (las sombras)
        if (other.CompareTag("Collectible"))
        {
            sombraUIManager.RecogerSombra(other.gameObject); // Llama a RecogerSombra y pasa la esfera
        }
    }
}
