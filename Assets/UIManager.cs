using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;

    public TextMeshProUGUI playerText;

    public Button[] reactionButtons;

    string currentResponse;

    public bool responding = false;

    public float timeBetweenWords = 2f;

    public TextMeshProUGUI suspectText;

    public TextMeshProUGUI denialLevelText;
    public TextMeshProUGUI egoLevelText;
    public TextMeshProUGUI attentionLevelText;
    public TextMeshProUGUI futilityLevelText;

    public GameObject end = null;
    public GameObject intro = null;
    public GameObject lose = null;

    string[] SympathizePlayer = new string[5] {"I know it must be hard being in a new marrige","new marriges are rough","its not always easy living with a partner","working a full time job, while you husband doesnt, must be hard","you must work hard to keep everything together"};
    string[] RationalizePlayer = new string[5] { "It was an accident", "Anyone in your position would have done the same thing", "Mistakes happen", "We understand that you didnt mean to", "We know it was an accident" };
    string[] ConfrontPlayer = new string[5] { "We know you did it", "There is no point in denying it", "We have proof that you did it", "Noone else could have done it but you", "you are guilty and we are going to prove it" };
    string[] EngagePlayer = new string[5] { "Are you listening", "are you there", "hello?", "pay attention", "this isnt over yet" };


    // Start is called before the first frame update
    void Start()
    {
        Intro();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopGame();
        }   
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

    IEnumerator Assert(intent i)
    {
        string outcome = PickResponse(i);
        string[] words = outcome.Split(' ');
        string currentOutcome = "";

        foreach(string word in words)
        {
            currentOutcome = currentOutcome + " " + word;
            playerText.SetText(currentOutcome);
            yield return new WaitForSeconds(.1f);
        }
    }

    public void StartResponse(string response, intent i)
    {
        StartCoroutine(Respond(response, i));

        
    }

    IEnumerator Respond(string response, intent i)
    {
        responding = true;

        StartCoroutine(Assert(i));

        yield return new WaitForSeconds(timeBetweenWords*5);

        string[] words = response.Split(' ');
        string currentResponseText = "";

        foreach (string word in words)
        {
            currentResponseText = currentResponseText + " " + word;
            suspectText.SetText(currentResponseText);
            yield return new WaitForSeconds(timeBetweenWords);
        }

        responding = false;

    }

    public void Intro()
    {
        end.SetActive(false);
        intro.SetActive(true);
        lose.SetActive(false);
         
    }

    public void closeIntro()
    {
        intro.SetActive(false);
    }

    public void End()
    {
        end.SetActive(true);
    }

    public void Lose()
    {
        lose.SetActive(true);
    }

    public void StopGame()
    {
        Application.Quit();
    }

    private string PickResponse(intent i)
    {
        int ran = Random.Range(0, 4);

        if(i.Equals(intent.sympathize))
        {
            return SympathizePlayer[ran];
        }

        if (i.Equals(intent.rationalize))
        {
            return RationalizePlayer[ran];
        }

        if (i.Equals(intent.confront))
        {
            return ConfrontPlayer[ran];
        }

        if (i.Equals(intent.engage))
        {
            return EngagePlayer[ran];
        }
        return "";
    }

    
}
