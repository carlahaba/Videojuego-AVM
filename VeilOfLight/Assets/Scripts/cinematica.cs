using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicaControl : MonoBehaviour
{
    public float duracionCinematica = 20f; // Tiempo que dura la cinemática
    public string gameplaySceneName; // Nombre de la escena de juego

    void Start()
    {
        StartCoroutine(EsperarYCambiarEscena());
    }

    IEnumerator EsperarYCambiarEscena()
    {
        yield return new WaitForSeconds(duracionCinematica);
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}