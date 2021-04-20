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
}
