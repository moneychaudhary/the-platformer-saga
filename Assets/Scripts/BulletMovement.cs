using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public GameObject BulletPrefab;
    public GameObject startLocation;
    public int speed = 10;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shoot();
        }
    }

    public void shoot()
    {
        Vector3 position = new Vector3(startLocation.transform.position.x, startLocation.transform.position.y + 1, startLocation.transform.position.z);
        GameObject bullet = Instantiate(BulletPrefab, startLocation.transform.position, startLocation.transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = startLocation.transform.up * speed;
    }
}
