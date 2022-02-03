using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Security.Cryptography;

public class LetterTileScript : MonoBehaviour
{
    [Header("Letter store fields")]
    public Vector2 gridPos;
    public char storedLetter;
    public Image tileImage;
    public bool added;
    public TextMeshProUGUI textComponent;

    [Header("Event based variables")]
    public EventTrigger myTileEventTriggers;

    [Header("Bonus variables")]
    public GameObject bonus;
    public bool isBonus;

    [Header("Blocker variables")]
    public GameObject blocked;
    public bool isBlocked;

    [Header("Colors")]
    public Color active;
    public Color inactive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHoldDown()
	{
        GlobalActionManager.instance.pointerDownOnTile = true;
        GlobalActionManager.instance.trackedTiles.Add(this);
        GlobalActionManager.instance.addedChars.Add(storedLetter);
        added = true;
        tileImage.color = active;
        //Change Tile color
	}
    public void OnPass()
	{
        if(GlobalActionManager.instance.pointerDownOnTile)
		{
            if(!added)
			{
                GlobalActionManager.instance.trackedTiles.Add(this);
                GlobalActionManager.instance.addedChars.Add(storedLetter);
                added = true;
                tileImage.color = active;
            }
		}
	}

    public void OnReleaseUp()
	{
        //StoreWord
        //Run comparer
        //Call global action manager for the rest
        GlobalActionManager.instance.pointerDownOnTile = false;
        GlobalActionManager.instance.GenerateString(GlobalActionManager.instance.addedChars.ToArray());
        GlobalActionManager.instance.ClearTrackedTiles();
        GlobalActionManager.instance.addedChars.Clear();


    }
}
