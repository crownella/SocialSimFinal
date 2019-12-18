using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class Dialogue : MonoBehaviour
{
    private string filePath;

    [Serializable]
    public class EntryCollection
    {
        public Entry[] entries;

        public int currentNumberOfEntries = 0;

        public EntryCollection()
        {
            entries = new Entry[100];
        }

        public void add(Entry e)
        {
            if(currentNumberOfEntries >= 100)
            {
                throw new System.Exception("Entry Array Full");
            }
            entries[currentNumberOfEntries] = e;
            currentNumberOfEntries += 1;
        }

        
    }

    public EntryCollection currentCollection;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Program Starting");
        //WriteEntries();
        ReadEntries();
        Debug.Log("Start Complete");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void WriteEntries()
    {
        filePath = Application.dataPath + Path.DirectorySeparatorChar + "Dialogue.json";
        GenerateRandomEntry();
        Entry[] collection = new Entry[currentCollection.currentNumberOfEntries];
        
        using (StreamWriter stream = new StreamWriter(filePath))
        {
            for(int i = 0; i < currentCollection.currentNumberOfEntries; i++)
            {
                collection[i] = currentCollection.entries[i];
                //string json = JsonConvert.SerializeObject(currentCollection.entries[i], Formatting.Indented);
                //stream.Write(json);
            }
            string json = JsonConvert.SerializeObject(collection, Formatting.Indented);
            stream.Write(json);
        }
        Debug.Log("Entries Written: " + currentCollection.currentNumberOfEntries);
    }


    void ReadEntries()
    {
        filePath = Application.dataPath + Path.DirectorySeparatorChar + "Dialogue.json";
        currentCollection = new EntryCollection();
        using (StreamReader stream = new StreamReader(filePath))
        {
            string json = stream.ReadToEnd();
            JsonTextReader reader = new JsonTextReader(new StringReader(json));
            
            Entry[] collection  = JsonConvert.DeserializeObject<Entry[]>(json);
            foreach(Entry e in collection)
            {
                currentCollection.add(e);
            }

            //JsonTextReader reader = new JsonTextReader
            //string.json = stream.ReadLine();
            // = JsonUtility.FromJson<EntryCollection>(json);
        }

        Debug.Log("Entries Loaded: " + currentCollection.currentNumberOfEntries);

    }

    void GenerateRandomEntry()
    {
        Entry testOne = new Entry("testOne", new string[] { "isNotEmotional","FirstConfessionFlase", "SecondConfessionFalse" },intent.confront ,new string[] { "I aint do it", "i didnt do it", "stop harassing me, i didnt do it" }, new string[] { "Denied" }, new string[] { "DenialLevel+=1" });
        Entry testTwo = new Entry("testTwo", new string[] { "isEmotional","FirstConfessionFlase", "SecondConfessionFalse" },intent.confront ,new string[] { "I loved him", "he meant everything to me", "i would never hurt him" }, new string[] { "Denied" }, new string[] { "DenialLevel+=1" });

        currentCollection = new EntryCollection();
        currentCollection.add(testOne);
        currentCollection.add(testTwo);

        Debug.Log("Random Generated Collection Size: " + currentCollection.currentNumberOfEntries);
    }
}
