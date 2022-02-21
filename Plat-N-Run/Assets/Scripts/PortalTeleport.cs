using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTeleport : MonoBehaviour
{
    //public GameObject portal;
    public LevelLoader levelLoad;
    public GameObject achievementChecker;
    public int sceneToLoad;
    public Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        PlayerPrefs.SetInt("Level " + scene.buildIndex + " Complete", 1);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //levelLoad.LoadLevel(sceneToLoad);
            StartCoroutine("WaitToLoad", sceneToLoad);
        }
    }
    public IEnumerator WaitToLoad(int sceneIndex)
    {
        Debug.Log("Im waiting to load");
        achievementChecker.SendMessage("changeLevelBool");
        yield return new WaitForSeconds(.001f);
        levelLoad.LoadLevel(sceneIndex);
    }
}
