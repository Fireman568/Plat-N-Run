using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class LevelSwitcher : MonoBehaviour
{
    public LevelLoader levelLoader;
    public Scene scene;

    public TextMeshProUGUI characterInfo;
    public TextMeshProUGUI defaultGuyInfo;
    public TextMeshProUGUI bigGuyInfo;
    public TextMeshProUGUI agileGirlInfo;
    public TextMeshProUGUI parkourManInfo;
    //actual pages on the menu
    public GameObject settings;
    public GameObject levelSelect;
    public GameObject mainMenu;
    public GameObject credits;
    public GameObject stats;
    public GameObject characters;

    //buttons to go to the specified level on the level select screen
    public GameObject level1Button;
    public GameObject level2Button;
    public GameObject level3Button;

    public GameObject nextLevelButton;

    //achievementChecker object that holds the achievements
    

    //making this just in case the one above this one is tied to something that i dont know about
    public AchievementChecker achievements;


    public Animator transition;
    public TextMeshProUGUI collectiblesAmount;
    public float transitionTime;
    public float amountOfCollectibles; //whyd i do it like that?


    //for switching the text of which achievements to show, and the text of which level you are currently looking at
    public ToggleGroup levelsForStats;
    public Toggle level1;
    public Toggle level2;
    public Toggle level3;
    public string completeOrNot;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI achievementsText;
    public TextMeshProUGUI defaultGuyTxt;
    public TextMeshProUGUI bigGuyTxt;
    public TextMeshProUGUI agileGirlTxt;
    public TextMeshProUGUI parkourGuyTxt;

    //this is for the info on the characters screen to be turned on per character


    //for displaying the level buttons depending on whether or not they have been completed yet.
    public bool level1Complete;
    public bool level2Complete;
    public bool level3Complete;

    //for displaying the amount of collectibles collected
    public bool collectible1Collected;
    public bool collectible2Collected;
    public bool collectible3Collected;
    public bool collectible4Collected;
    public bool collectible5Collected;
    public bool collectible6Collected;

    //this is to hold the best time value and put assign them to the UI text stuff
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

    //this is to hold which level to go to once a character has been picked to be used in the level that the player picked
    public int levelToGoTo;
    //this is to hold which character the player picked to use in the level that they picked to go through. its an int for easier setting 
    public int character;
    
    public void Start()
    {
        mainMenu.SetActive(true);
        settings.SetActive(false);
        characters.SetActive(false);
        levelSelect.SetActive(false);
        stats.SetActive(false);
        levelSelect.SetActive(false);
        scene = SceneManager.GetActiveScene();
        level1Complete = PlayerPrefs.GetInt("Level 1 Complete") == 1 ? true : false;
        level2Complete = PlayerPrefs.GetInt("Level 2 Complete") == 1 ? true : false;
        level3Complete = PlayerPrefs.GetInt("Level 3 Complete") == 1 ? true : false;
        collectible1Collected = PlayerPrefs.GetInt("Collectible 1 Collected") == 1 ? true : false;
        collectible2Collected = PlayerPrefs.GetInt("Collectible 2 Collected") == 1 ? true : false;
        collectible3Collected = PlayerPrefs.GetInt("Collectible 3 Collected") == 1 ? true : false;
        collectible4Collected = PlayerPrefs.GetInt("Collectible 4 Collected") == 1 ? true : false;
        collectible5Collected = PlayerPrefs.GetInt("Collectible 5 Collected") == 1 ? true : false;
        collectible6Collected = PlayerPrefs.GetInt("Collectible 6 Collected") == 1 ? true : false;
        amountOfCollectibles = PlayerPrefs.GetInt("amountOfCollectibles");
        collectiblesAmount.text = "Collectibles collected: " + amountOfCollectibles;
        default0BestTime = PlayerPrefs.GetFloat("default0BestTime");
        big0BestTime = PlayerPrefs.GetFloat("big0BestTime");
        agile0BestTime = PlayerPrefs.GetFloat("agile0BestTime");
        parkour0BestTime = PlayerPrefs.GetFloat("parkour0BestTime");
        default1BestTime = PlayerPrefs.GetFloat("default1BestTime");
        big1BestTime = PlayerPrefs.GetFloat("big1BestTime");
        agile1BestTime = PlayerPrefs.GetFloat("agile1BestTime");
        parkour1BestTime = PlayerPrefs.GetFloat("parkour1BestTime");
        default2BestTime = PlayerPrefs.GetFloat("default2BestTime");
        big2BestTime = PlayerPrefs.GetFloat("big2BestTime");
        agile2BestTime = PlayerPrefs.GetFloat("agile2BestTime");
        parkour2BestTime = PlayerPrefs.GetFloat("parkour2BestTime");
        
        
        //i dont have this turning on in the start method because it gets taken care of in another function
        //if (level1Complete)
        //{
        //    level1Button.SetActive(true);
        //}
        //else
        //{
        //    level1Button.SetActive(false);
        //}

        if (level2Complete)
        {
            level2Button.SetActive(true);
        }
        else
        {
            level2Button.SetActive(false);
        }

        if (level3Complete)
        {
            level3Button.SetActive(true);
        }
        else
        {
            level3Button.SetActive(false);
        }

    }
    
    public void GoToLevel1()
    {
        //levelLoader.LoadLevel(sceneIndex);
        levelToGoTo = 1;
        goToCharacters();
    }
    public void GoToLevel2()
    {
        //levelLoader.LoadLevel(sceneIndex);
        levelToGoTo = 2;
        goToCharacters();
    }
    public void GoToLevel3()
    {
        //levelLoader.LoadLevel(sceneIndex);
        levelToGoTo = 3;
        goToCharacters();
    }

    public void GoBackToMainMenu(GameObject pageToTurnOff)
    {
        pageToTurnOff.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void GoToSettings()
    {
        mainMenu.SetActive(false);
        settings.SetActive(true);
    }
    public void GoToCredits()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }
    
    public void goToLevel()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(true);
        level1Button.SetActive(true);
    }
    private void goToCharacters()
    {
        
        levelSelect.SetActive(false);
        characters.SetActive(true);
    }
    public void loadLevel()
    {
        levelLoader.LoadLevel(levelToGoTo);
    }
    public void QuitTheGame()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
    public void turnOnDefault()
    {
        characterInfo.text = defaultGuyInfo.text;
        PlayerPrefs.SetInt("characterPick", 1);
        nextLevelButton.SetActive(true);
    }
    public void turnOnBig()
    {
        characterInfo.text = bigGuyInfo.text;
        PlayerPrefs.SetInt("characterPick", 2);
        nextLevelButton.SetActive(true);
    }
    public void turnOnAgile()
    {
        characterInfo.text = agileGirlInfo.text;
        PlayerPrefs.SetInt("characterPick", 3);
        nextLevelButton.SetActive(true);
    }
    public void turnOnParkour()
    {
        characterInfo.text = parkourManInfo.text;
        PlayerPrefs.SetInt("characterPick", 4);
        nextLevelButton.SetActive(true);
    }
    public void GoBackToLevelSelectFromCharacterSelect()
    {
        characters.SetActive(false);
        levelSelect.SetActive(true);
    }
    public void GoToStatsScreen()
    {
        stats.SetActive(true);
        mainMenu.SetActive(false);
        achievementsText.text = "";

        foreach (Achievement item in achievements.level3Achievements)
        {
            if (item.completed)
            {
                Debug.Log("the item is completed");
                completeOrNot = "Complete!";
            }
            else
            {
                Debug.Log("The item is not completed");
                completeOrNot = " Not Complete.";
            }
            achievementsText.text += item.achievementName + "\n" + item.achievementDescription + "\n" + completeOrNot + "\n\n";

           
        }
        defaultGuyTxt.text = "Default guy best time: " + default0BestTime.ToString();
        bigGuyTxt.text = "Big Man best time: " + big0BestTime.ToString();
        agileGirlTxt.text = "Agile Girl best time: " + agile0BestTime.ToString();
        parkourGuyTxt.text = "Parkour man best time: " + parkour0BestTime.ToString();
        if (default0BestTime == 0)
        {
            defaultGuyTxt.text = "Default Guy: Not Completed";
        }
        if (big0BestTime == 0)
        {
            bigGuyTxt.text = "Big Guy: Not Completed";
        }
        if (agile0BestTime == 0)
        {
            agileGirlTxt.text = "Agile Girl: Not Completed";
        }
        if (parkour0BestTime == 0)
        {
            parkourGuyTxt.text = "Parkour Guy: NotCompleted";
        }
        levelText.text = "Level 1 Stats";
    }

    public void switchToLevel1ForStats()
    {
        achievementsText.text = "";
        
        foreach (Achievement item in achievements.level3Achievements)
        {
            if (item.completed)
            {
                completeOrNot = "Complete!";
            }
            else
            {
                completeOrNot = " Not Complete.";
            }
            achievementsText.text += item.achievementName + "\n" + item.achievementDescription + "\n" + completeOrNot + "\n\n";
            
            
        }
        defaultGuyTxt.text = "Default guy best time: " + default0BestTime.ToString();
        bigGuyTxt.text = "Big Man best time: " + big0BestTime.ToString();
        agileGirlTxt.text = "Agile Girl best time: " + agile0BestTime.ToString();
        parkourGuyTxt.text = "Parkour man best time: " + parkour0BestTime.ToString();
        if (default0BestTime == 0)
        {
            defaultGuyTxt.text = "Default Guy: Not Completed";
        }
        if (big0BestTime == 0)
        {
            bigGuyTxt.text = "Big Guy: Not Completed";
        }
        if (agile0BestTime == 0)
        {
            agileGirlTxt.text = "Agile Girl: Not Completed";
        }
        if (parkour0BestTime == 0)
        {
            parkourGuyTxt.text = "Parkour Guy: NotCompleted";
        }
        levelText.text = "Level 1 Stats";
    }
    public void switchToLevel2ForStats()
    {
        achievementsText.text = "";
        foreach (Achievement item in achievements.level1Achievements)
        {
            if (item.completed)
            {
                completeOrNot = "Complete!";
            }
            else
            {
                completeOrNot = " Not Complete.";
            }
            achievementsText.text += item.achievementName + "\n" + item.achievementDescription + "\n" + completeOrNot + "\n\n";
            
            
        }
        defaultGuyTxt.text = "Default guy best time: " + default1BestTime.ToString();
        bigGuyTxt.text = "Big Man best time: " + big1BestTime.ToString();
        agileGirlTxt.text = "Agile Girl best time: " + agile1BestTime.ToString();
        parkourGuyTxt.text = "Parkour man best time: " + parkour1BestTime.ToString();
        if (default1BestTime == 0)
        {
            defaultGuyTxt.text = "Default Guy: Not Completed";
        }
        if (big1BestTime == 0)
        {
            bigGuyTxt.text = "Big Guy: Not Completed";
        }
        if (agile1BestTime == 0)
        {
            agileGirlTxt.text = "Agile Girl: Not Completed";
        }
        if (parkour1BestTime == 0)
        {
            parkourGuyTxt.text = "Parkour Guy: NotCompleted";
        }
        levelText.text = "Level 2 Stats";
    }
    public void switchToLevel3ForStats()
    {
        achievementsText.text = "";
        foreach (Achievement item in achievements.level2Achievements)
        {
            if (item.completed)
            {
                completeOrNot = "Complete!";
            }
            else
            {
                completeOrNot = " Not Complete.";
            }
            achievementsText.text += item.achievementName + "\n" + item.achievementDescription + "\n" + completeOrNot + "\n\n";

            defaultGuyTxt.text = "Default guy best time: " + default2BestTime.ToString();
            bigGuyTxt.text = "Big Man best time: " + big2BestTime.ToString();
            agileGirlTxt.text = "Agile Girl best time: " + agile2BestTime.ToString();
            parkourGuyTxt.text = "Parkour man best time: " + parkour2BestTime.ToString();
        }
        if (default2BestTime == 0)
        {
            defaultGuyTxt.text = "Default Guy: Not Completed";
        }
        if (big2BestTime == 0)
        {
            bigGuyTxt.text = "Big Guy: Not Completed";
        }
        if (agile2BestTime == 0)
        {
            agileGirlTxt.text = "Agile Girl: Not Completed";
        }
        if (parkour2BestTime == 0)
        {
            parkourGuyTxt.text = "Parkour Guy: Not Completed";
        }
        levelText.text = "Level 3 Stats";
    }
    public void DeleteSave()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(scene.name);
    }
    //IEnumerator AnimationStransition(int )
    //{
    //    transition.SetTrigger("Start");
    //    yield return new WaitForSeconds(transitionTime);

    //}
}
