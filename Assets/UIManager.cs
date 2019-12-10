using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;

    public TextMeshProUGUI[] playerOptionsText;

    public Button[] reactionButtons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VoidIntents()
    {
        for(int i = 0; i < reactionButtons.Length; i++)
        {
            reactionButtons[i].interactable = false;
        }
    }

    public void DisplayIntents()
    {
        for (int i = 0; i < reactionButtons.Length; i++)
        {
            reactionButtons[i].interactable = true;
        }
    }

    public void DisplayOptions(string[] intentsToDisplay)
    {
        for(int i = 0; i < playerOptionsText.Length; i++)
        {
            playerOptionsText[i].SetText(intentsToDisplay[i]);
        }
    }
}
