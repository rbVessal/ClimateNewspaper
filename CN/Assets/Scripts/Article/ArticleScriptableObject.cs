using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ArticleValues
{
    public float climate;
    public int money;
    public int reach;
}
public enum ArticleCategory
{
    Advertisement,
    Article
}
[CreateAssetMenu(fileName = "New_Article", menuName = "ScriptableObjects/Article")]

public class ArticleScriptableObject : ScriptableObject
{
    public string headline;
    [TextArea] public string bodyText;
    public float climateChange;
    public int moneyChange;
    public int reachChange;
    public Sprite articleArt;
    public ArticleCategory articleCategory;
    
    public void PublishArticle()//call this function when the article is published to reflect value changes on economy
    {
        //call updates to the game economy and other events here
    }
}
 