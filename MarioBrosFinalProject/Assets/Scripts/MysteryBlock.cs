using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBlock : MonoBehaviour
{
    public GameObject objectToSpawn;
    public PlayerController Mario;
    public bool alreadySpawned = false;
    public Collider2D lowerCollider;
    // Start is called before the first frame update


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !alreadySpawned && collision.otherCollider == lowerCollider)
        {
            // Spawn position is the same as this object but one unit above
            Vector3 spawnPosition = transform.position + new Vector3(0, 1, 0);

            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

            // Intentamos obtener el componente PowerUp del objeto instanciado
            PowerUp powerUp = spawnedObject.GetComponent<PowerUp>();
            if (powerUp != null)
            {
                powerUp.SetMario(Mario);
            }

            alreadySpawned = true;
        }
    }
}
