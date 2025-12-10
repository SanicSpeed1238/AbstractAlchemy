using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BookUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Image pageImage;            // Main image display
    public TextMeshProUGUI pageText;   // Final text page

    public Button nextButton;
    public Button prevButton;

    [Header("Page Content")]
    public Sprite[] imagePages;        
    [TextArea(3, 10)]
    public string finalTextPage;      

    private int currentPage = 0;       
                                        

    void Start()
    {
        UpdatePage();

        nextButton.onClick.AddListener(NextPage);
        prevButton.onClick.AddListener(PrevPage);
    }

    void NextPage()
    {
        if (currentPage < imagePages.Length)   // last index is the text page
        {
            currentPage++;
            UpdatePage();
        }
    }

    void PrevPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdatePage();
        }
    }

    void UpdatePage()
    {
        
        if (currentPage < imagePages.Length)
        {
            
            pageImage.gameObject.SetActive(true);
            pageText.gameObject.SetActive(false);

            pageImage.sprite = imagePages[currentPage];

            prevButton.interactable = currentPage > 0;
            nextButton.interactable = true;  
        }
        else
        {
            
            pageImage.gameObject.SetActive(false);
            pageText.gameObject.SetActive(true);

            pageText.text = finalTextPage;

            prevButton.interactable = true;
            nextButton.interactable = false; 
        }
    }
}
