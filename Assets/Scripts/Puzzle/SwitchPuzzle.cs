using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchPuzzle : MonoBehaviour
{
    [SerializeField] List<GameObject> buttonPosition;

    private void Start()
    {
        for (int i = 1; i < buttonPosition.Count; i++)
        {
            buttonPosition[i].GetComponent<Image>().color = Color.white;
        }
        buttonPosition[0].GetComponent<Image>().color = Color.green;
    }
    public void buttonIndex(int index)
    {
        if(index==0)
        {
            CheckColor(index);
            CheckColor(index + 1);
            CheckColor(7);
        }
        else if(index == 7)
        {
            CheckColor(index);
            CheckColor(index -1);
            CheckColor(0);
        }
        else
        {
            CheckColor(index);
            CheckColor(index + 1);
            CheckColor(index - 1);
        }


    }

    private void CheckColor(int index)
    {
        Color colorButton = buttonPosition[index].GetComponent<Image>().color;
        if (colorButton==Color.white)
        {
            buttonPosition[index].GetComponent<Image>().color = Color.green;
        }
        else
        {
            buttonPosition[index].GetComponent<Image>().color = Color.white;
        }
    }
}
