using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BookUI : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI pageText;
    public Image logoImage;

    public Button nextButton;
    public Button prevButton;

    private int currentPage = 0;

    // Page 0 will be the image.
    // Pages 1+ will be text.
    private string[] textPages = {
        "Welcome to Abstract Alchemy! This book will teach you how to play.",
        "Use the grip buttons to grab objects in the world.",
        "Press the trigger to interact or use tools.",
        "Collect all the items and mix them into different potions. Good luck!"
    };

    void Start()
    {
        UpdatePage();

        nextButton.onClick.AddListener(NextPage);
        prevButton.onClick.AddListener(PrevPage);
    }

    void NextPage()
    {
        if (currentPage < textPages.Length)  // logo is page 0, text pages go 1..N
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
        if (currentPage == 0)
        {
            // Show image page
            logoImage.gameObject.SetActive(true);
            pageText.gameObject.SetActive(false);

            prevButton.interactable = false;           // can't go back from logo
            nextButton.interactable = true;            // can always go forward if text exists
        }
        else
        {
            // Show text page
            logoImage.gameObject.SetActive(false);
            pageText.gameObject.SetActive(true);

            pageText.text = textPages[currentPage - 1];  // subtract 1 because page 1 = textPages[0]

            prevButton.interactable = true;
            nextButton.interactable = currentPage < textPages.Length;
        }
    }
}

