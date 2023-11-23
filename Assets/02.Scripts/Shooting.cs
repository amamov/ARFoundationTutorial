using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject Bullet;
    public int bulletSpeed;

    private float FireTimer = 1.0f;

    private void Fire()
    {
        GameObject bullet = Instantiate(Bullet);
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Camera.main.transform.rotation;

        bullet.GetComponent<Rigidbody>().AddForce(transform.forward* bulletSpeed);
    }


    void Update()
    {
        if (Input.touchCount > 0 || Input.GetMouseButton(0)) 
        {
            FireTimer += Time.deltaTime;
            if (FireTimer > 1.0f) 
            {
                Fire();
                FireTimer = 0.0f;
            }
        }
    }
}
