using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelSwitcherForPlayer : MonoBehaviour
{
    public LevelLoader levelLoader;
    //public GameObject levelSelect;
    public GameObject pauseMenu;
    public GameObject player;
    //public GameObject turret;
    public GameObject healthText;
    public Animator animation;
    public float transitionTime = 1f;
    public bool ableToPause = true;
    public void Start()
    {
        //levelSelect.SetActive(false);
        pauseMenu.SetActive(false);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && ableToPause)
        {
            player.GetComponent<Player>().enabled = false;
            //turret.GetComponent<ShootAtPlayer>().enabled = false;
            //Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            ableToPause = false;
            healthText.SetActive(false);
            pauseMenu.SetActive(true);
        }
    }
    public void Resume()
    {
        healthText.SetActive(true);
        pauseMenu.SetActive(false);
        player.GetComponent<Player>().enabled = true;
        //turret.GetComponent<ShootAtPlayer>().enabled = true;
        //Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        ableToPause = true;
    }
    //public void LevelSelect()
    //{
    //    pauseMenu.SetActive(false);
    //    levelSelect.SetActive(true);
    //}
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
    public void GoToMainMenu()
    {
        levelLoader.LoadLevel(0);
    }
    public void GoBackToMainMenu(GameObject pageToTurnOff)
    {
        pageToTurnOff.SetActive(false);
        pauseMenu.SetActive(true);
    }
    public void QuitGame()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
    //IEnumerator AnimationTransition(int sceneIndex)
}
