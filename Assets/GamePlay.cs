using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{

    public UIManager UIManager;
    public Suspect currentSuspect;
    private intent playerCurrentIntent;
    private bool end = false;
    private bool lost = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //called by UI button
    public void QueryRationalize()
    {
        Query(intent.rationalize);
    }

    //called by UI button
    public void QuerySympathize()
    {
        Query(intent.sympathize);
    }

    //called by UI button
    public void QueryConfront()
    {
        Query(intent.confront);
    }

    //called by UI button
    public void QueryEngage()
    {
        Query(intent.engage);
    }

    //creates a Query object then uses it to find response, then starts the response on the UI manager
    void Query(intent _intent)
    {
        if (!end && !lost && !UIManager.responding)
        {
            Query q = new Query(_intent);

            q.setSuspect(currentSuspect);

            UIManager.StartResponse(q.FindMatch(), _intent);
        }

        if (currentSuspect.firstConfession && !end)
        {
            StartCoroutine(delayEnd());
        }

        if(currentSuspect.demandedLayer && !lost)
        {
            StartCoroutine(delayLose());
        }
    }

    IEnumerator delayEnd()
    {
        end = true;
        yield return new WaitForSeconds(3f);
        UIManager.End();
    }

    IEnumerator delayLose()
    {
        lost = true;
        yield return new WaitForSeconds(3f);
        UIManager.Lose();
    }


    
}
