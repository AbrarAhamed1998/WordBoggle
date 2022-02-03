using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Splits wordsList.txt by the string length and further splitting them by the starting letter.
/// This gives a total of 5 word length groups with approx 26 alphabet files for each.
/// </summary>
public class WordsList : MonoBehaviour 
{
    public TextAsset wordfile;
    List<string> rawWordList = new List<string>();

    public ThreeLetter threeLetterobj;
    public FourLetter fourLetterobj;
    public FiveLetter fiveLetterobj;
    public SixLetter sixLetterobj;
    public SevenLetter sevenLetterobj;
    public EightLetter eightLetterobj;   


    [Header("WordLists by Count")]
    
    
    
    
    
    
    


    string fullFileString;
    // Start is called before the first frame update
    void Start()
    {
        threeLetterobj = new ThreeLetter();
        fourLetterobj = new FourLetter();
        fiveLetterobj = new FiveLetter();
        sixLetterobj = new SixLetter();
        sevenLetterobj = new SevenLetter();
        eightLetterobj = new EightLetter();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGeneratingWordFiles()
	{
        StartCoroutine(GetRawWords());
	}
    IEnumerator GetRawWords()
	{
        Debug.Log("Generating Word Lists...");
        fullFileString = wordfile.text;
        string[] words = fullFileString.Split("\n"[0]);
        rawWordList = words.ToList();
        yield return ToLengthGroups(rawWordList);
        Debug.Log("Finished Setting Words to groups...");
        StartCoroutine(WriteToFiles());
        
	}


    IEnumerator WriteToFiles()
	{
        string generalPath = Path.Combine(Application.dataPath,"WordLists");


        //Write to directories names after the number of letters
        Debug.Log("Creating directories...");
        Directory.CreateDirectory(Path.Combine(generalPath, threeLetterobj.directoryName));
        Directory.CreateDirectory(Path.Combine(generalPath, fourLetterobj.directoryName));
        Directory.CreateDirectory(Path.Combine(generalPath, fiveLetterobj.directoryName));
        Directory.CreateDirectory(Path.Combine(generalPath, sixLetterobj.directoryName));
        Directory.CreateDirectory(Path.Combine(generalPath, sevenLetterobj.directoryName));
        Directory.CreateDirectory(Path.Combine(generalPath, eightLetterobj.directoryName));
        yield return null;
        WriteToRelativePath(Path.Combine(generalPath, threeLetterobj.directoryName), threeLetterobj.alphabeticOrderedList);
        WriteToRelativePath(Path.Combine(generalPath, fourLetterobj.directoryName), fourLetterobj.alphabeticOrderedList);
        WriteToRelativePath(Path.Combine(generalPath, fiveLetterobj.directoryName), fiveLetterobj.alphabeticOrderedList);
        WriteToRelativePath(Path.Combine(generalPath, sixLetterobj.directoryName), sixLetterobj.alphabeticOrderedList);
        WriteToRelativePath(Path.Combine(generalPath, sevenLetterobj.directoryName), sevenLetterobj.alphabeticOrderedList);
        WriteToRelativePath(Path.Combine(generalPath, eightLetterobj.directoryName), eightLetterobj.alphabeticOrderedList);

        Debug.Log("Finished Writing Word Files...");
	}

    IEnumerator ToLengthGroups(List<string> rawWords)
	{
        for(int i = 0; i< rawWords.Count; i++)
		{
            switch(rawWords[i].Length)
			{
                case 3:
                    threeLetterobj.threeLetter.Add(rawWords[i]);
                    ArrangeIntoAlphabetLists(rawWords[i],threeLetterobj.alphabeticOrderedList);
                    yield return null;
                    break;
                case 4:
                    fourLetterobj.fourLetter.Add(rawWords[i]);
                    ArrangeIntoAlphabetLists(rawWords[i], fourLetterobj.alphabeticOrderedList);
                    yield return null;
                    break;
                case 5:
                    fiveLetterobj.fiveLetter.Add(rawWords[i]);
                    ArrangeIntoAlphabetLists(rawWords[i], fiveLetterobj.alphabeticOrderedList);
                    yield return null;
                    break;
                case 6:
                    sixLetterobj.sixLetter.Add(rawWords[i]);
                    ArrangeIntoAlphabetLists(rawWords[i], sixLetterobj.alphabeticOrderedList);
                    yield return null;
                    break;
                case 7:
                    sevenLetterobj.sevenLetter.Add(rawWords[i]);
                    ArrangeIntoAlphabetLists(rawWords[i], sevenLetterobj.alphabeticOrderedList);
                    yield return null;
                    break;
                case 8:
                    eightLetterobj.eightLetter.Add(rawWords[i]);
                    ArrangeIntoAlphabetLists(rawWords[i], eightLetterobj.alphabeticOrderedList);
                    yield return null;
                    break;
			}
		}
	}

    public void ArrangeIntoAlphabetLists(string word, List<List<string>> alphabeticOrderedList)
    {
        for (int alphabet = 0; alphabet < 26; alphabet++)
        {
            word.ToLower();
            if (word.StartsWith(Convert.ToChar(alphabet + (int)'a').ToString()))
            {
                alphabeticOrderedList[alphabet].Add(word);
                return;
            }
        }

    }

    public void WriteToRelativePath(string relativePath, List<List<string>> alphabeticOrderedList)
    {
        for (int alphabet = 0; alphabet < 26; alphabet++)
        {
            File.WriteAllLines(Path.Combine(relativePath, Convert.ToChar(alphabet + (int)'a').ToString() + ".txt"), alphabeticOrderedList[alphabet]);
        }
    }




    
}

[System.Serializable]
public class ThreeLetter
{
    public string directoryName = "ThreeLetterWords";
    public List<string> threeLetter = new List<string>();
    public List<List<string>> alphabeticOrderedList = new List<List<string>>(26);

