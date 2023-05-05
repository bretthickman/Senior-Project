using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableUI : MonoBehaviour
{

    public int collectableNum = 3;
    private void Start()
    {
        foreach (Transform child in transform)
        {
            Debug.Log("Foreach loop: " + child);
        }
        updateCollectables();
    }

    void updateCollectables()
    {
        int counter = collectableNum;
        foreach (Transform child in transform)
        {
            if (counter != 0)
            {
                child.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                counter--;
            }
            else
                child.GetComponent<Image>().color = new Color32(255, 255, 225, 0);
        }
    }
}