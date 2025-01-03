using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PipeController : MonoBehaviour
{
    public PipeController exitPipe;  // El punto de salida (donde el jugador será transportado)
    public float teleportTime = 0.5f;  // Tiempo de espera antes de teletransportar al jugador
    private bool canTeleport = false;  // Bandera que indica si el jugador puede entrar en la tubería

    // Detectar cuando el jugador entra en el área de la tubería
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canTeleport = true;  // Permitir teletransportarse si el jugador está cerca de la tubería
        }
    }

    // Detectar cuando el jugador sale del área de la tubería
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canTeleport = false;  // Impedir teletransportarse si el jugador sale de la tubería
        }
    }

    // Actualizar la lógica en cada frame
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
        // Puede agregar una animación o efecto de transición aquí
        yield return new WaitForSeconds(teleportTime);

        // Teletransportar al jugador a la posición de salida de la tubería
        GameObject player = GameObject.FindWithTag("Player");
        Vector2 newPosition = exitPipe.transform.position;
        newPosition.y += 1;
        player.transform.position = newPosition;

        // Opcional: Puede realizar alguna animación o efecto visual
    }
}