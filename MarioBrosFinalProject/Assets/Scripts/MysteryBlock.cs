using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBlock : MonoBehaviour
{
    public GameObject objectToSpawn;
    public PlayerController Mario;
    public bool alreadySpawned = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !alreadySpawned)
        {
            //Spawn position is the same as this object but one unit above
            Vector3 spawnPosition = transform.position + new Vector3(0, 1, 0);

            GameObject spawnedBuff = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            spawnedBuff.GetComponent<MushroomLogic>().SetMario(Mario);
            alreadySpawned = true;
        }
    }
}
