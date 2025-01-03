using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 offset;
    public Transform player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = player.position;
        Vector3 newPosition = new Vector3(playerPosition.x, offset.y, offset.z);
        transform.position = newPosition;
    }
}
