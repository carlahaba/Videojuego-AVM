using UnityEngine;
using UnityEngine.UI;

public class SombraUIManager : MonoBehaviour
{
    public Image sombraImage; // Image UI para mostrar la animación
    private int sombrasRecogidas = 0; // Contador de sombras recogidas
    private int totalSombras = 5; // Número total de sombras

    private Sprite[] sombraSprites; // Array de sprites para cada animación

    void Start()
    {
        // Al inicio, mostramos el primer frame de la animación (no se ha recogido ninguna sombra)
        LoadAnimation(1); // Carga la primera animación (en caso de que no se haya recogido ninguna sombra)
    }

    void Update()
    {
        // Solo cargar la animación correspondiente cuando haya sombras recogidas
        if (sombrasRecogidas > 0)
        {
            ShowFrame(sombrasRecogidas);
        }
    }

    // Función para cargar los sprites desde las carpetas de animación
    void LoadAnimation(int numAnimacion)
    {
        // Carga todos los sprites de la carpeta correspondiente dentro de Resources
        sombraSprites = Resources.LoadAll<Sprite>($"Palillo{numAnimacion}_PNG_FramesIndividuales");

        // Si no hay sombras recogidas, muestra el primer frame de la animación
        if (sombrasRecogidas == 0)
        {
            sombraImage.sprite = sombraSprites[0]; // Muestra el primer frame de la animación
        }
        else
        {
            sombraImage.sprite = sombraSprites[sombraSprites.Length - 1]; // Muestra el último frame si no hay sombras recogidas
        }
    }

    // Función para mostrar el último frame según el número de sombras recogidas
    void ShowFrame(int numAnimacion)
    {
        sombraImage.sprite = sombraSprites[sombraSprites.Length - 1]; // Último frame de la animación
    }

    // Función para ser llamada cuando se recoja una sombra
    public void RecogerSombra(GameObject esfera)
    {
        if (sombrasRecogidas < totalSombras)
        {
            sombrasRecogidas++;
            Destroy(esfera); // Destruye la esfera recogida
            LoadAnimation(sombrasRecogidas); // Recarga la animación correspondiente
        }
    }
}
