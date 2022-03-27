using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class LevelSwitcherForPlayer : MonoBehaviour
{
    public LevelLoader levelLoader;
    private GameObject timerThing;
    private Scene scene;
    public TextMeshProUGUI achievementText;
    public AchievementChecker achievements;
   
    public GameObject achievementChecker;
    public GameObject pauseMenu;
    public GameObject achievementPage;
    public GameObject player;
    public GameObject bigBulkyMan;
    public GameObject agileQuickGirl;
    public GameObject timeText;
    public GameObject wallCooldowns;
    public GameObject agileWallCooldowns;

    public Animator animation;
    public float transitionTime = 1f;
    private string completeOrNot;
    public bool ableToPause = true;
    public void Start()
    {
        //levelSelect.SetActive(false);
        pauseMenu.SetActive(false);
        
        timerThing = this.gameObject;
        scene = SceneManager.GetActiveScene();
        if (player.activeSelf || bigBulkyMan.activeSelf)
        {
            wallCooldowns.SetActive(true);
        }
        else
        {
            agileWallCooldowns.SetActive(true);
        }   
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && ableToPause)
        {
            if (player.activeSelf)
            {
                player.GetComponent<Player>().enabled = false;
            }
            else if (bigBulkyMan.activeSelf)
            {
                bigBulkyMan.GetComponent<Player>().enabled = false;
            }
            //else if (parkourMan.activeSelf)
            //{

            //}
            else
            {
                agileQuickGirl.GetComponent<Player>().enabled = false;
            }
            //turret.GetComponent<ShootAtPlayer>().enabled = false;
            //Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            ableToPause = false;
            timeText.SetActive(false);
            wallCooldowns.SetActive(false);
            agileWallCooldowns.SetActive(false);
            pauseMenu.SetActive(true);
            timerThing.SendMessage("stopCounting");
        }
    }
    public void Resume()
    {
        timeText.SetActive(true);
        if (player.activeSelf || bigBulkyMan.activeSelf)
        {
            wallCooldowns.SetActive(true);
        }
        else
        {
            agileWallCooldowns.SetActive(true);
        }
        pauseMenu.SetActive(false);
        if (player.activeSelf)
        {
            player.GetComponent<Player>().enabled = true;
        }
        else if (bigBulkyMan.activeSelf)
        {
            bigBulkyMan.GetComponent<Player>().enabled = true;
        }
        //else if (parkourMan.activeSelf)
        //{

        //}
        else
        {
            agileQuickGirl.GetComponent<Player>().enabled = true;
        }
        //turret.GetComponent<ShootAtPlayer>().enabled = true;
        //Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        ableToPause = true;
        timerThing.SendMessage("resumeCounting");
    }
    //public void LevelSelect()
    //{
    //    pauseMenu.SetActive(false);
    //    levelSelect.SetActive(true);
    //}
    public void GoToLevel1(int sceneIndex)
    {
        //achievementChecker.SendMessage("changeLevelBool", true);
        //levelLoader.LoadLevel(sceneIndex);
        StartCoroutine("WaitToLoad", sceneIndex);
    }
    public void GoToLevel2(int sceneIndex)
    {
        //achievementChecker.SendMessage("changeLevelBool", true);
        //levelLoader.LoadLevel(sceneIndex);
        StartCoroutine("WaitToLoad", sceneIndex);
    }
    public void GoToLevel3(int sceneIndex)
    {
        //achievementChecker.SendMessage("changeLevelBool", true);
        //levelLoader.LoadLevel(sceneIndex);
        StartCoroutine("WaitToLoad", sceneIndex);
    }
    public void GoToMainMenu()
    {
        levelLoader.LoadLevel(0);
    }
    public void GoBackToMainMenu(GameObject pageToTurnOff)
    {
        pageToTurnOff.SetActive(false);
        pauseMenu.SetActive(true);
    }
    public void GoToAchievementsScreen()
    {
        achievementPage.SetActive(true);
        pauseMenu.SetActive(false);
        
        if(scene.name == "Level1")
        {
            foreach(Achievement item in achievements.level1Achievements)
            {
                if (item.completed)
                {
                    completeOrNot = "Complete!";
                }
                else
                {
                    completeOrNot = "Not Complete";
                }
                achievementText.text += item.achievementName + "\n" + item.achievementDescription + " - " + completeOrNot +"\n\n";

            }
        }
        if(scene.name == "Level2")
        {
            foreach(Achievement item in achievements.level2Achievements)
            {
                if (item.completed)
                {
                    completeOrNot = "Complete!";
                }
                else
                {
                    completeOrNot = "Not Complete";
                }
                achievementText.text += item.achievementName + "\n" + item.achievementDescription + " - " + completeOrNot + "\n\n";
            }
        }
    }
    public void QuitGame()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
    //IEnumerator AnimationTransition(int sceneIndex)

    public IEnumerator WaitToLoad(int sceneIndex)
    {
        Debug.Log("Im waiting to load");
        achievementChecker.SendMessage("changeLevelBool");
        yield return new WaitForSeconds(.001f);
        levelLoader.LoadLevel(sceneIndex);
    }
}
