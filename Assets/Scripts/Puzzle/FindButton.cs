using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindButton : MonoBehaviour
{
    [SerializeField] GameObject _canvas;
    [SerializeField] GameObject[] button = new GameObject[3];
    [SerializeField] GameObject[] buttonClone = new GameObject[3];
    private int pastClick = 0;
    int[] buttonRnd;
    List<GameObject> buttonTransform = new List<GameObject>();
    [SerializeField] List<GameObject> buttonTransformClone;

    private void Start()
    {

        for (int i = 0; i < button.Length; i++)
        {
            buttonTransform.Add(button[i]);
        }
        for (int i = 0; i < buttonClone.Length; i++)
        {
            buttonTransform.Add(buttonClone[i]);
        }
    }

    public void CheckButton(int index)
    {

        if (pastClick == index * -1)
        {

            /* button[Mathf.Abs(index) - 1].GetComponent<Image>().color = Color.green;
             buttonTransformClone[Mathf.Abs(index) - 1].SetActive(false);
             buttonTransformClone[Mathf.Abs(index) - 1+4].SetActive(false);
             buttonClone[Mathf.Abs(index) - 1].GetComponent<Image>().color = Color.green;*/

            button[Mathf.Abs(index) - 1].GetComponent<Image>().color = Color.green;
            buttonTransformClone[Mathf.Abs(index) - 1].SetActive(false);
            buttonTransformClone[Mathf.Abs(index) - 1 + button.Length].SetActive(false);
            buttonClone[Mathf.Abs(index) - 1].GetComponent<Image>().color = Color.green;
            pastClick = 0;

            foreach (var item in buttonTransformClone)
            {
                if (item.activeSelf)
                {
                    ChangeButton();
                    return;
                }
            }

            _canvas.SetActive(false);
        }
        else
        {

            pastClick = index;
        }
        buttonRnd = RandomMass();

    }

    public void ChangeButton()
    {

        Debug.Log(buttonRnd[0] + " " + buttonRnd[1] + " " + buttonRnd[2] + buttonRnd[3] + buttonRnd[4] + buttonRnd[5]);

        for (int i = 0; i < buttonTransform.Count; i++)
        {
            buttonTransform[i].transform.position = buttonTransformClone[buttonRnd[i]].transform.position;

        }


    }

    public int[] RandomMass()
    {
        int[] data = new int[buttonTransformClone.Count];

        for (int i = 0; i < data.Length; i++)
        {
            data[i] = i;
        }
        for (int i = data.Length - 1; i >= 0; i--)
        {
            int j = Random.Range(0, 3);
            // обменять значения data[j] и data[i]
            var temp = data[j];
            data[j] = data[i];
            data[i] = temp;
        }
        return data;
    }
}
