using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BookUI : MonoBehaviour
{
    public TextMeshProUGUI pageText;
    public Button nextButton;
    public Button prevButton;

    private int currentPage = 0;
    private string[] pages = {
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
        if (currentPage < pages.Length - 1)
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
        pageText.text = pages[currentPage];
        prevButton.interactable = currentPage > 0;
        nextButton.interactable = currentPage < pages.Length - 1;
    }
}

