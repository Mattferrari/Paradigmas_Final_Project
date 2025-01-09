using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack: MonoBehaviour
{
    // movement related
    private PlayerMovement movement;
    private int perspective;

    //Attack keycode
    [SerializeField] private KeyCode attackKey;

    // fields
    private bool isFireMario = false;
    private float fireBallTimer;
    [SerializeField] private float rechargeTime;
    [SerializeField] private GameObject fireBall;
    
    public void SetFireMario(bool firemario) { isFireMario=firemario; }
    public int GetPerspective() { return perspective; }

    public void ThrowFire()
    {
        perspective = movement.GetPerspective();
        Vector3 offset = new Vector3(perspective, 0, 0);
        Vector3 spawnPosition = transform.position + offset;
        GameObject fireball = Instantiate(fireBall, spawnPosition, Quaternion.identity);
        FireBallVS fireballScript = fireball.GetComponent<FireBallVS>();
        fireballScript.Mario = this; 
    }



    public void Start()
    {
        fireBallTimer = Time.time;

        //PlayerMovement related
        movement = GetComponent<PlayerMovement>();
        perspective = movement.GetPerspective();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isFireMario && Time.time - fireBallTimer > rechargeTime)
        {
            ThrowFire();
            fireBallTimer = Time.time;
        }
    }
}