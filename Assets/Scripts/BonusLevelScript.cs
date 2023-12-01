using System.Collections;
using UnityEngine;

public class BonusLevelScript : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    public LayerMask platform;

    bool slowDebuf = false;
    bool lavaDebuf = false;
    bool speedBall = false;

    public GameObject player;
    public GameObject lava;
    public GameObject prize;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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

            string abilityName = hit.transform.GetComponent<ColorChanger>().colors[hit.transform.GetComponent<ColorChanger>().currentColorIndex].colorName;

            switch (abilityName)
            {
                case "Freeze_Enemy":  if(!slowDebuf) StartCoroutine(SlowPlayer()); break;
                case "Double_Damage": if(!lavaDebuf) StartCoroutine(SpeedLava()); break;
                case "One-Hit KO": if (!speedBall) StartCoroutine(SpeedBall()); break;
                default: break;
            }

        }
    }

    public IEnumerator SlowPlayer()
    {
        Debug.Log("Player Slowed!!!");
        slowDebuf = true;
        float temp = player.GetComponent<PlayerMovement>().speed;
        player.GetComponent<PlayerMovement>().speed = temp * 0.75f;
        yield return new WaitForSeconds(5);
        player.GetComponent<PlayerMovement>().speed = temp;
        slowDebuf = false;
    }

    public IEnumerator SpeedLava()
    {
        Debug.Log("Lava is fast!!!!");
        lavaDebuf = true;
        float temp = lava.GetComponent<LavaMovement>().scaleSpeed;
        lava.GetComponent<LavaMovement>().scaleSpeed = temp * 2;
        yield return new WaitForSeconds(5);
        lava.GetComponent<LavaMovement>().scaleSpeed = temp;
        lavaDebuf = false;
    }

    public IEnumerator SpeedBall()
    {
        Debug.Log("Better catch the ball!!!!");
        speedBall = true;
        float temp = prize.GetComponent<PrizeMovement>().verticalSpeed;
        prize.GetComponent<PrizeMovement>().verticalSpeed = temp * 2;
        yield return new WaitForSeconds(5);
        prize.GetComponent<PrizeMovement>().verticalSpeed = temp;
    }


}
