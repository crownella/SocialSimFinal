using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class represents the types of intents the player can have
public enum intent
{
    rationalize,
    sympathize,
    confront,
    engage
}

/// <summary>
/// This class represents an instance of one entry. An array of entires is used to find an entry that matches a given Query. An entry represents one possible dialogue option for the suspect. 
/// 
/// Each entry has specific criteria linked to it, as well as the suspects responses and the effects of the response, saved as a trigger.
/// </summary>
[Serializable]
public class Entry
{
    public string name;
    public string[] criteria;
    public intent intent;
    public string[] response;
    public string[] remember;
    public string[] trigger;

    public Entry(string n, string[] c, intent i, string[] r, string[] rm, string[] t)
    {
        name = n;
        criteria = c;
        intent = i; 
        response = r;
        remember = rm;
        trigger = t;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override string ToString()
    {
        string returnString = "Entry: ";
        returnString += "Criteria: " + criteria.ToString();
        returnString += "Response: " + response.ToString();
        if(remember != null)
        {
            returnString += "Rememeber: " + remember.ToString();
        }
        else
        {
            returnString += "Rememeber: ";
        }
        if(trigger != null)
        {
            returnString += "Trigger: " + trigger.ToString();
        }
        else
        {
            returnString += "Trigger: ";
        }
        
        return returnString;
    }
}
