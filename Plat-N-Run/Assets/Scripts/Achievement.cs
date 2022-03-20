using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Achievement
{   
    [Header("Normal Acheivements")]
    [Tooltip("type of achievement")]
    public Types achievementType;
    [Tooltip("whether or not the achievement is completed")]
    public bool completed;
    [Tooltip("Whether or not the achievement isnt completed. used for checking only achievements that havent been completed yet")]
    public bool notCompleted;
    [Tooltip("How many times the player can die in the level before the achievement is checked off as not completed. Will be checked for completion at the end of the level.")]
    public int deathThreshold;
    [Tooltip("How many collectibles the player needs to collect to get the collectible achievement. Will be two different types, one where they only need to collect 1, and then one where they need" +
        "to collect all. ")]
    public int collectibleAmount;
    [Tooltip("How long the player has to complete the level to get the achievement. Will be checked at the end of the level.")]
    public float timeThreshold;
    [Tooltip("The name of the character to check against to see if the player completed it with a specific character. Will be checked at the end of the level")]
    public string characterName;
    [Tooltip("name of the achievement")]
    public string achievementName;
    [Tooltip("description of the achievement")]
    public string achievementDescription;
    [Tooltip("Tag used for saving into player prefabs")]
    public string tag;

    [Header("Special Achievements")]
    //for special achievements only
    public int deathAmount;
    public int numOfJumpsThreshold;
    public int numOfWallsThreshold;
    public enum Types
    {
        TIME,
        ONE_COLLECTIBLE,
        ALL_COLLECTIBLE,
        UNDER_DEATH,
        CHARACTER_PASS,
        SPECIAL
    }
}
