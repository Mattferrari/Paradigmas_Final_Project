using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;

public class KoopaMovement : MonoBehaviour, IEnemy
{
    public PlayerController mario;  // Referencia al jugador
    public Rigidbody2D rb;  // Rigidbody2D del Koopa

    private Animator Animator;

    public bool shell = false;  // Determina si Koopa est� en su estado de c�scara
    private int move;  // Direcci�n de movimiento
    private float speed = 2f;  // Velocidad de movimiento
    private float time;  // Tiempo en el que Koopa se convierte en c�scara
    private float timeBeingShell = 5f;  // Tiempo que Koopa pasa en su estado de c�scara
    private float maxDistanceToFollow = 10;
    private bool follow;
    private int speedBooster = 5;

    public Collider2D superiorCollider;
    public Collider2D lowerCollider;


    public void Move()
    {
        
    }
    public void Atack()
    {

    }

    public void GetKilled() 
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        time = -1f;  // Inicializamos time a un valor negativo para que no se active en el primer frame
        move = -1;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 marioPosition = mario.transform.position;
        Vector2 koopaPosition = transform.position;  // Obtener la posici�n de Koopa correctamente

        follow = FollowOrNot(marioPosition, koopaPosition);

        if (!shell)
        {
            move = DecideMovement(marioPosition, koopaPosition);
            Move(move);
        }
        else
        {
            // Si el tiempo transcurrido desde que Koopa se convirti� en c�scara supera el tiempo establecido,
            // Koopa dejar� el estado de c�scara
            if (Time.time - time > timeBeingShell)
            {
                Shell();
                time = -1f;  // Resetear el tiempo
            }
        }
    }

    bool FollowOrNot(Vector2 marioPosition, Vector2 koopaPosition)
    {
        if (Mathf.Abs(marioPosition.x - koopaPosition.x) < maxDistanceToFollow)
        {
            return true;
        }
        else { return false; }
    }
    // Decide si Koopa debe moverse hacia la izquierda o derecha en funci�n de la posici�n de Mario
    int DecideMovement(Vector2 marioPosition, Vector2 koopaPosition)
    {
        
        if (marioPosition.x > koopaPosition.x && follow)
        {
            if (move == -1)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            move = 1;  // Koopa se mueve hacia la derecha si Mario est� a la derecha
        }
        else if (follow)
        {
            if (move == 1)
            {
                transform.localScale = new Vector2(1, 1);
            }
            move = -1;  // Koopa se mueve hacia la izquierda si Mario est� a la izquierda
        }
        if (!follow) 
        { 
            move = move;
        }

        return move;
    }

    // Mueve a Koopa en la direcci�n determinada
    private void Move(int move)
    {
        rb.velocity = new Vector2(move * speed, rb.velocity.y);  // Solo modificamos el movimiento en el eje X
    }

    // Activa o desactiva el estado de c�scara de Koopa
    private void Shell()
    {
        Animator.SetTrigger("Shell");
        shell = !shell;
        if (!shell)
        {
            move = move / speedBooster;
        }
    }

    // Se llama cuando Koopa colisiona con otro objeto
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider == superiorCollider && collision.gameObject.CompareTag("Player") && shell)
        {
            // Si Koopa est� en su estado de c�scara y colisiona con Mario, Koopa es destruido
            Destroy(gameObject);
            mario.Attacked = true;
        }
        else if (collision.otherCollider == superiorCollider && collision.gameObject.CompareTag("Player") && !shell)
        {
            // Si Koopa no est� en su estado de c�scara y colisiona con Mario, se convierte en c�scara
            Shell();
            time = Time.time;  // Almacenamos el tiempo en el que Koopa se convierte en c�scara
            mario.Attacked = true;
        }
        else if (collision.otherCollider == lowerCollider && collision.gameObject.CompareTag("Player") && !shell)
        {
            mario.GetHit();
        }
        else if (collision.otherCollider == lowerCollider && collision.gameObject.CompareTag("Player") && shell)
        {
            move = -speedBooster*mario.move;
            Move(move);
        }
        else if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Element"))
        {
            if (!shell)
            {
                move = -move;
                Move(move);
                if (move == -1)
                {
                    transform.localScale = new Vector2(-1, 1);
                }
                else if (move == 1)
                {
                    transform.localScale = new Vector2(1, 1);
                }
            }
            else
            {
                move = -speedBooster;
            }
            
        }
    }
}

