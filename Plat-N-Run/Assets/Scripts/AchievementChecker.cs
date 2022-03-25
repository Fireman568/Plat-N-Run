﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementChecker : MonoBehaviour
{
    //lists of achievements per level as well as special achievements
    public List<Achievement> level1Achievements;
    public List<Achievement> level2Achievements;
    public List<Achievement> level3Achievements;
    public List<Achievement> level4Achievements;
    public List<Achievement> level5Achievements;
    public List<Achievement> special;

    public GameObject achievementImage;

    public bool level1_0;
    public bool level1_1;
    public bool level1_2;
    public bool level1_3;
    public bool level1_4;
    public bool level1_5;
    public bool level1_6;
    public bool level1_7;
    public bool special_0;

    //get the current scene to retrieve the scene name
    private Scene scene;

    //set the name of the current scene to this variable
    public string level;

    //get a reference to the player to get the players deaths, level time, and any other variables i need from that script
    public Player player;

    //use a bool to determine if the level is done or not
    public bool levelDone;

    public string playerName;

    // Start is called before the first frame update
    public void Start()
    {
        scene = SceneManager.GetActiveScene();
        level = scene.name;
        levelDone = false;
        playerName = player.name;
        level1_0 = PlayerPrefs.GetInt("Level1_0") == 1 ? true : false;
        level1_1 = PlayerPrefs.GetInt("Level1_1") == 1 ? true : false;
        level1_2 = PlayerPrefs.GetInt("Level1_2") == 1 ? true : false;
        level1_3 = PlayerPrefs.GetInt("Level1_3") == 1 ? true : false;
        level1_4 = PlayerPrefs.GetInt("Level1_4") == 1 ? true : false;
        level1_5 = PlayerPrefs.GetInt("Level1_5") == 1 ? true : false;
        level1_6 = PlayerPrefs.GetInt("Level1_6") == 1 ? true : false;
        level1_7 = PlayerPrefs.GetInt("Level1_7") == 1 ? true : false;
        special_0 = PlayerPrefs.GetInt("Special_0") == 1 ? true : false;
        
        //level1Achievements = new List<Achievement>();
        //level2Achievements = new List<Achievement>();
        //level3Achievements = new List<Achievement>();
        //level4Achievements = new List<Achievement>();
        //level5Achievements = new List<Achievement>();
        //special = new List<Achievement>();
        foreach(Achievement item in level1Achievements)
        {
            if(item.tag == "Level1_0" && level1_0)
            {
                item.completed = true;
                item.notCompleted = false;
            }
            if (item.tag == "Level1_1" && level1_1)
            {
                item.completed = true;
                item.notCompleted = false;
            }
            if (item.tag == "Level1_2" && level1_2)
            {
                item.completed = true;
                item.notCompleted = false;
            }
            if (item.tag == "Level1_3" && level1_3)
            {
                item.completed = true;
                item.notCompleted = false;
            }
            if (item.tag == "Level1_4" && level1_4)
            {
                item.completed = true;
                item.notCompleted = false;
            }
            if (item.tag == "Level1_5" && level1_5)
            {
                item.completed = true;
                item.notCompleted = false;
            }
            if (item.tag == "Level1_6" && level1_6)
            {
                item.completed = true;
                item.notCompleted = false;
            }
            if (item.tag == "Level1_7" && level1_7)
            {
                item.completed = true;
                item.notCompleted = false;
            }
        }
        foreach(Achievement item in special)
        {
            if (item.tag == "Special_0" && special_0)
            {
                item.completed = true;
                item.notCompleted = false;
            }
        }
    }

    // Update is called once per frame
    public void Update()
    {
        checkAchievements();
    }
    public void checkAchievements()
    {
        Debug.Log("This is the level name: " + level);
        if (level == "Level1")
        {
            foreach (Achievement item in level1Achievements)
            {

                if (item.notCompleted == true)
                {
                    Debug.Log("checking item");
                    if (item.achievementType == Achievement.Types.TIME && levelDone == true)
                    {

                        if (player.levelTime < item.timeThreshold)
                        {
                            item.completed = true;
                            item.notCompleted = false;
                            achievementImage.SetActive(true);
                            achievementImage.SendMessage("getAchievementInfo", item);
                            achievementImage.SendMessage("showAchievement", true);
                            Debug.Log("You completed the level in time! Congrations!");
                            PlayerPrefs.SetInt("Level1_0", 1);
                            Debug.Log("Achievement earned: " + item.achievementName);
                        }
                    }
                    else if (item.achievementType == Achievement.Types.ONE_COLLECTIBLE)
                    {
                        Debug.Log(player.collectiblesCollected);
                        if (player.collectiblesCollected == 1)
                        {
                            item.completed = true;
                            item.notCompleted = false;
                            achievementImage.SetActive(true);
                            achievementImage.SendMessage("getAchievementInfo", item);
                            achievementImage.SendMessage("showAchievement", true);
                            PlayerPrefs.SetInt("Level1_1", 1);
                            Debug.Log("Achievement earned: " + item.achievementName);
                        }
                    }
                    else if (item.achievementType == Achievement.Types.ALL_COLLECTIBLE)
                    {
                        if (player.collectiblesCollected == item.collectibleAmount)
                        {
                            item.completed = true;
                            item.notCompleted = false;
                            achievementImage.SetActive(true);
                            achievementImage.SendMessage("getAchievementInfo", item);
                            achievementImage.SendMessage("showAchievement", true);
                            PlayerPrefs.SetInt("Level1_2", 1);
                            Debug.Log("Achievement earned: " + item.achievementName);
                        }
                    }
                    else if (item.achievementType == Achievement.Types.UNDER_DEATH && levelDone == true)
                    {

                        if (player.deathAmount < item.deathThreshold)
                        {
                            item.completed = true;
                            item.notCompleted = false;
                            achievementImage.SetActive(true);
                            achievementImage.SendMessage("getAchievementInfo", item);
                            achievementImage.SendMessage("showAchievement", true);
                            Debug.Log("You didnt die too many times! Congrations!");
                            PlayerPrefs.SetInt("Level1_3", 1);
                            Debug.Log("Achivement earned: " + item.achievementName);
                        }
                    }
                    else if (item.achievementType == Achievement.Types.CHARACTER_PASS && levelDone == true)
                    {
                        if (item.characterName == playerName)
                        {
                            if (playerName == "Player")
                            {
                                PlayerPrefs.SetInt("Level1_4", 1);
                            }
                            else if (playerName == "Big Bulky Man")
                            {
                                PlayerPrefs.SetInt("Level1_5", 1);
                            }
                            else if (playerName == "Agile Quick Girl")
                            {
                                PlayerPrefs.SetInt("Level1_6", 1);
                            }
                            else
                            {
                                PlayerPrefs.SetInt("Level1_7", 1);
                            }
                            item.completed = true;
                            item.notCompleted = false;
                            Debug.Log("Achivement earned: " + item.achievementName);
                        }
                    }

                }

            }
            foreach (Achievement item in special)
            {
                if (item.notCompleted == true)
                {
                    if (item.achievementType == Achievement.Types.SPECIAL)
                    {
                        if (player.jumpAmount >= item.numOfJumpsThreshold)
                        {
                            item.completed = true;
                            item.notCompleted = false;
                            //item.notCompleted = false;
                            achievementImage.SetActive(true);
                            achievementImage.SendMessage("getAchievementInfo", item);
                            achievementImage.SendMessage("showAchievement", true);
                            PlayerPrefs.SetInt("Special_0", 1);
                            Debug.Log("Youve jumped a lot. Here's an achievement");
                        }
                    }
                }

            }
        }
    }
    public void changeLevelBool()
    {
        Debug.Log("I have switched the level bool");
        levelDone = !levelDone;
    }
}
