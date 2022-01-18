using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class LevelSwitcher : MonoBehaviour
{
    public LevelLoader levelLoader;
    public Scene scene;
    public GameObject settings;
    public GameObject collectibles;
    public GameObject levelSelect;
    public GameObject mainMenu;
    public GameObject credits;
    public GameObject level1Button;
    public GameObject level2Button;
    public GameObject level3Button;
    public Animator transition;
    public TextMeshProUGUI collectiblesAmount;
    public float transitionTime;
    public float amountOfCollectibles;
    public bool level1Complete;
    public bool level2Complete;
    public bool level3Complete;
    public bool collectible1Collected;
    public bool collectible2Collected;
    public bool collectible3Collected;
    public bool collectible4Collected;
    public bool collectible5Collected;
    public bool collectible6Collected;
    public void Start()
    {
        mainMenu.SetActive(true);
        settings.SetActive(false);
        collectibles.SetActive(false);
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
        if (level1Complete)
        {
            level1Button.SetActive(true);
        }
        else
        {
            level1Button.SetActive(false);
        }

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
    public void GoToLevel1(int sceneIndex)
    {
        levelLoader.LoadLevel(sceneIndex);
    }
    public void GoToLevel2(int sceneIndex)
    {
        levelLoader.LoadLevel(sceneIndex);
    }
    public void GoToLevel3(int sceneIndex)
    {
        levelLoader.LoadLevel(sceneIndex);
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
    public void GoToCollectibles()
    {
        mainMenu.SetActive(false);
        collectibles.SetActive(true);
    }
    public void goToLevel()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(true);
    }
    public void QuitTheGame()
    {
        PlayerPrefs.Save();
        Application.Quit();
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
