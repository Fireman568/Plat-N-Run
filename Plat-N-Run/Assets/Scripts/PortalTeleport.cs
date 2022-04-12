﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PortalTeleport : MonoBehaviour
{
    //public GameObject portal;
    public LevelLoader levelLoad;
    public GameObject achievementChecker;
    public GameObject resultsScreen;
    public GameObject player;
    public GameObject bigMan;
    public GameObject agileGirl;
    public GameObject parkourMan;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI bestTimeText;

    public float default0BestTime;
    public float big0BestTime;
    public float agile0BestTime;
    public float parkour0BestTime;
    public float default1BestTime;
    public float big1BestTime;
    public float agile1BestTime;
    public float parkour1BestTime;
    public float default2BestTime;
    public float big2BestTime;
    public float agile2BestTime;
    public float parkour2BestTime;
    public float bestTime;
    public float timeFromPlayer;
    public int sceneToLoad;
    public Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        resultsScreen.SetActive(false);
        PlayerPrefs.SetInt("Level " + scene.buildIndex + " Complete", 1);
        default1BestTime = PlayerPrefs.GetFloat("default1BestTime");
        big1BestTime = PlayerPrefs.GetFloat("big1BestTime");
        agile1BestTime = PlayerPrefs.GetFloat("agile1BestTime");
        parkour1BestTime = PlayerPrefs.GetFloat("parkour1BestTime");

        if (default0BestTime == 0)
        {
            default0BestTime = 1000000;
        }
        if (big0BestTime == 0)
        {
            big0BestTime = 1000000;
        }
        if (agile0BestTime == 0)
        {
            agile0BestTime = 1000000;
        }
        if (parkour0BestTime == 0)
        {
            parkour0BestTime = 1000000;
        }
        if (default1BestTime == 0)
        {
            default1BestTime = 1000000;
        }
        if(big1BestTime == 0)
        {
            big1BestTime = 1000000;
        }
        if(agile1BestTime == 0)
        {
            agile1BestTime = 1000000;
        }
        if(parkour1BestTime == 0)
        {
            parkour1BestTime = 1000000;
        }
        if (default2BestTime == 0)
        {
            default2BestTime = 1000000;
        }
        if (big2BestTime == 0)
        {
            big2BestTime = 1000000;
        }
        if (agile2BestTime == 0)
        {
            agile2BestTime = 1000000;
        }
        if (parkour2BestTime == 0)
        {
            parkour2BestTime = 1000000;
        }
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("I have been triggered by something entering me");
        if (scene.name == "Tutorial")
        {
            if (other.name == "Player")
            {
                //levelLoad.LoadLevel(sceneToLoad);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                achievementChecker.SendMessage("changeLevelBool");
                Debug.Log("I am about to turn off the player");
                player.GetComponent<Player>().enabled = false;
                timeFromPlayer = player.GetComponent<Player>().levelTime;
                if (timeFromPlayer < default0BestTime)
                {
                    bestTime = timeFromPlayer;
                    PlayerPrefs.SetFloat("default1BestTime", timeFromPlayer);
                }
                else
                {
                    bestTime = default0BestTime;
                }

                timeText.text = timeFromPlayer.ToString();
                bestTimeText.text = "Regular Guy Best Time: " + bestTime.ToString();
                resultsScreen.SetActive(true);

                //StartCoroutine("WaitToLoad", sceneToLoad);
            }
            else if (other.name == "BigBulkyMan")
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                achievementChecker.SendMessage("changeLevelBool");
                bigMan.GetComponent<Player>().enabled = false;
                timeFromPlayer = bigMan.GetComponent<Player>().levelTime;
                if (timeFromPlayer < big0BestTime)
                {
                    bestTime = timeFromPlayer;
                    PlayerPrefs.SetFloat("big1BestTime", timeFromPlayer);
                }
                else
                {
                    bestTime = big0BestTime;
                }

                timeText.text = timeFromPlayer.ToString();
                bestTimeText.text = "Bulky Man Best Time: " + bestTime.ToString();
                resultsScreen.SetActive(true);
            }
            else if (other.name == "AgileQuickGirl")
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                achievementChecker.SendMessage("changeLevelBool");
                agileGirl.GetComponent<Player>().enabled = false;
                timeFromPlayer = agileGirl.GetComponent<Player>().levelTime;
                if (timeFromPlayer < agile0BestTime)
                {
                    bestTime = timeFromPlayer;
                    PlayerPrefs.SetFloat("agile1BestTime", timeFromPlayer);
                }
                else
                {
                    bestTime = agile0BestTime;
                }

                timeText.text = timeFromPlayer.ToString();
                bestTimeText.text = "Agile Girl Best Time: " + bestTime.ToString();
                resultsScreen.SetActive(true);
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                achievementChecker.SendMessage("changeLevelBool");
                parkourMan.GetComponent<Player>().enabled = false;
                timeFromPlayer = parkourMan.GetComponent<Player>().levelTime;
                if (timeFromPlayer < parkour0BestTime)
                {
                    bestTime = timeFromPlayer;
                    PlayerPrefs.SetFloat("parkour1BestTime", timeFromPlayer);
                }
                else
                {
                    bestTime = parkour0BestTime;
                }

                timeText.text = timeFromPlayer.ToString();
                bestTimeText.text = "Parkour Guy Best Time: " + bestTime.ToString();
                resultsScreen.SetActive(true);
            }
        }
        if (scene.name == "Level1")
        {
            if (other.name == "Player")
            {
                //levelLoad.LoadLevel(sceneToLoad);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                achievementChecker.SendMessage("changeLevelBool");
                Debug.Log("I am about to turn off the player");
                player.GetComponent<Player>().enabled = false;
                timeFromPlayer = player.GetComponent<Player>().levelTime;
                if (timeFromPlayer < default1BestTime)
                {
                    bestTime = timeFromPlayer;
                    PlayerPrefs.SetFloat("default1BestTime", timeFromPlayer);
                }
                else
                {
                    bestTime = default1BestTime;
                }

                timeText.text = timeFromPlayer.ToString();
                bestTimeText.text = "Regular Guy Best Time: " + bestTime.ToString();
                resultsScreen.SetActive(true);

                //StartCoroutine("WaitToLoad", sceneToLoad);
            }
            else if (other.name == "BigBulkyMan")
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                achievementChecker.SendMessage("changeLevelBool");
                bigMan.GetComponent<Player>().enabled = false;
                timeFromPlayer = bigMan.GetComponent<Player>().levelTime;
                if (timeFromPlayer < big1BestTime)
                {
                    bestTime = timeFromPlayer;
                    PlayerPrefs.SetFloat("big1BestTime", timeFromPlayer);
                }
                else
                {
                    bestTime = big1BestTime;
                }

                timeText.text = timeFromPlayer.ToString();
                bestTimeText.text = "Bulky Man Best Time: " + bestTime.ToString();
                resultsScreen.SetActive(true);
            }
            else if (other.name == "AgileQuickGirl")
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                achievementChecker.SendMessage("changeLevelBool");
                agileGirl.GetComponent<Player>().enabled = false;
                timeFromPlayer = agileGirl.GetComponent<Player>().levelTime;
                if (timeFromPlayer < agile1BestTime)
                {
                    bestTime = timeFromPlayer;
                    PlayerPrefs.SetFloat("agile1BestTime", timeFromPlayer);
                }
                else
                {
                    bestTime = agile1BestTime;
                }

                timeText.text = timeFromPlayer.ToString();
                bestTimeText.text = "Agile Girl Best Time: " + bestTime.ToString();
                resultsScreen.SetActive(true);
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                achievementChecker.SendMessage("changeLevelBool");
                parkourMan.GetComponent<Player>().enabled = false;
                timeFromPlayer = parkourMan.GetComponent<Player>().levelTime;
                if (timeFromPlayer < parkour1BestTime)
                {
                    bestTime = timeFromPlayer;
                    PlayerPrefs.SetFloat("parkour1BestTime", timeFromPlayer);
                }
                else
                {
                    bestTime = parkour1BestTime;
                }

                timeText.text = timeFromPlayer.ToString();
                bestTimeText.text = "Parkour Guy Best Time: " + bestTime.ToString();
                resultsScreen.SetActive(true);
            }
        }
        if(scene.name == "Level2")
        {
            if (other.name == "Player")
            {
                //levelLoad.LoadLevel(sceneToLoad);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                achievementChecker.SendMessage("changeLevelBool");
                Debug.Log("I am about to turn off the player");
                player.GetComponent<Player>().enabled = false;
                timeFromPlayer = player.GetComponent<Player>().levelTime;
                if (timeFromPlayer < default2BestTime)
                {
                    bestTime = timeFromPlayer;
                    PlayerPrefs.SetFloat("default1BestTime", timeFromPlayer);
                }
                else
                {
                    bestTime = default2BestTime;
                }

                timeText.text = timeFromPlayer.ToString();
                bestTimeText.text = "Regular Guy Best Time: " + bestTime.ToString();
                resultsScreen.SetActive(true);

                //StartCoroutine("WaitToLoad", sceneToLoad);
            }
            else if (other.name == "BigBulkyMan")
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                achievementChecker.SendMessage("changeLevelBool");
                bigMan.GetComponent<Player>().enabled = false;
                timeFromPlayer = bigMan.GetComponent<Player>().levelTime;
                if (timeFromPlayer < big2BestTime)
                {
                    bestTime = timeFromPlayer;
                    PlayerPrefs.SetFloat("big1BestTime", timeFromPlayer);
                }
                else
                {
                    bestTime = big2BestTime;
                }

                timeText.text = timeFromPlayer.ToString();
                bestTimeText.text = "Bulky Man Best Time: " + bestTime.ToString();
                resultsScreen.SetActive(true);
            }
            else if (other.name == "AgileQuickGirl")
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                achievementChecker.SendMessage("changeLevelBool");
                agileGirl.GetComponent<Player>().enabled = false;
                timeFromPlayer = agileGirl.GetComponent<Player>().levelTime;
                if (timeFromPlayer < agile2BestTime)
                {
                    bestTime = timeFromPlayer;
                    PlayerPrefs.SetFloat("agile1BestTime", timeFromPlayer);
                }
                else
                {
                    bestTime = agile2BestTime;
                }

                timeText.text = timeFromPlayer.ToString();
                bestTimeText.text = "Agile Girl Best Time: " + bestTime.ToString();
                resultsScreen.SetActive(true);
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                achievementChecker.SendMessage("changeLevelBool");
                parkourMan.GetComponent<Player>().enabled = false;
                timeFromPlayer = parkourMan.GetComponent<Player>().levelTime;
                if (timeFromPlayer < parkour2BestTime)
                {
                    bestTime = timeFromPlayer;
                    PlayerPrefs.SetFloat("parkour1BestTime", timeFromPlayer);
                }
                else
                {
                    bestTime = parkour2BestTime;
                }

                timeText.text = timeFromPlayer.ToString();
                bestTimeText.text = "Parkour Guy Best Time: " + bestTime.ToString();
                resultsScreen.SetActive(true);
            }
        }
        
    }
    public void goToNextLevel(int sceneIndex)
    {
        levelLoad.LoadLevel(sceneIndex);
    }
    public void restartTheLevel()
    {
        SceneManager.LoadScene(scene.name);
    }
    //public IEnumerator WaitToLoad(int sceneIndex)
    //{
    //    Debug.Log("Im waiting to load");
        
    //    yield return new WaitForSeconds(.001f);
    //    levelLoad.LoadLevel(sceneIndex);
    //}
}
