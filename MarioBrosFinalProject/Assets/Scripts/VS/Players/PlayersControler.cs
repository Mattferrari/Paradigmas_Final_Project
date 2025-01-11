using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayersController : MonoBehaviour
{
    // objects
    private Animator Animator;

    // Enemy
    private PlayersController enemy;
    private BoxCollider2D enemyUpper;
    private BoxCollider2D enemyLower;

    // movement related
    private PlayerMovement movement;
    private bool canMove = true;

    // colliders
    [SerializeField] private BoxCollider2D upperCollider;
    public BoxCollider2D GetUpperCollider() { return upperCollider; }

    [SerializeField] private BoxCollider2D lowerCollider;
    public BoxCollider2D GetLowerCollider() { return lowerCollider; }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene("MainMenu");
    }

    public void Die()
    {
        // Destroy colliders
        Destroy(upperCollider);
        Destroy(lowerCollider);

        // Destroy rigidbody
        Destroy(GetComponent<Rigidbody2D>());

        //Wait one second and continue
        StartCoroutine(DeadAnimation());

    }

    IEnumerator DeadAnimation()
    {
        Animator.SetBool("MarioDead", true);
        yield return new WaitForSeconds(2);
        canMove = false;
        GameOver();
    }

    public PlayersController GetEnemy()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            if (player != this.gameObject)
            {
                enemy = player.GetComponent<PlayersController>();
                break;
            }
        }
        return enemy;
    }

    void Start()
    {
        PlayersController enemy = GetEnemy();

        // get enemy colliders
        enemyUpper = enemy.GetUpperCollider();
        enemyLower = enemy.GetLowerCollider();

        // get Managers
        Animator = GetComponent<Animator>();

        //PlayerMovement related
        movement = GetComponent<PlayerMovement>();
        movement.SetCanMove(canMove);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == enemyLower && collision.otherCollider == upperCollider)
        {
            Die();
        }
    }
}