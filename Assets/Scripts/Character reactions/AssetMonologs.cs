using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character reaction", menuName = "Character reaction")]
public class AssetMonologs : ScriptableObject
{
    [TextArea]
    public  List<string> LookingPhrases;
    [TextArea]
    public List<string> InteractionPhrases;
    [TextArea]
    public List<string> BeforeInteractionPhrases;
}