    public ThreeLetter()
	{
        if(alphabeticOrderedList.Count == 0)
		{
            for (char alphabet = 'a'; alphabet <= 'z'; alphabet++)
            {
                List<string> startLetter = new List<string>();
                alphabeticOrderedList.Add(startLetter);
            }
        }
	}

    
}
[System.Serializable]
public class FourLetter
{
    public string directoryName = "FourLetterWords";
    public List<string> fourLetter = new List<string>();
    public List<List<string>> alphabeticOrderedList = new List<List<string>>(26);

    public FourLetter()
    {
        if (alphabeticOrderedList.Count == 0)
        {
            for (char alphabet = 'a'; alphabet <= 'z'; alphabet++)
            {
                List<string> startLetter = new List<string>();
                alphabeticOrderedList.Add(startLetter);
            }
        }
    }
    
}
[System.Serializable]
public class FiveLetter
{
    public string directoryName = "FiveLetterWords";
    public List<string> fiveLetter = new List<string>();
    public List<List<string>> alphabeticOrderedList = new List<List<string>>(26);

    public FiveLetter()
    {
        if (alphabeticOrderedList.Count == 0)
        {
            for (char alphabet = 'a'; alphabet <= 'z'; alphabet++)
            {
                List<string> startLetter = new List<string>();
                alphabeticOrderedList.Add(startLetter);
            }
        }
    }
    
}
[System.Serializable]
public class SixLetter
{
    public string directoryName = "SixLetterWords";
    public List<string> sixLetter = new List<string>();
    public List<List<string>> alphabeticOrderedList = new List<List<string>>(26);

    public SixLetter()
    {
        if (alphabeticOrderedList.Count == 0)
        {
            for (char alphabet = 'a'; alphabet <= 'z'; alphabet++)
            {
                List<string> startLetter = new List<string>();
                alphabeticOrderedList.Add(startLetter);
            }
        }
    }
    
}
[System.Serializable]
public class SevenLetter
{
    public string directoryName = "SevenLetterWords";
    public List<string> sevenLetter = new List<string>();
    public List<List<string>> alphabeticOrderedList = new List<List<string>>(26);

    public SevenLetter()
    {
        if (alphabeticOrderedList.Count == 0)
        {
            for (char alphabet = 'a'; alphabet <= 'z'; alphabet++)
            {
                List<string> startLetter = new List<string>();
                alphabeticOrderedList.Add(startLetter);
            }
        }
    }
    
}
[System.Serializable]
public class EightLetter
{
    public string directoryName = "EightLetterWords";
    public List<string> eightLetter = new List<string>();
    public List<List<string>> alphabeticOrderedList = new List<List<string>>(26);

    public EightLetter()
    {
        if (alphabeticOrderedList.Count == 0)
        {
            for (char alphabet = 'a'; alphabet <= 'z'; alphabet++)
            {
                List<string> startLetter = new List<string>();
                alphabeticOrderedList.Add(startLetter);
            }
        }
    }
    
}

