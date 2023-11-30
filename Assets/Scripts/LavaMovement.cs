using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LavaMovement : MonoBehaviour
{

    [SerializeField] float scaleSpeed = 0.1f;
    private BoxCollider2D boxCollider;
    [SerializeField] public LayerMask platform;
    [SerializeField] public LayerMask player;
    public GameObject fireEffectPrefab;
    GameObject fireEffect;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.localScale = new Vector2(transform.localScale.x,
                                       transform.localScale.y + scaleSpeed * Time.deltaTime);


        boxCollider = GetComponent<BoxCollider2D>();
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0f,
            Vector2.down,
            0.2f,
            platform);

        RaycastHit2D playerHit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0f,
            Vector2.down,
            0.2f,
            player);
        if (hit)
        {
            Destroy(hit.collider.gameObject);
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
