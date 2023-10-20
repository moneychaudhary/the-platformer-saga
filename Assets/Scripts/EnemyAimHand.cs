using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

public class EnemyAimHand : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform aimTransform;
    private Transform aimGunEndPoint;
    public GameObject targetPlayer;
    void Start()
    {
        
    }

    private void Awake()
    {
        aimTransform = transform.Find("EnemyAim");
        aimGunEndPoint = aimTransform.Find("EnemyGunEndPoint");
        Debug.Log(aimGunEndPoint);
    }

    // Update is called once per frame
    void Update()
    {
        HandleAim();
    }

    private void HandleAim()
    {
        Vector3 aimDirection = (targetPlayer.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }
}
