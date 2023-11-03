using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class FireBullet : MonoBehaviour
{
    [SerializeField] private PlayerAimHand playerAimHand;
    private void Start()
    {
        playerAimHand.OnShoot += PlayerAimHand_OnShoot;
    }


    private void PlayerAimHand_OnShoot(object sender, PlayerAimHand.OnShootEventArgs e) {

    }




}
