using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class represents an instance of a Query. It uses a declared instance of a Suspect and a Dialogue script to find a response to the Query based on the Querys intent and criteria
/// </summary>
public class Query : MonoBehaviour
{
    string[] queryCriteria;
    intent _intent;
    Suspect suspectToQ;
    Dialogue D;

    public Query(intent i)
    {
        _intent = i;
    }

    // Start is called before the first frame update
    void Start()
    {
        D = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<Dialogue>();
        if (D == null)
        {
            throw new System.Exception("No Dialogue Manager in Scene");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setSuspect(Suspect s)
    {
        suspectToQ = s;
    }

    public string FindMatch()
    {
        //handle end cases
        if(suspectToQ._denialLevel >= 10 || suspectToQ.attention < 1 || suspectToQ.egoLevel < 1)
        {
            suspectToQ.demandedLayer = true;
            return "I cant handle this, get me a lawyer";
        }
        if(suspectToQ == null)
        {
            throw new System.Exception("Suspect Never Set");
        }

        D = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<Dialogue>();
        if (D == null)
        {
            throw new System.Exception("No Dialogue Manager in Scene");
        }

        //find all entries that have the same intent (this will basically cut the number of dialogue to be dealth with to 1/4)
        Entry[] intentMatches = FindIntentMatches();

        //use those entries to find criteria matches
        Entry matchedEntry = FindCriteriaMatch(intentMatches);

        Debug.Log(matchedEntry.name);

        //activate any triggers on the entry
        TriggerTriggers(matchedEntry.trigger);

        //pick a random number from the 0  to responses length
        int random = Random.Range(0, matchedEntry.response.Length - 1);

        //return a random response
        return matchedEntry.response[random];
    }

    

    //returns an array of Entries that match the current intent
    Entry[] FindIntentMatches()
    {
        Entry[] currentEntries = D.currentCollection.entries;
        Entry[] intentMatches = new Entry[100];
        int intentsMatched = 0;
        int length = D.currentCollection.currentNumberOfEntries;

        //find the matches and copy it to a new array
        for (int i = 0; i < length; i++)
        {
            if (currentEntries[i].intent.Equals(_intent))
            {
                intentMatches[intentsMatched++] = currentEntries[i];
            }
        }

        //copy it to a smaller array to avoid overhead later
        Entry[] finalIntentMatches = new Entry[intentsMatched];
        for(int i = 0; i < intentsMatched; i++)
        {
            finalIntentMatches[i] = intentMatches[i];
        }

        return finalIntentMatches;
    }

    

    //returns the best entry match based on the current game state from the given entries
    Entry FindCriteriaMatch(Entry[] intentMatches)
    {
        EntryMatch[] matches = new EntryMatch[100];
        int matchesFound = 0;
 
        foreach(Entry entry in intentMatches)
        {
            int _score = 0;
            bool failCheck = false;

            //check each tag to see if its true
            foreach (string criteria in entry.criteria)
            {
                if (ExpressionTrue(criteria))
                {
                    _score += 1;
                }
                else
                {
                    failCheck = true;
                }
            }

            //if we failed any of the checks, dont add this entry to the list
            //if any of the tags were true, add it to the matches array, keping track of how many tags were true
            if (_score > 0 && !failCheck)
            {
                matches[matchesFound++] = new EntryMatch(entry, _score);
            }
        }

        if(matchesFound < 1)
        {
            throw new System.Exception("No Dialogue Matches Found");
        }

        int topScore = 0;
        int topIndex = 0;

        EntryMatch[] highScoreMatch = new EntryMatch[50];
        int highMatchesFound = 0;

        //find the match with the highest score
        for(int i = 0; i < matchesFound; i++)
        {
            if(matches[i].score > topScore)
            {
                topScore = matches[i].score;
            }
        }

        //find all matches that have that same score
        for (int i = 0; i < matchesFound; i++)
        {
            if (matches[i].score == topScore)
            {
                highScoreMatch[highMatchesFound++] = matches[i];
            }
        }

        //incase there is a mix up along the way
        if(highMatchesFound < 1)
        {
            throw new System.Exception("Error in top score calculation");
        }

        //if theres more than one, pick one randomly
        else if(highMatchesFound > 1)
        {
            int random = Random.Range(0, highMatchesFound - 1);
            return highScoreMatch[random].entry;
        }

        //will only run if highMatchesFound == 1
        return highScoreMatch[0].entry;
    }

    //evaluates all tags and returns true if the tag is true of the current game state
    bool ExpressionTrue(string expression)
    {
        if (expression.Equals("IsEmotional"))
        {
            return suspectToQ.emotional;
        }

        if (expression.Equals("IsNotEmotional"))
        {
            return !suspectToQ.emotional;
        }

        if (expression.Equals("FirstConfessionTrue"))
        {
            return suspectToQ.firstConfession;
        }

        if (expression.Equals("FirstConfessionFalse"))
        {
            return !suspectToQ.firstConfession;
        }

        if (expression.Equals("SecondConfessionTrue"))
        {
            return suspectToQ.secondConfession;
        }

        if (expression.Equals("SecondConfessionFalse"))
        {
            return !suspectToQ.secondConfession;
        }

        //if it needs to be parsed
        if (expression[1].Equals('='))
        {

            //get the number in the expression
            char charToEval = expression[2];
            int numberToEval = (int)char.GetNumericValue(charToEval);


            //get the tag without the number or operators
            string var = expression.Substring(3);

            if (var.Equals("DenialLevel"))
            {
                if (expression[0].Equals('<'))
                {
                    return suspectToQ._denialLevel < numberToEval;
                }

                if (expression[0].Equals('>'))
                {
                    return suspectToQ._denialLevel >= numberToEval;
                }

                if (expression[0].Equals('='))
                {
                    return suspectToQ._denialLevel.Equals(numberToEval);
                }
            }

            if (var.Equals("EgoLevel"))
            {

                if (expression[0].Equals('<'))
                {
                    return suspectToQ.egoLevel < numberToEval;
                }

                if (expression[0].Equals('>'))
                {
                    return suspectToQ.egoLevel >= numberToEval;
                }

                if (expression[0].Equals('='))
                {
                    return suspectToQ.egoLevel.Equals(numberToEval);
                }
            }

            if (var.Equals("FutilityLevel"))
            {
                if (expression[0].Equals('<'))
                {
                    return suspectToQ.futilityLevel < numberToEval;
                }

                if (expression[0].Equals('>'))
                {
                    return suspectToQ.futilityLevel >= numberToEval;
                }

                if (expression[0].Equals('='))
                {
                    return suspectToQ.futilityLevel.Equals(numberToEval);
                }
            }

            if (var.Equals("AttentionLevel"))
            {
                if (expression[0].Equals('<'))
                {
                    return suspectToQ.attention < numberToEval;
                }

                if (expression[0].Equals('>'))
                {
                    return suspectToQ.attention >= numberToEval;
                }

                if (expression[0].Equals('='))
                {
                    return suspectToQ.attention.Equals(numberToEval);
                }
            }
        }

        //if it needs to be check thorugh the query criteria - not implmented yet
        if (expression[0].Equals("$"))
        {
            string var = expression.Substring(1);

            foreach(string criteria in queryCriteria)
            {
                if (criteria.Equals(var))
                {
                    return true;
                }
            }

            return false;
        }

        throw new System.Exception("Tag Evaluation Failed: " +expression + "Expression[1]: " + expression[1]);
    }

    void TriggerTriggers(string[] triggers)
    {
        
        foreach(string trigger in triggers)
        {
            bool returned = false;
            if (trigger.Equals(""))
            {
                returned = true;
            }
            //if the trigger is an expression
            if (trigger[1].Equals('='))
            {
                //find the number in the expression
                char charToEval = trigger[2];
                int numberToEval = (int)char.GetNumericValue(charToEval);

                //give trigger without number or operators
                string var = trigger.Substring(3);

                if (var.Equals("DenialLevel"))
                {
                    if (trigger[0].Equals('+'))
                    {
                        if(suspectToQ._denialLevel < 10 - numberToEval)
                        {
                            suspectToQ._denialLevel += numberToEval;
                        }
                        returned = true;
                    }

                    if (trigger[0].Equals('-'))
                    {
                        if(suspectToQ._denialLevel > numberToEval - 1)
                        {
                            suspectToQ._denialLevel -= numberToEval;
                        }
                        returned = true;
                    }
                }

                if (var.Equals("EgoLevel"))
                {
                    if (trigger[0].Equals('+'))
                    {
                        if (suspectToQ.egoLevel < 10 - numberToEval)
                        {
                            suspectToQ.egoLevel += numberToEval;
                        }
                        returned = true;
                    }

                    if (trigger[0].Equals('-'))
                    {
                        if(suspectToQ.egoLevel > numberToEval - 1)
                        {
                            suspectToQ.egoLevel -= numberToEval;
                        }
                        returned = true;
                    }
                }

                if (var.Equals("FutilityLevel"))
                {
                    if (trigger[0].Equals('+'))
                    {
                        if(suspectToQ.futilityLevel < 10 - numberToEval)
                        {
                            suspectToQ.futilityLevel += numberToEval;
                        }
                        returned = true;
                    }

                    if (trigger[0].Equals('-'))
                    {
                        if(suspectToQ.futilityLevel > numberToEval - 1)
                        {
                            suspectToQ.futilityLevel -= numberToEval;
                        }
                        returned = true;
                    }
                }

                if (var.Equals("AttentionLevel"))
                {
                    if (trigger[0].Equals('+'))
                    {
                        if(suspectToQ.attention < 10 - numberToEval)
                        {
                            suspectToQ.attention += numberToEval;
                        }
                        returned = true;
                    }

                    if (trigger[0].Equals('-'))
                    {
                        if(suspectToQ.attention > numberToEval - 1)
                        {
                            suspectToQ.attention -= numberToEval;
                        }
                        returned = true;
                    }
                }
            }

            if (trigger.Equals("FirstConfessionTrue"))
            {
                suspectToQ.firstConfession = true;
                returned = true;
            }

            if (trigger.Equals("SecondConfessionTrue"))
            {
                suspectToQ.secondConfession = true;
                returned = true;
            }


            if (!returned)
            {
                throw new System.Exception("Trigger not Recognized");
            }
            
        }
    }


    //class used for saving entries with their score when criteria matching
    public class EntryMatch
    {
        public Entry entry;
        public int score;

        public EntryMatch(Entry e, int _score)
        {
            entry = e;
            score = _score;
        }
    }
}
