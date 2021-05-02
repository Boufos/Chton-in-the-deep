using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterReaction : MonoBehaviour
{
    [SerializeField]
    private AssetMonologs _phrases;
    private int _lookingPhraseIndex = 0;

    private int _interactionPhraseIndex = 0;

    private int _noInteractionPhraseIndex = 0;
    
    public string LookingPhrase 
    { 
        get => GetPhrase(_phrases.LookingPhrases, ref _lookingPhraseIndex);
    }
    public string InteractionPhrase
    {
        get => GetPhrase(_phrases.InteractionPhrases, ref _interactionPhraseIndex); 
    }
    public string BeforeInteractionPhrase
    {
        get => GetPhrase(_phrases.BeforeInteractionPhrases, ref _noInteractionPhraseIndex);
    }


    public void SetReaction(string text)
    {
        DialogPanel.Instance.SetTextBox(text);
    }
    private string GetPhrase(List<string> phrases, ref int index)
    {
        if(phrases.Count <= 0)
        {
            return "Текста нету -_-";
        }
        if (index >= phrases.Count)
        {
            index = 0;
        }
        return phrases[index++];
    }
}
