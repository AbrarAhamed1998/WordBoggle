using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System;
using UnityEngine;

public class Boggle : MonoBehaviour
{
    public string[] boggleDice = {
 "AAEEGN", "ABBJOO", "ACHOPS", "AFFKPS",
 "AOOTTW", "CIMOTU", "DEILRX", "DELRVY",
 "DISTTY", "EEGHNW", "EEINSU", "EHRTVW",
 "EIOSST", "ELRTTY", "HIMNQU", "HLNNRZ"
};

    public string[] shuffledBoggleDice = new string[16];
    public char[,] boggle = new char[4,4];
    //public static readonly int n = dictionary.Length;
    public static readonly int M = 4, N = 4;
	// Start is called before the first frame update
	public void Start()
	{
        FindSolution();
	}

    public void ShuffleBoggleDice()
	{
        shuffledBoggleDice = boggleDice;
        string tempString;
        int r;
        for(int i=0;i<shuffledBoggleDice.Length;i++)
		{
            r = UnityEngine.Random.Range(0, shuffledBoggleDice.Length);
            tempString = shuffledBoggleDice[r];
            shuffledBoggleDice[r] = shuffledBoggleDice[i];
            shuffledBoggleDice[i] = tempString;
		}
	}
    public void RandomizeBoggle()
	{
        string currentDiceString;
        for(int i = 0; i< M; i++)
		{
            for(int j=0; j<N; j++)
			{
                currentDiceString = shuffledBoggleDice[j + N * i];
                boggle[i, j] = currentDiceString[UnityEngine.Random.Range(0,currentDiceString.Length)];
                //boggle[i, j] = Convert.ToChar(UnityEngine.Random.Range(0, 26) + (int)'a');
                
			}
		}
	}


	public void FindSolution()
	{
         Main(null);
    }
    static bool isWord(string str)
    {
        if(str.Length > 2)
		{
            return WordStorer.instance.wordExists(str.ToLower());
        }
        else
		{
            return false;
		}
    }
    // A recursive function to print all words present on boggle
    static void findWordsUtil(char[,] boggle, bool[,] visited,
                                  int i, int j, string str)
    {
        // Mark current cell as visited and
        // append current character to str
        visited[i, j] = true;
        str = str + boggle[i, j];

        // If str is present in dictionary,
        // then print it
        if (isWord(str))
		{
            Debug.Log(str);
            GlobalActionManager.instance.possibleWords.Add(str);
            
        }
            

        // Traverse 8 adjacent cells of boggle[i,j]
        for (int row = i - 1; row <= i + 1 && row < M; row++)
            for (int col = j - 1; col <= j + 1 && col < N; col++)
                if (row >= 0 && col >= 0 && !visited[row, col])
                    findWordsUtil(boggle, visited, row, col, str);

        // Erase current character from string and
        // mark visited of current cell as false
        str = "" + str[str.Length - 1];
        visited[i, j] = false;
    }

    // Prints all words present in dictionary.
    void findWords(char[,] boggle)
    {
        // Mark all characters as not visited
        bool[,] visited = new bool[M, N];

        // Initialize current string
        string str = "";

        // Consider every character and look for all words
        // starting with this character
        for (int i = 0; i < M; i++)
		{
            for(int j = 0; j < N; j++)
			{
                Debug.Log("Element : " + i + "," + j);
                
                findWordsUtil(boggle, visited, i, j, str);
            }
    
        }
            
    }

    // Driver Code
    public void Main(string[] args)
    {
        Debug.Log("Boggle started");
        ShuffleBoggleDice();
        RandomizeBoggle();
        /*char[,] boggle = { { 'G', 'I', 'Z', 'R'},
                           { 'U', 'E', 'K', 'T'},
                           { 'Q', 'S', 'E' ,'Y'},
                           { 'P', 'R', 'F', 'X'}};*/

        //Debug.Log("Following words of " +
                          //"dictionary are present");
        GlobalActionManager.instance.SetStoredLetters(boggle, M, N);
        //findWords(boggle);
        //SetLevelData(boggle, GlobalActionManager.instance.possibleWords);
        Debug.Log("Boggle Ended");
    }

    public static void SetLevelData(char[,] boggle,List<string> wordsInboggle)
	{
        ///Bugs and blocked tiles may be set randomly on start
        LevelData lev = new LevelData();
        lev.boggle = boggle;
        lev.wordsInBoggle = wordsInboggle;
        string writeData = JsonUtility.ToJson(lev,true);
        File.WriteAllText(Path.Combine(Application.dataPath, "Resources/Levels/lvl1.txt"),writeData);
	}
}
