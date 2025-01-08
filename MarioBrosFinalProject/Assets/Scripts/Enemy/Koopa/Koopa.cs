using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;

public class Koopa : MonoBehaviour, IEnemy
{
    private PlayerController Mario;  // Referencia al jugador
    private Rigidbody2D rb;  // Rigidbody2D del Koopa
    private GameObject targetPrefab;

    private Animator Animator;

    private int movedir;  // Dirección de movimiento

    [SerializeField] private float speed = 2f;  // Velocidad de movimiento
    [SerializeField] private float maxDistanceToFollow = 10;
    [SerializeField] private float minDistanceToFollow = 1;
    private bool follow;

    public Collider2D superiorCollider;
    public Collider2D lowerCollider;


    public void Move()
    {
        Vector2 marioPosition = Mario.transform.position;
        Vector2 koopaPosition = transform.position;

        follow = FollowOrNot(marioPosition, koopaPosition);
        if (follow)
        {
            DecideMovement(marioPosition, koopaPosition);
        }


        transform.localScale = new Vector2(movedir, 1);
        rb.velocity = new Vector2(movedir * speed, rb.velocity.y);

    }
    public void Atack()
    {
        Mario.GetHit();
    }

    public void GetKilled()
    {
        SwitchPrefab();
    }

    public void SwitchPrefab()
    {
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;
        Destroy(gameObject);
        GameObject newPrefab = Instantiate(targetPrefab, position, rotation);
    }

    // decide if Koopa will follow mario based on x distance
    bool FollowOrNot(Vector2 marioPosition, Vector2 koopaPosition)
    {
        float xdist = Mathf.Abs(marioPosition.x - koopaPosition.x);
        if (xdist < maxDistanceToFollow && xdist > minDistanceToFollow)
        {
            return true;
        }
        else { return false; }
    }

    // set movement direction when following mario
    void DecideMovement(Vector2 marioPosition, Vector2 koopaPosition)
    {
        movedir = (int)Mathf.Sign(marioPosition.x - koopaPosition.x);
    }

    // shell animation
    private void Shell()
    {
        Animator.SetTrigger("Shell");
    }

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        Mario = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        targetPrefab = Resources.Load<GameObject>("Prefabs/Enemies/Koopa/Shell");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.otherCollider == superiorCollider && collision.gameObject.CompareTag("Player"))
        {
            GetKilled();
            Mario.Attacked = true;
        }
        else if (collision.otherCollider == lowerCollider && collision.gameObject.CompareTag("Player"))
        {
            Atack();
        }
        else if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Element"))
        {
            movedir *= -1;
        }
    }
}

