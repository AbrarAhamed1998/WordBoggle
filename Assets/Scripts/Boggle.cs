using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boggle : MonoBehaviour
{
    
    //public static readonly int n = dictionary.Length;
    public static readonly int M = 4, N = 4;
    // Start is called before the first frame update
    void Start()
    {
        //Ideally split the words up into 26 lists based on first letter
        FindSolution();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FindSolution()
	{

    }
    static bool isWord(string str)
    {
        return WordStorer.instance.wordExists(str);
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
            GlobalActionManager.instance.possibleWords.Add(str);
            Debug.Log(str);

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
    static void findWords(char[,] boggle)
    {
        // Mark all characters as not visited
        bool[,] visited = new bool[M, N];

        // Initialize current string
        string str = "";

        // Consider every character and look for all words
        // starting with this character
        for (int i = 0; i < M; i++)
            for (int j = 0; j < N; j++)
                findWordsUtil(boggle, visited, i, j, str);
    }

    // Driver Code
    public static void Main(string[] args)
    {
        char[,] boggle = { { 'G', 'I', 'Z', 'R'},
                           { 'U', 'E', 'K', 'T'},
                           { 'Q', 'S', 'E' ,'Y'},
                           { 'P', 'R', 'F', 'X'}};

        Debug.Log("Following words of " +
                          "dictionary are present");
        GlobalActionManager.instance.SetStoredLetters(boggle, M, N);
        findWords(boggle);
    }
}
