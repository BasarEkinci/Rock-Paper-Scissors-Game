using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> playerPrefabs;
    public List<GameObject> enemyPrefabs;
    public GameObject menuPanel;
    //Texts---------------------------------------
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI computerScoreText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI loseText;
    public TextMeshProUGUI drawText;
    //Buttons-------------------------------------
    public UnityEngine.UI.Button rockButton;
    public UnityEngine.UI.Button paperButton;
    public UnityEngine.UI.Button scissorsButton;
    public Button menuButton;
    //Sounds---------------------------------------
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip spawnSound;
    public AudioClip drawSound;
    private AudioSource gameAudio;
    //Transform Variables--------------------------
    public Transform playerPos;
    public Transform enemyPos;
    //Other Variables------------------------------
    private bool isMenuActive = false;
    private int enemyIndex;
    private int playerScore;
    private int computerScore;
    public Destroyer destroyer;

    #region UnityFunctions
    private void Awake()
    {
        gameAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        enemyIndex = Random.Range(0, 3);
        playerScoreText.text = "Player     : " + " " + playerScore;
        computerScoreText.text = "Computer : " + computerScore;
    }
    #endregion

    #region GameplayButtons
    //Gameplay Buttons-------
    public void RockButton()
    {
        ButtonFunction(0, 1, 2);
    }
    public void PaperButton()
    {
        ButtonFunction(1, 2, 0);
    }
    public void ScissorsButton()
    {
        ButtonFunction(2, 0, 1);
    }

    #endregion

    #region MenuFunctions
    //Button Functions
    public void ExitMenuButton()
    {
        menuPanel.SetActive(false);
        menuButton.gameObject.SetActive(true);
        isMenuActive = false;
    }
    public void DeleteTotalScoreButton()
    {
        playerScore = 0;
        computerScore = 0;
    }
    public void ReturnMainMenuButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void MenuButton()
    {
        if (isMenuActive == false)
        {
            menuPanel.gameObject.SetActive(true);
            menuButton.gameObject.SetActive(false);
            isMenuActive = true;
        }
    }
    #endregion

    public void ButtonFunction(int prefabNum, int enemyIndex1, int enemyIndex2)
    {
        //Instantinate enemy prefab and player prefab
        Instantiate(playerPrefabs[prefabNum], playerPos.transform.position, Quaternion.identity);
        Instantiate(enemyPrefabs[enemyIndex], enemyPos.transform.position, Quaternion.identity);
        //Add or substract scores
        if (enemyIndex == enemyIndex1)
        {
            computerScore++;
            loseText.gameObject.SetActive(true);
            winText.gameObject.SetActive(false);
            StartCoroutine(DisableText());
            gameAudio.PlayOneShot(loseSound);
        }
        if (enemyIndex == enemyIndex2)
        {
            playerScore++;
            loseText.gameObject.SetActive(false);
            winText.gameObject.SetActive(true);
            StartCoroutine(DisableText());
            gameAudio.PlayOneShot(winSound);
        }
        if (enemyIndex == prefabNum)
        {
            drawText.gameObject.SetActive(true);
            StartCoroutine(DisableText());
            gameAudio.PlayOneShot(drawSound);
        }
        StartCoroutine(DisableButton());
        gameAudio.PlayOneShot(spawnSound);
    }

    #region IEnumerators
    IEnumerator DisableText()
    {
        yield return new WaitForSecondsRealtime(2);
        loseText.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
        drawText.gameObject.SetActive(false);
    }

    IEnumerator DisableButton()
    {
        rockButton.enabled = false;
        paperButton.enabled = false;
        scissorsButton.enabled = false;
        yield return new WaitForSecondsRealtime(2);
        rockButton.enabled = true;
        paperButton.enabled = true;
        scissorsButton.enabled = true;
    }
    #endregion
}
