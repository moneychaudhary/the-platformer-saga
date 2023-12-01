using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LavaMovement : MonoBehaviour
{

    [SerializeField] public float scaleSpeed = 0.02f;
    private BoxCollider2D boxCollider;
    [SerializeField] public LayerMask platform;
    [SerializeField] public LayerMask player;
    [SerializeField] public LayerMask bullet;
    [SerializeField] public LayerMask enemyBullet;
    public GameObject fireEffectPrefab;
    GameObject fireEffect;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        float scaleIncrease = scaleSpeed * Time.deltaTime;
        transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y + scaleIncrease);

        // Move the object up by half the amount it grew
        transform.position = new Vector3(transform.position.x, transform.position.y + scaleIncrease, transform.position.z);

        //transform.localScale = new Vector2(transform.localScale.x,
                                       //transform.localScale.y + scaleSpeed * Time.deltaTime);


        boxCollider = GetComponent<BoxCollider2D>();
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0f,
            Vector2.up,
            0.2f,
            platform);

        RaycastHit2D playerHit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0f,
            Vector2.up,
            0.2f,
            player);

        RaycastHit2D bulletHit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0f,
            Vector2.up,
            0.2f,
            bullet);
        RaycastHit2D enemyBulletHit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0f,
            Vector2.up,
            0.2f,
            enemyBullet);
        if (hit)
        {
            Destroy(hit.collider.gameObject);
        }

        if (bulletHit)
        {
            Destroy(bulletHit.collider.gameObject);
        }

        if (enemyBulletHit)
        {
            Destroy(enemyBulletHit.collider.gameObject);
        }

        if (playerHit)
        {
            if( playerHit.collider.gameObject.tag == "Player")
            {
                fireEffect = Instantiate(fireEffectPrefab, playerHit.transform);
                playerHit.collider.gameObject.GetComponent<PlayerMovement>().canJump = false;
                playerHit.collider.gameObject.GetComponent<PlayerMovement>().speed = 0;
                StartCoroutine(PlayerDeath());

            }
            
        }
    }

    public IEnumerator PlayerDeath()
    {
        
        yield return new WaitForSeconds(3);
        Debug.Log("Player Died");
        SceneManager.LoadScene("GameOver");

    }
}
