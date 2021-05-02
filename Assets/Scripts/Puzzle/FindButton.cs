using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindButton : MonoBehaviour
{
   [SerializeField] GameObject[] button = new GameObject[3];
    [SerializeField] GameObject[] buttonClone = new GameObject[3];
    List<GameObject> _listButton = new List<GameObject>();
    private int pastClick=0;
    GameObject click;

    private void Start()
    {
        for(int i =0; i<button.Length;i++)
        {
            _listButton.Add(button[i]);
        }
    }

    public void CheckButton(int index)
    {
        // Color color = _listButton[index].GetComponent<Image>().color;
        // _listButton[index].GetComponent<Image>().color = Color.yellow;
        if (pastClick==index*-1)
        {
            Debug.Log("кнопка становится зелёной");
            button[Mathf.Abs(index)-1].SetActive(false);
            buttonClone[Mathf.Abs(index) - 1].SetActive(false);
            pastClick = 0;
        }
        else
        {
            pastClick = index;
        }
       //  _listButton[index].GetComponent<Image>().color = color;
        
        
    }
}
