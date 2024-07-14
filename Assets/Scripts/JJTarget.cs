using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class JJTarget : MonoBehaviour
{
    AudioSource audioSource;

    public string nextScene;

    private string[] paths;

    public void Start() {
        // AssetBundle assB = AssetBundle.LoadFromFile("Assets/Scenes");
        // paths = assB.GetAllScenePaths();
        // Debug.Log(paths);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player" && GameObject.Find("JJManager").GetComponent<JJManager>().ready)
        {
            Invoke("switchScene", 3.0f);
        }
    }

    void switchScene() {
        GameObject.Find("JJManager").GetComponent<JJManager>().disableObstacles();
        SceneManager.LoadScene(sceneName:nextScene);        
    }
}
