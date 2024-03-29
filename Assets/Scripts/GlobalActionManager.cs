using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalActionManager : MonoBehaviour
{
    public static GlobalActionManager instance;
    public bool pointerDownOnTile;
    public List<char> addedChars = new List<char>();

    public List<LetterTileScript> letterTiles = new List<LetterTileScript>();

    public List<LetterTileScript> trackedTiles = new List<LetterTileScript>();
    /// <summary>
    /// List to compare all words found by the user.
    /// </summary>
    public List<string> possibleWords = new List<string>();

    public List<string> userWordsFound = new List<string>();

    public TextMeshProUGUI scoreText;

    public int globalscore = 0;
    public string candidate;
	private void Awake()
	{
        instance = this;
	}
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	private void OnDestroy()
	{
        instance = null; 
	}

    //Limited to 4x4 grids
    public void SetStoredLetters(char[,] letters, int rowsize, int columnsize)
	{
        for(int i=0;i<rowsize; i++)
		{
            for (int j = 0; j < columnsize; j++)
            {
                letterTiles[j + columnsize * i].gridPos = new Vector2(i, j);
                letterTiles[j + columnsize * i].storedLetter = letters[i, j];
                letterTiles[j + columnsize * i].textComponent.text = letterTiles[j + columnsize * i].storedLetter.ToString();
            }
        }

	}

    public void SplitWordsintoList()
	{

	}


    public void ClearTrackedTiles()
	{
        if(trackedTiles.Count>0)
		{
            for(int i=0;i<trackedTiles.Count; i++)
			{
                trackedTiles[i].added = false;
                trackedTiles[i].tileImage.color = trackedTiles[i].inactive;
			}
            trackedTiles.Clear();
		}
	}

    public void GenerateString(char[] charArray)
	{
        candidate = string.Join("",charArray);
        Debug.Log("Candidate String : " + candidate);
        CallForCompare(candidate);
	}


    public void CallForCompare(string candidateString)
	{
        if(WordStorer.instance.wordExists(candidateString.ToLower()) && candidateString.Length > 2)
		{
            if(!userWordsFound.Contains(candidateString))
			{
                scoreText.text = "Score : " + GetScore(candidateString).ToString();
                userWordsFound.Add(candidateString);
            }
            else
			{
                Debug.Log("Word already found");
			}
		}
        else
		{
            Debug.Log("Word doesn't exist");
		}
        //Compare word
        //Assign score if found
	}

    public int GetScore(string candidate)
	{
        //int score = candidate.Length;
        globalscore += candidate.Length;//multiply by bugCount
        return globalscore;
	}

    public void BackToMenu()
	{
        SceneManager.LoadScene(0);
	}

}
