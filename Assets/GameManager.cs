using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public UIManager UIManager;
    public Suspect currentSuspect;
    public Suspect[] allSuspects;

    public enum effect { Futility, Ego, attention };

    public enum intent { rationalize, sympathize, confront, engage};

    private intent playerCurrentIntent;

    string[] rationalizeText = {"This was probably all a misunderstaning.",
                                 "Anyone in your situation would have done the same thing.",
                                 "Sometimes accidents happen.",
                                 "Im sure you didnt hurt him on purpose"};

    string[] sympathizeText = {  "I understand a new marrige can be hard.",
                                 "Maybe Lane had been distant lately. I know that can be hard to deal with in a marrige.",
                                 "Maybe the honeymoon phase was over and you felt like there was no other way out.",
                                 "I can tell you are a good guy. I just need you to tell me where he is."};

    string[] confrontText = {   "We know you were involved with Lane`s disappearance.",
                                 "We have a witness who saw Lane arrive at home with you at 1 am Sunday morning. Tell us where he is.",
                                 "We know you killed him. Where is he? ",
                                 "No one else saw him but you that night. You must know where he is."};

    string[] engageText = {      "*touch shoulder* We know you miss him.",
                                 "*move closer* I understand this cant be easy.",
                                 "*direct eye contact* Please help us find him.",
                                 "We cant do this without you."};

    private void Awake()
    {
        
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSuspect.responding)
        {
            UIManager.VoidIntents();
        }
        else
        {
            UIManager.DisplayIntents();

            if(playerCurrentIntent == intent.rationalize)
            {
                UIManager.DisplayOptions(rationalizeText);
            }
            else if(playerCurrentIntent == intent.sympathize)
            {
                UIManager.DisplayOptions(sympathizeText);
            }
            else if (playerCurrentIntent == intent.confront)
            {
                UIManager.DisplayOptions(confrontText);
            }
            else if (playerCurrentIntent == intent.engage)
            {
                UIManager.DisplayOptions(engageText);
            }

        }
    }

    public class TextOption {
        effect textEffect; //if the option will effect the suspects futility, ego or attention
        int value; //value to be set to or modified by
        bool setter; //if this text option sets a suspect value instead of modifiying it
        string text; //the actual text

        public TextOption(string _text, effect _textEffect, bool _setter, int _value)
        {
            text = _text;
            textEffect = _textEffect;
            setter = _setter;
            value = _value;
        }
    }



    public class PlayerNode {
        TextOption[] options; //4 options
        intent nodeIntent; //the players intended action

        public PlayerNode(intent _nodeIntent, TextOption optionOne, TextOption optionTwo, TextOption optionThree, TextOption optionFour)
        {
            options = new TextOption[4];
            options[0] = optionOne;
            options[1] = optionTwo;
            options[3] = optionThree;
            options[4] = optionFour;

            nodeIntent = _nodeIntent;

        }
    }



    ///UI MANAGEMENT
    
    public void setIntent(int _intent)
    {
        if(_intent == 1)
        {
            playerCurrentIntent = intent.rationalize;
        }
        else if(_intent == 2)
        {
            playerCurrentIntent = intent.sympathize;
        }
        else if (_intent == 3)
        {
            playerCurrentIntent = intent.confront;
        }
        else if (_intent == 4)
        {
            playerCurrentIntent = intent.engage;
        }
        // playerCurrentIntent = _intent;
    }

    public void engage()
    {
        
        int tmp = 0;
        
        if(playerCurrentIntent == intent.rationalize)
        {
            tmp = 1;
        }else if (playerCurrentIntent == intent.sympathize)
        {
            tmp = 2;
        }
        else if (playerCurrentIntent == intent.confront)
        {
            tmp = 3;
        }
        else if (playerCurrentIntent == intent.engage)
        {
            tmp = 4;
        }
        
        currentSuspect.getResponse(tmp);
       
    }




}
