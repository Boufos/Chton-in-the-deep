using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogPanel : MonoBehaviour
{
    public Text Text;
    public static DialogPanel Instance;
    [SerializeField]
    private int MaxSymbols;
    private Image _image;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        Text = GetComponentInChildren<Text>();
        _image = GetComponent<Image>();
        Active(false);
        
    }
    public static Coroutine _showPhrase;
    private void OnEnable()
    {
        Text.text = "";
    }
    private void OnDisable()
    {
        Text.text = "";
    }
    public void Active(bool enable)
    {
        Text.text = "";
        _image.enabled = enable;
        Text.enabled = enable;
    }
    public void SetTextBox(string text)
    {
        if (_showPhrase != null)
        {
            Instance.Text.text = "";
            StopCoroutine(_showPhrase);
            Active(false);
        }
        _showPhrase = StartCoroutine(ShowText(text));
    }
    public IEnumerator ShowText(string text)
    {
        int i = 0;
        Active(true);
        if (text.Length < 1)
        {
            text = "    ";
        }
        do
        {
            Text.text += text[i++];
            yield return new WaitForSeconds(0.07f);
        } while (i < text.Length);

        do
        {
            yield return new WaitForSeconds(4);
        } while (false);
        Active(false);
    }
}
