
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class MainMenu : MonoBehaviour
{
    public GameObject informationMenu;
    public GameObject playButton;
    public GameObject informationButton;
    public GameObject returnMainMenuButton;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void InformationButton()
    {
        informationMenu.SetActive(true);
        playButton.SetActive(false);
        informationButton.SetActive(false);
        returnMainMenuButton.SetActive(true);  
    }

    public void ReturnMainMenu()
    {
        informationMenu.SetActive(false);
        returnMainMenuButton.SetActive(false);
        informationButton.SetActive(true);
        playButton.SetActive(true);
    }
}
