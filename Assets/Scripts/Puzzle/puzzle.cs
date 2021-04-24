using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class puzzle : MonoBehaviour, IPointerClickHandler
{
    public GameObject puzzleArea;
    bool onPuzzleArea;

    private void Start()
    {
        onPuzzleArea = false;
        puzzleArea.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
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
