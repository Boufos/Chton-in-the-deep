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
    
    public string LookingPhraseBefore 
    { 
        get {return GetPhrase(_phrases.LookingPhrasesBefore, ref _lookingPhraseIndex);} 
    }
    public string LookingPhraseAfter
    {
        get { return GetPhrase(_phrases.LookingPhrasesAfter, ref _lookingPhraseIndex); }
    }
    public string InteractionPhrase
    {
        get { return GetPhrase(_phrases.LookingPhrasesBefore, ref _interactionPhraseIndex); }
    }
    public string InteractionPhraseAfter
    {
        get => GetPhrase(_phrases.AfterInteractionPhrases, ref _noInteractionPhraseIndex);
    }
    public string InteractionPhraseBefore
    {
        get => GetPhrase(_phrases.BeforeInteractionPhrases, ref _noInteractionPhraseIndex);
    }
    private Coroutine ShowPhrase;
    public IEnumerator ShowText(string text)
    {
        int i = 0;
        DialogPanel.Instance.Active(true);
        if(text.Length < 1)
        {
            text = "    ";
        }
        do
        {
            DialogPanel.Instance.Text.text += text[i++];
            yield return new WaitForSeconds(0.05f);
        } while (i < text.Length);

        do
        {
            yield return new WaitForSeconds(4);
        } while (false);
        DialogPanel.Instance.Active(false);
    }
    public void Reaction(string text)
    {
        if (ShowPhrase != null)
        {
            DialogPanel.Instance.Active(false);
            StopCoroutine(ShowPhrase);
        }
        ShowPhrase = StartCoroutine(ShowText(text));
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
