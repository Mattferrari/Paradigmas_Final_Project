using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleFlag : MonoBehaviour
{
    private PlayerController player;
    public Collider2D extraLifeCollider;
    public Collider2D mainCollider;
    private GameManager gameManager;
    private bool levelPassed = false;
    public GameObject flag;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!levelPassed)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //gameManager.RemoveAllEnemies();
                StartCoroutine(PlayerDown());
                StartCoroutine(FlagUp());
                levelPassed = true;
            }
            if (collision.otherCollider == extraLifeCollider)
            {
                gameManager.GainLife();
                // Imagen 1UP
            }
            
        }
        
    }

    private IEnumerator PlayerDown()
    {
        float elapsedPlayer = 0f;
        float durationPlayer = 2f;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        player.FlagSound();
        player.canMove = false;
        Vector2 startPositionPlayer = player.transform.position;

        while (elapsedPlayer < durationPlayer)
        {
            float t = elapsedPlayer / durationPlayer;
            // Mover al jugador desde su posicion hasta la parte de abajo del objeto actual
            player.transform.position = Vector2.Lerp(startPositionPlayer, new Vector3(startPositionPlayer.x, 1.5f), t);
            elapsedPlayer += Time.deltaTime;

            yield return null;
        }

        player.GetComponent<PlayerController>().canMove = true;
        //Cambiamos de posicion el collider
        mainCollider.offset = new Vector2(mainCollider.offset.x - 0.8f, mainCollider.offset.y);
    }
    private IEnumerator FlagUp()
    {
        float elapsedFlag = 0f;
        float durationFlag = 2f;
        Vector2 startPositionFlag = flag.transform.position;
        GameObject player = GameObject.FindWithTag("Player");
        Vector2 positionPlayer = player.transform.position;

        while (elapsedFlag < durationFlag)
        {
            float t = elapsedFlag / durationFlag;
            // Mover la bandera tan arriba como la altura a la que haya saltado el jugador en el mástil
            flag.transform.position = Vector2.Lerp(startPositionFlag, new Vector3(startPositionFlag.x, startPositionFlag.y + positionPlayer.y - 1.5f), t);
            elapsedFlag += Time.deltaTime;

            yield return null;
        }
    }
}
