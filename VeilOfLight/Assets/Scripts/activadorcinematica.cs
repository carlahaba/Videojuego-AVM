using System.Collections;
using UnityEngine;

public class CinematicaTemplo : MonoBehaviour
{
    [Header("Imagen que aparece como cinemática")]
    public GameObject imagenCinematica;

    [Header("Duración de la imagen en pantalla")]
    public float duracion = 5.5f;

    private bool yaActivado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!yaActivado && other.CompareTag("Player"))
        {
            yaActivado = true;
            StartCoroutine(MostrarCinematica());
        }
    }

    IEnumerator MostrarCinematica()
    {
        imagenCinematica.SetActive(true);
        yield return new WaitForSeconds(duracion);
        imagenCinematica.SetActive(false);
        Destroy(gameObject); // Para que la cinemática no se repita
    }
}