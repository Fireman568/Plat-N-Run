using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AchievementTabDisplay : MonoBehaviour
{
    public TextMeshProUGUI achievementName;
    public TextMeshProUGUI achievementDescription;
    public GameObject achievementImage;
    float timer;
    public float timerThreshold;
    public bool showAch;
    // Start is called before the first frame update
    void Start()
    {
        achievementImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (showAch)
        {
            timer += Time.deltaTime;
            achievementImage.SetActive(true);
        }
        if(timer >= timerThreshold)
        {
            showAch = false;
            timer = 0;
        }
    }

    public void getAchievementInfo(string achievementNamePar, string achievementDescriptionPar)
    {
        achievementName.text = achievementNamePar;
        achievementDescription.text = achievementDescriptionPar;
    }
    public void showAchievement(bool show)
    {
        showAch = show;
    }
}
