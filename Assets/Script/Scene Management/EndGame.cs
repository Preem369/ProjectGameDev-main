using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "Scene_EndGame";
    private float waitToLoadTime = 1f;

    public void DestroyAllDontDestroyOnLoadObjects()
    {

        var go = new GameObject("Sacrificial Lamb");
        DontDestroyOnLoad(go);

        foreach (var root in go.scene.GetRootGameObjects())
            Destroy(root);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {          
            UIFade.Instance.FadeToBlack();
            StartCoroutine(LoadSceneRoutine());
 
        }
    }

    private IEnumerator LoadSceneRoutine()
    {
        while (waitToLoadTime >= 0)
        {
            waitToLoadTime -= Time.deltaTime;
            yield return null;
        }
        DestroyAllDontDestroyOnLoadObjects();
        SceneManager.LoadScene(sceneToLoad);
        
        
    }
}
