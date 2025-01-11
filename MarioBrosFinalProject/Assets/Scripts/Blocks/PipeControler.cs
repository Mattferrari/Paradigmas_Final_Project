using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PipeController : MonoBehaviour
{
    private PlayerController player;
    public Collider2D mainCollider;
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
        if (canTeleport && Input.GetKeyDown(KeyCode.S))  // El jugador presiona 'Abajo' para entrar
        {
            //Cambiar el offset del main collider
            mainCollider.offset = new Vector2(mainCollider.offset.x, mainCollider.offset.y - 2);
            StartCoroutine(TeleportPlayer());
        }
    }

    // Coroutine para esperar un poco antes de teletransportar al jugador
    private IEnumerator TeleportPlayer()
    {
        float elapsed = 0f;
        float duration = 1f;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        player.PipeSound();
        player.canMove = false;
        Vector2 startPosition = player.transform.position;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            // Mover al jugador hacia abajo
            player.transform.position = Vector2.Lerp(startPosition, new Vector3(startPosition.x, startPosition.y - 2), t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        // Teletransportar al jugador a la posici�n de salida de la tuber�a

        Vector2 newPosition = exitPipe.transform.position;
        newPosition.y += 1;
        player.transform.position = newPosition;
        mainCollider.offset = new Vector2(mainCollider.offset.x, mainCollider.offset.y + 2);
        player.GetComponent<PlayerController>().canMove = true;

    }
}