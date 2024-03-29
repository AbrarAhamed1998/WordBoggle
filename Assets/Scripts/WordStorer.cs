using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
using UnityEngine.UI;
using NUnit.Framework.Internal.Execution;

public class WordStorer : MonoBehaviour
{
    public static WordStorer instance;
    public List<GeneralWord> wordGroups = new List<GeneralWord>(6);
    string generalPath;
    public bool collectingWords;
    public TextAsset wordfile;
    string fullFileString;
    List<string> rawWordList = new List<string>();
	private void Awake()
	{
        if (instance != null)
		{
            Destroy(this);
            return;
		}
        else
		{
            instance = this;
        }
        DontDestroyOnLoad(this);
	}
	// Start is called before the first frame update
	void Start()
    {
        //Add a loading screen here at the start of the app
        StartCoroutine(GenerateGeneralWordList());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator GenerateGeneralWordList()
	{
        fullFileString = wordfile.text;
        string[] words = fullFileString.Split("\n"[0]);
        rawWordList = words.ToList();
        collectingWords = true;
        Debug.Log("Started collecting words from resources");
        for (int i = 0; i < 6; i++)
        {
            GeneralWord wordGroup = new GeneralWord();
            wordGroup.length = i + 3;
            generalPath = "WordLists/" + NumberToWord(wordGroup.length) + "LetterWords";
            Debug.Log("generalPath : " + generalPath);
            yield return LoadWordsFromResources(generalPath, wordGroup.alphabetList);
            wordGroups.Add(wordGroup);
        }
        Debug.Log("Finished collecting words from resources");
        collectingWords = false;
    }

    public string NumberToWord(int i)
	{
        string word;
        switch(i)
		{
            case 3:
                word =  "Three";
                break;
            case 4:
                word = "Four";
                break;

            case 5:
                word = "Five";
                break;

            case 6:
                word = "Six";
                break;

            case 7:
                word = "Seven";
                break;

            case 8:
                word = "Eight";
                break;

            default:
                word = "";
                Debug.LogError("word Count does not exist in Resources");
                break;
        }
        return word;
	}
    public IEnumerator LoadWordsFromResources(string generalPath, List<List<string>> alphabetList)
    {
        TextAsset tempFile;
        string tempString;
        string[] tempStringArray;
        alphabetList.Clear();
        for (int alphabet = 0; alphabet < 26; alphabet++)
        {
            Debug.Log(generalPath + "/" + Convert.ToChar(alphabet + (int)'a').ToString());
            tempFile = Resources.Load(generalPath+ "/"+Convert.ToChar(alphabet + (int)'a').ToString()) as TextAsset;
            tempString = tempFile.text;
            tempStringArray = tempString.Split("\n"[0]);

            alphabetList.Add(tempStringArray.ToList());
            yield return null;
        }
    }

    public bool wordExists(string candidate)
	{
        bool exists = false;
        //int startLetterIndex = FindStartLetterIndex(candidate);
        //Debug.Log("start letter index " + startLetterIndex + "  and letter " + Convert.ToChar(startLetterIndex + (int)'a').ToString() + " and string length is " + candidate.Length);
        /*switch(candidate.Length)
		{
            case 3:
                exists = wordGroups[0].SearchWordInList(candidate, startLetterIndex);
                break;
            case 4:
                exists = wordGroups[1].SearchWordInList(candidate, startLetterIndex);
                break;
            case 5:
                exists = wordGroups[2].SearchWordInList(candidate, startLetterIndex);
                break;
            case 6:
                exists = wordGroups[3].SearchWordInList(candidate, startLetterIndex);
                break;
            case 7:
                exists = wordGroups[4].SearchWordInList(candidate, startLetterIndex);
                break;
            case 8:
                exists = wordGroups[5].SearchWordInList(candidate, startLetterIndex);
                break;
        }*/
        if(rawWordList.Contains(candidate))
		{
            exists = true;
		}
        return exists;
	}

    public int FindStartLetterIndex(string str)
	{
        int startIndex = 0;
        Debug.Log("finding start letter index");
        for(int i =0; i<26;i++)
		{
            if(str.StartsWith(Convert.ToChar(i+(int)'a').ToString()))
			{
                startIndex = i;
                break;
			}
		}
        return startIndex;
	}
}

[System.Serializable]
public class GeneralWord
{
    public int length;
    public List<List<string>> alphabetList = new List<List<string>>();

    public bool SearchWordInList(string word, int startLetterIndex)
	{
        bool existsInList = false;
        
        Debug.Log("Find word " + word + " in list with of count + " + alphabetList[startLetterIndex].Count);
        /*if (alphabetList[startLetterIndex].Exists(element => element == word))
        {
            existsInList = true;
        }*/
        for(int i=0;i<alphabetList[startLetterIndex].Count;i++)
		{
            alphabetList[startLetterIndex][i].Trim();
            Debug.Log(alphabetList[startLetterIndex][i]);
            if(word == alphabetList[startLetterIndex][i])
			{
                existsInList = true;
			}

		}
        return existsInList;
	}
}
