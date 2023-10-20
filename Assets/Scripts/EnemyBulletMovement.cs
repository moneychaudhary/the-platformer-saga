using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMovement : MonoBehaviour
{
 
    float fireRate;
    float nextFire;
    public int speed = 20;
    public GameObject BulletPrefab;
    public GameObject startLocation;
    public GameObject targetPlayer;
    Vector2 moveDir;
    void Start()
    {
        fireRate = 1.2f;
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        checkIfTimeToFire();
    }


    void checkIfTimeToFire()
    {
        if(Time.time > nextFire)
        {
            GameObject bullet = Instantiate(BulletPrefab, startLocation.transform.position, Quaternion.Euler(new Vector3(startLocation.transform.rotation.x, startLocation.transform.rotation.y, startLocation.transform.rotation.z - 90)));
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Renderer bulletSprite = bullet.GetComponent<Renderer>();
            bulletSprite.material.color = Color.green;
            moveDir = (targetPlayer.transform.position - transform.position).normalized * speed;
            if(moveDir.x > 0)
            {
                moveDir.x = 1;
            }
            rb.velocity = new Vector2(moveDir.x, moveDir.y);
            nextFire = Time.time + fireRate;
        }
    }
}
