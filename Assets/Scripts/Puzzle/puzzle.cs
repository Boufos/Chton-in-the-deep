using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class puzzle : MonoBehaviour, IPointerClickHandler
{
    public GameObject puzzleArea;

    public bool IsReadyToStart;

    private bool onPuzzleArea;


    private void Start()
    {
        onPuzzleArea = false;
        puzzleArea.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
    }

    public void ActivatePuzzle()
    {
        if (onPuzzleArea == false)
        {
            puzzleArea.SetActive(true);
            onPuzzleArea = true;

        }
        else if (onPuzzleArea == true)
        {
            puzzleArea.SetActive(false);
            onPuzzleArea = false;

        }
    }
 
}
