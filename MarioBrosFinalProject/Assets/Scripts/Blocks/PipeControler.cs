using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PipeController : MonoBehaviour
{
    public PipeController exitPipe;  // El punto de salida (donde el jugador ser� transportado)
    public float teleportTime = 0.5f;  // Tiempo de espera antes de teletransportar al jugador
    private bool canTeleport = false;  // Bandera que indica si el jugador puede entrar en la tuber�a

    // Detectar cuando el jugador entra en el �rea de la tuber�a
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canTeleport = true;  // Permitir teletransportarse si el jugador est� cerca de la tuber�a
        }
    }

    // Detectar cuando el jugador sale del �rea de la tuber�a
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canTeleport = false;  // Impedir teletransportarse si el jugador sale de la tuber�a
        }
    }

    // Actualizar la l�gica en cada frame
    void Update()
    {
        if (canTeleport && Input.GetKeyDown(KeyCode.S))  // El jugador presiona 'Arriba' para entrar
        {
            Debug.Log("Hl");
            StartCoroutine(TeleportPlayer());
        }
    }

    // Coroutine para esperar un poco antes de teletransportar al jugador
    private IEnumerator TeleportPlayer()
    {
        // Puede agregar una animaci�n o efecto de transici�n aqu�
        yield return new WaitForSeconds(teleportTime);

        // Teletransportar al jugador a la posici�n de salida de la tuber�a
        GameObject player = GameObject.FindWithTag("Player");
        Vector2 newPosition = exitPipe.transform.position;
        newPosition.y += 1;
        player.transform.position = newPosition;

        // Opcional: Puede realizar alguna animaci�n o efecto visual
    }
}