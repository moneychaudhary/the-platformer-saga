using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
       SceneManager.LoadScene("GameOver");
    }


    private void OnCollisionEnter2D(Collision2D collision) {
    }

    public void ReloadGame() {
         SceneManager.LoadScene("Start");
    }

    public void PlayTutorial() {
         SceneManager.LoadScene("Tutorial-Level 1");
    }

    public void SelectLevel() {
         SceneManager.LoadScene("SelectLevel");
    }

    public void StartLevel1() {
         SceneManager.LoadScene("Level 1");
    }

    public void StartLevel2() {
         SceneManager.LoadScene("Level 2");
    }

    public void StartLevel3() {
         SceneManager.LoadScene("Level 3");
    }

    public void StartLevel4() {
         SceneManager.LoadScene("Level 4");
    }

    public void StartLevel5() {
         SceneManager.LoadScene("Level 5");
    }

}
