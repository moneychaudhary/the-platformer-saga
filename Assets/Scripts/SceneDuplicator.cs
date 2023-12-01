using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDuplicator : MonoBehaviour
{
    public Transform playerTransform;
    public float duplicationThreshold = 10f;

    void Update()
    {
        // Check if the player is moving up
        if (playerTransform.position.y > transform.position.y - duplicationThreshold)
        {
            DuplicateScene();
        }
    }

    void DuplicateScene()
    {
        // Duplicate the current scene
        Debug.Log("New Scene Created");
        SceneManager.LoadScene("Bonus_Scene_Endless", LoadSceneMode.Additive);

        // Adjust the position of the new scene above the current one
        Scene duplicatedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        GameObject[] rootObjects = duplicatedScene.GetRootGameObjects();

        foreach (GameObject obj in rootObjects)
        {
            obj.transform.position += new Vector3(0, duplicationThreshold, 0);
        }
    }
}

