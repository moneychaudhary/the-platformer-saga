using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

public class PlayerAimHand : MonoBehaviour
{
    private Transform aimTransform;
    private Transform aimGunEndPoint;
    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
    }
    public int speed = 3;
    public GameObject BulletPrefab;
    public GameObject startLocation;
    private void Awake()
    {
        aimTransform = transform.Find("Aim");
        aimGunEndPoint = aimTransform.Find("GunEndPoint");
        Debug.Log(aimGunEndPoint);
}

    private void Update()
    {
        HandleAim();
        HandleShooting();
    }

    private void HandleAim()
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        angle = Math.Min(Math.Max(angle, -90), 90);
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
            OnShoot?.Invoke(this, new OnShootEventArgs
            {
                gunEndPointPosition = aimGunEndPoint.position,
                shootPosition = mousePosition,
            });
            shoot();
        }
    }

    public void shoot()
    {
        Debug.Log(aimGunEndPoint.position);
        Debug.Log(startLocation.transform.position);
        Debug.Log(startLocation.transform.rotation);
        GameObject bullet = Instantiate(BulletPrefab, startLocation.transform.position, Quaternion.Euler(new Vector3(startLocation.transform.rotation.x, startLocation.transform.rotation.y, startLocation.transform.rotation.z  - 90)));
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        SpriteRenderer bulletSprite = bullet.GetComponent<SpriteRenderer>();
        bulletSprite.color = Color.red;
        rb.velocity = startLocation.transform.right * speed;
    }

}
