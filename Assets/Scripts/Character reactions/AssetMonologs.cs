using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character reaction", menuName = "Character reaction")]
public class AssetMonologs : ScriptableObject
{
    [TextArea]
    public  List<string> LookingPhrasesBefore;
    [TextArea]
    public List<string> LookingPhrasesAfter;
    [TextArea]
    public List<string> AfterInteractionPhrases;
    [TextArea]
    public List<string> InteractionPhrases;
    [TextArea]
    public List<string> BeforeInteractionPhrases;
}
