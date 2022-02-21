using System.Collections;
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

    //get the current scene to retrieve the scene name
    private Scene scene;

    //set the name of the current scene to this variable
    public string level;

    //get a reference to the player to get the players deaths, level time, and any other variables i need from that script
    public Player player;

    //use a bool to determine if the level is done or not
    public bool levelDone;

    // Start is called before the first frame update
    public void Start()
    {
        scene = SceneManager.GetActiveScene();
        level = scene.name;
        levelDone = false;
        //level1Achievements = new List<Achievement>();
        //level2Achievements = new List<Achievement>();
        //level3Achievements = new List<Achievement>();
        //level4Achievements = new List<Achievement>();
        //level5Achievements = new List<Achievement>();
        //special = new List<Achievement>();
    }

    // Update is called once per frame
    public void Update()
    {
        Debug.Log("This is the level name: " + level);
        if(level == "Level1")
        {
            foreach (Achievement item in level1Achievements)
            {
                
                if(item.notCompleted == true)
                {
                    
                    if (item.achievementType == Achievement.Types.TIME && levelDone == true)
                    {
                       
                        if (player.levelTime < item.timeThreshold)
                        {
                            item.completed = true;
                            item.notCompleted = false;
                            Debug.Log("You completed the level in time! Congrations!");
                        }
                    }
                    else if (item.achievementType == Achievement.Types.UNDER_DEATH && levelDone == true)
                    {
                        
                        if (player.deathAmount < item.deathThreshold)
                        {
                            item.completed = true;
                            item.notCompleted = false;
                            Debug.Log("You didnt die too many times! Congrations!");
                        }
                    }
                    //else if(item.achievementType == Achievement.Types.SPECIAL)
                    //{
                    //    Debug.Log("I should be checking the special jumping achievement");
                    //    if(player.jumpAmount >= item.numOfJumpsThreshold)
                    //    {
                    //        item.completed = true;
                    //        //item.notCompleted = false;
                    //        Debug.Log("Youve jumped a lot. Here's an achievement");
                    //    }
                    //}
                }
                
            }
            foreach(Achievement item in special)
            {
                if(item.notCompleted == true)
                {
                    if (item.achievementType == Achievement.Types.SPECIAL)
                    {
                        if (player.jumpAmount >= item.numOfJumpsThreshold)
                        {
                            item.completed = true;
                            item.notCompleted = false;
                            //item.notCompleted = false;
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
