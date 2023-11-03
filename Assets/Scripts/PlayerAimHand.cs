using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;
using UnityEngine.UI;

public class PlayerAimHand : MonoBehaviour
{
    private Transform aimTransform;
    float fireRate;
    float nextFire;
    float reloadNextFire;
    private Transform aimGunEndPoint;
    public Text bulletCountText;
    public int bulletCount = 30;
    public int emptyBulletCount = 0;
    bool emptyBullet = false;
    [SerializeField] public LayerMask platform;
    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
    }
    public int speed = 3;
    public GameObject BulletPrefab;
    public GameObject startLocation;
    private Color bulletColor;
    private BoxCollider2D boxCollider;
    public int[] abilityCount = new int[5];

    private void Start()
    {
        fireRate = 0.45f;
        nextFire = Time.time;
        reloadNextFire = Time.time;
        bulletCountText.text = bulletCount.ToString();
        emptyBulletCount = 0;
    }
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
        HandleEmptyBullet();
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
            if (Time.time > nextFire)
            {
                shoot();
                nextFire = Time.time + fireRate;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("BulletReload")) {
            Destroy(collision.gameObject);
             HandleReload();
        }
        if (collision.gameObject.CompareTag("HealthReload")) {
            Destroy(collision.gameObject);
            HealthReload();
        }
    }

    public void HandleReload()
    {
       bulletCount = Math.Min(bulletCount + 3, 30);
       bulletCountText.text = bulletCount.ToString();
    }

    public void HealthReload()
    {
        if(Health.playerHealth < 1.0f)
        {
           Health.playerHealth = Math.Min(Health.playerHealth + 0.2f, 1.0f);
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
        if (hit && bulletCount > 0)
        {
            GameObject bullet = Instantiate(BulletPrefab, startLocation.transform.position, Quaternion.Euler(new Vector3(startLocation.transform.rotation.x, startLocation.transform.rotation.y, startLocation.transform.rotation.z - 90)));
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Renderer bulletSprite = bullet.GetComponent<Renderer>();
            bulletColor = hit.transform.GetComponent<Renderer>().material.color;
            Debug.Log(bulletColor);
            bulletSprite.material.color = bulletColor;
            
            rb.velocity = startLocation.transform.right * speed;
            bulletCount--;
            bulletCountText.text = bulletCount.ToString();

            string abilityName = hit.transform.GetComponent<ColorChanger>().colors[hit.transform.GetComponent<ColorChanger>().currentColorIndex].colorName;
            string formEntry = "";

            int analyticAbilty = 0;

            switch(abilityName)
            {
                case "Freeze_Enemy": formEntry = "entry.65825415"; analyticAbilty = ++abilityCount[0];  ; break;
                case "Gain_Bullet": formEntry = "entry.898798054"; analyticAbilty = ++abilityCount[1]; break;
                case "Double_Damage": formEntry = "entry.1750617544"; analyticAbilty = ++abilityCount[2]; ; break;
                case "Gain_Health": formEntry = "entry.1606122053"; analyticAbilty = ++abilityCount[3]; break;
                case "Immune": formEntry = "entry.394612529"; analyticAbilty = ++abilityCount[4]; break;
                default: break;
            }

            sendData(formEntry, analyticAbilty);
        }
            
    }

    public void sendData(string formEntry, int analyticAbilty)
    {
        if(formEntry!="")
        {
            GameObject analytics = GameObject.Find("Analytics");
            if(analytics)
            {
                analytics.GetComponent<GoogleFormUploader>().RecordData(formEntry,analyticAbilty);
            }
        }
    }

    void HandleEmptyBullet()
    {
        if(!emptyBullet &&  bulletCount<=0)
        {
            emptyBulletCount++;
            emptyBullet = !emptyBullet;
        }
    }

}
