using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// This class represents an instance of a suspect object. It stores variables about the suspects current state and bools to track where in the confession process the suspect is.
/// </summary>
public class Suspect : MonoBehaviour
{
   


    public bool emotional = false;
    public int _denialLevel = 0; //game ends if it reaches 10, affects dialouge and confession;
    public int egoLevel = 5; //max of 10, affects dialouge and confession
    public int futilityLevel = 5; //max of 10, affects dialouge and confession
    public int attention = 5; //max of 10, affects dialouge and confession
    public bool firstConfession = false; //if they have admitted to a lesser crime
    public bool secondConfession = false; //if they have been fully caught
    public bool demandedLayer = false; //lose conditong


    /* OLD CODE FROM PROTOTYPE
    string[] denialOptions = {   "I didnt do this.",
                                 "I could never hurt Dylan.",
                                 "I love Dylan. I didnt hurt him.",
                                 "This is crazy. I would never hurt Dylan."};
    string[] egoUpOptions = {    "Me and Dylan dont always get along",
                                 "I have always been a good husband to Dylan.",
                                 "I just want Dylan to come back home.",
                                 "Dylan had been pulling away from me recently."};
    string[] futilityOptions = { "I didnt do this.",
                                 "I could never hurt Dylan.",
                                 "I love Dylan. I didnt hurt him.",
                                 "This is crazy. I would never hurt Dylan."};
    string[] engageOptions = {   "I want Dylan back.",
                                 "I dont want to be here. I want to be out there looking for Dylan.",
                                 "I love Dylan. I didnt hurt him.",
                                 "This is crazy. I would never hurt Dylan."};
    string demandLawyer = "I want a Lawyer";
    string firstConfessionText = "I didnt mean to hurt Dylan. Sometimes he would just make me mad and I couldnt control my anger. I hit him once or twice but I could never kill him.";
    



    //intent is 1-4 based on players intent
    public void getResponse(int _intent)
    {
        if (!firstConfession)
        {
            if (_intent == 1) //intent is to rationalize
            {
                if (emotional) //if the suspect is emotional
                {
                    if(_denialLevel < 10)
                    {
                        currentResponse = denialOptions[Random.Range(0, denialOptions.Length - 1)];
                        _denialLevel += 1;
                    }
                    else //if denial is at 10, demand a laywer
                    {
                        
                        currentResponse = demandLawyer;
                    } 
                }
                else //if the suspect is not emotional, rationalizing has a greater effect
                {
                    currentResponse = egoUpOptions[Random.Range(0, egoUpOptions.Length - 1)];
                    if (egoLevel < 10)
                    {
                        egoLevel += 1;
                    }
                }
            }else if(_intent == 2) //if the intent is to sympathize
            {
                if (emotional) //if the suspect is emotional
                {
                    currentResponse = egoUpOptions[Random.Range(0, egoUpOptions.Length - 1)];
                    if (egoLevel < 10)
                    {
                        egoLevel += 1;
                    }
                }
                else //if the suspect is not emotional, sympathizing has a lesser effect
                {
                    if (_denialLevel < 10)
                    {
                        currentResponse = denialOptions[Random.Range(0, denialOptions.Length - 1)];
                        _denialLevel += 1;
                    }
                    else //if denial is at 10, demand a laywer
                    {

                        currentResponse = demandLawyer;
                    } 
                }
            }
            else if (_intent == 3) //if the intent is to confront
            {
                currentResponse = futilityOptions[Random.Range(0, futilityOptions.Length - 1)];
                if(futilityLevel < 10)
                {
                    futilityLevel += 1;
                }
                else if(futilityLevel == 10 && egoLevel == 10)
                {
                    currentResponse = firstConfessionText;
                    firstConfession = true;
                }
            }else if(_intent == 4)//if the intent is to engage
            {
                currentResponse = engageOptions[Random.Range(0, engageOptions.Length - 1)];
                if(attention < 10)
                {
                    attention += 1;
                }
            }
        }


        StartCoroutine(Respond());
    }

    */

    public void makeEmotional()
    {
        emotional = !emotional;
    }

}
