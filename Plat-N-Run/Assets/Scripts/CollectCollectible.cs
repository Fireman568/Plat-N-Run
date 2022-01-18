using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCollectible : MonoBehaviour
{
    public int amountOfCollectibles;
    public AudioSource audioSource;
    public AudioClip clip;
    public bool collectible1Collected;
    public bool collectible2Collected;
    public bool collectible3Collected;
    public bool collectible4Collected;
    public bool collectible5Collected;
    public bool collectible6Collected;
    public void Start()
    {
        amountOfCollectibles = PlayerPrefs.GetInt("amountOfCollectibles");
        collectible1Collected = PlayerPrefs.GetInt("Collectible 1 Collected") == 1 ? true : false;
        collectible2Collected = PlayerPrefs.GetInt("Collectible 2 Collected") == 1 ? true : false;
        collectible3Collected = PlayerPrefs.GetInt("Collectible 3 Collected") == 1 ? true : false;
        collectible4Collected = PlayerPrefs.GetInt("Collectible 4 Collected") == 1 ? true : false;
        collectible5Collected = PlayerPrefs.GetInt("Collectible 5 Collected") == 1 ? true : false;
        collectible6Collected = PlayerPrefs.GetInt("Collectible 6 Collected") == 1 ? true : false;

        if(collectible1Collected && tag == "Collectible 1")
        {
            gameObject.SetActive(false);
        }
        else if(collectible2Collected && tag == "Collectible 2")
        {
            gameObject.SetActive(false);
        }
        else if (collectible3Collected && tag == "Collectible 3")
        {
            gameObject.SetActive(false);
        }
        else if (collectible4Collected && tag == "Collectible 4")
        {
            gameObject.SetActive(false);
        }
        else if (collectible5Collected && tag == "Collectible 5")
        {
            gameObject.SetActive(false);
        }
        else if (collectible6Collected && tag == "Collectible 6")
        {
            gameObject.SetActive(false);
        }
    }
    public void Update()
    {
        amountOfCollectibles = PlayerPrefs.GetInt("amountOfCollectibles");
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            
            
            Debug.Log("This should be the amount of collectibles collected so far before getting the int: " + amountOfCollectibles);
            
            
            if(tag == "Collectible 1")
            {
                PlayerPrefs.SetInt("Collectible 1 Collected", 1);
                amountOfCollectibles += 1;
            }
            else if(tag == "Collectible 2")
            {
                PlayerPrefs.SetInt("Collectible 2 Collected", 1);
                amountOfCollectibles += 1;
            }
            else if (tag == "Collectible 3")
            {
                PlayerPrefs.SetInt("Collectible 3 Collected", 1);
                amountOfCollectibles += 1;
                Debug.Log("Amount of collectibles after collecting the third one: " + amountOfCollectibles);
            }
            else if (tag == "Collectible 4")
            {
                PlayerPrefs.SetInt("Collectible 4 Collected", 1);
                amountOfCollectibles += 1;
                Debug.Log("Amount of collectibles after collecting the 4th one: " + amountOfCollectibles);
            }
            else if (tag == "Collectible 5")
            {
                PlayerPrefs.SetInt("Collectible 5 Collected", 1);
                amountOfCollectibles += 1;
            }
            else if (tag == "Collectible 6")
            {
                PlayerPrefs.SetInt("Collectible 6 Collected", 1);
                amountOfCollectibles += 1;
            }
            PlayerPrefs.SetInt("amountOfCollectibles", amountOfCollectibles);
            UpdateCollectibles();
            PlayerPrefs.Save();
            playAudio(clip);
            gameObject.SetActive(false);
        }
    }
    public void UpdateCollectibles()
    {
        amountOfCollectibles = PlayerPrefs.GetInt("amountOfCollectibles");
    }
    public void playAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
