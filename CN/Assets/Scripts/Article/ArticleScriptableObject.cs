using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ensure the ScriptableObject can be created via the Unity Editor's Assets menu
[CreateAssetMenu(fileName="New_Article", menuName = "ScriptableObjects/Article")]
public class ArticleScriptableObject : ScriptableObject
{
    public string headline;
    [TextArea] public string bodyText;
    public int climateChange;
    public int moneyChange;
    public int reachChange;
    public Texture2D articleArt;

}
 