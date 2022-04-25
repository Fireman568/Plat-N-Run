using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTurnOn : MonoBehaviour
{
    public GameObject defaultGuy;
    public GameObject bigGuy;
    public GameObject agileGirl;
    public GameObject parkourMan;
    public int characterPicked;
    // Start is called before the first frame update
    void Awake()
    {
        characterPicked = PlayerPrefs.GetInt("characterPick");
        if(characterPicked == 1)
        {
            defaultGuy.SetActive(true);
        }
        else if(characterPicked == 2)
        {
            bigGuy.SetActive(true);
        }
        else if(characterPicked == 3)
        {
            agileGirl.SetActive(true);
        }
        else
        {
            parkourMan.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
