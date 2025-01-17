using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KoopaPatroopa : MonoBehaviour, IEnemy
{
    private PlayerController Mario;
    private Rigidbody2D rb;
    private GameObject targetPrefab;
    //going up(1) or down(-1)
    private int movedir = 1;
    
    //y range
    [SerializeField] private int maxy;
    [SerializeField] private int miny;

    private float speed = 2f;

    public Collider2D superiorCollider;
    public Collider2D lowerCollider;


    public void Atack()
    {
        Mario.GetHit();
    }
    
    public void GetKilled()
    {
        SwitchPrefab();
    }
    
    public void Move()
    {
        Vector2 PatroopaPosition = transform.position;
        if (PatroopaPosition.y >= maxy)
        {
            movedir = -1;
        }
        else if (PatroopaPosition.y <= miny)
        {
            movedir = 1;
        }
        rb.velocity = new Vector2(rb.velocity.x, movedir * speed);
    }


    public void SwitchPrefab()
    {
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;
        Destroy(gameObject); 
        GameObject newPrefab = Instantiate(targetPrefab, position, rotation); 
    }


    // Start is called before the first frame update
    void Start()
    {
        Mario = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        targetPrefab = Resources.Load<GameObject>("Prefabs/Enemies/Koopa/Koopa");
        if (targetPrefab != null) { Debug.Log("Prefab cargado correctamente: " + targetPrefab.name); } else { Debug.LogError("Error: No se pudo cargar el prefab."); }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (collision.otherCollider == lowerCollider) { Atack(); }
            else 
            { 
                GetKilled();
                Mario.Attacked = true;
            }
        }
        else { movedir *= -1; }
        
    }
}
