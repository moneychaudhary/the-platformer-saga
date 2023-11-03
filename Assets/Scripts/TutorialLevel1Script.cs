using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;
using UnityEngine.UI;

public class TutorialLevel1Script : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform aimTransform;
    float fireRate;
    float nextFire;
    float reloadNextFire;
    private Transform aimGunEndPoint;
    [SerializeField] public LayerMask platform;

    public int speed = 19;
    public GameObject BulletPrefab;
    public GameObject startLocation;
    private Color bulletColor;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        fireRate = 0.45f;
        nextFire = Time.time;
        reloadNextFire = Time.time;
    }
    private void Awake()
    {
        aimTransform = transform.Find("Aim");
        aimGunEndPoint = aimTransform.Find("GunEndPoint");
        Debug.Log(aimGunEndPoint);
    }

    // Update is called once per frame
    void Update()
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
            if (Time.time > nextFire)
            {
                shoot();
                nextFire = Time.time + fireRate;
            }
        }
    }

    public void shoot()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0f,
            Vector2.down,
            0.2f,
            platform);
        if (hit)
        {
            GameObject bullet = Instantiate(BulletPrefab, startLocation.transform.position, Quaternion.Euler(new Vector3(startLocation.transform.rotation.x, startLocation.transform.rotation.y, startLocation.transform.rotation.z - 90)));
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Renderer bulletSprite = bullet.GetComponent<Renderer>();
            bulletColor = hit.transform.GetComponent<Renderer>().material.color;
            Debug.Log(bulletColor);
            bulletSprite.material.color = bulletColor;

            rb.velocity = startLocation.transform.right * speed;

            string abilityName = hit.transform.GetComponent<ColorChanger>().colors[hit.transform.GetComponent<ColorChanger>().currentColorIndex].colorName;
            string formEntry = "";

            int analyticAbilty = 0;
            sendData(formEntry, analyticAbilty);
        }
    }

        public void sendData(string formEntry, int analyticAbilty)
        {
            if (formEntry != "")
            {
                GameObject analytics = GameObject.Find("Analytics");
                if (analytics)
                {
                    analytics.GetComponent<GoogleFormUploader>().RecordData(formEntry, analyticAbilty);
                }
            }
        }

}
