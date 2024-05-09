using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoardArticleDisplayer : MonoBehaviour
{


    [Tooltip("The UI displaying the article")]
    public List<ArticleDisplay> displays;
    [Tooltip("The articles to be displayed")]
    public List<ArticleScriptableObject> articlesToDisplay;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddArticleToDisplay(ArticleScriptableObject article)
    {
        articlesToDisplay.Add(article);
    }
}
