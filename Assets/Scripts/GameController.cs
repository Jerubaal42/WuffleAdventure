using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IUpdate
{
    void UpdateFunction();
}

public interface IFixedUpdate
{
    void FixedUpdateFunction();
}

public class GameController : MonoBehaviour
{
    public static GameController gameController;
    public List<IUpdate> updateList = new List<IUpdate>();
    public List<IFixedUpdate> fixedUpdateList = new List<IFixedUpdate>();
    public List<IUpdate> removeUpdateList = new List<IUpdate>();
    public List<IFixedUpdate> removeFixedUpdateList = new List<IFixedUpdate>();
    public bool allowMovement = false;
    public int score = 0;
    public int totalScore = 0;
    private float initTimescale;
    private bool hasAllowedMovement;
    private bool isPaused = false;
    public bool isMenu = true;
    public GameObject pauseButton;
    public GameObject pauseCanvas;
    public GameObject pauseImage;
    public GameObject scoreImage;
    public Text scoreText;
    public Image loadImage;
    public int charType = 0;
    public Sprite[] pauseSprites;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (gameController == null)
        {
            gameController = this;
        }
        else { Destroy(gameObject); }
        scoreText.text = "Score: " + score;
    }

    void Update()
    {
#if UNITY_STANDALONE
        if (Input.GetButtonDown("Pause") && !isMenu)
        {
            PauseFunction();
        }
#endif
        foreach (IUpdate updateItem in updateList)
        {
            if (updateItem == null) { removeUpdateList.Add(updateItem); }
            else { updateItem.UpdateFunction(); }
        }
        foreach (IUpdate updateRemove in removeUpdateList)
        {
            updateList.Remove(updateRemove);
        }
        removeUpdateList.Clear();
    }

    void FixedUpdate()
    {
        foreach (IFixedUpdate updateItem in fixedUpdateList)
        {
            if (updateItem == null) { removeFixedUpdateList.Add(updateItem); }
            else { updateItem.FixedUpdateFunction(); }
        }
        foreach (IFixedUpdate updateRemove in removeFixedUpdateList)
        {
            fixedUpdateList.Remove(updateRemove);
        }
        removeFixedUpdateList.Clear();
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        Debug.Log(score);
        scoreText.text = "Score: " + (totalScore + score);
    }

    public void ConglomerateScore()
    {
        totalScore += score;
        ClearScore();
        scoreText.text = "Score: " + (totalScore + score);
    }

    public void ClearScore()
    {
        score = 0;
        scoreText.text = "Score: " + (totalScore + score);
    }

    public void ResetScore()
    {
        totalScore = 0;
        score = 0;
        scoreText.text = "Score: " + (totalScore + score);
    }

    public void EmptyUpdate()
    {
        foreach (IUpdate updateItem in updateList)
        {
            removeUpdateList.Add(updateItem);
        }
        foreach (IFixedUpdate updateItem in fixedUpdateList)
        {
            removeFixedUpdateList.Add(updateItem);
        }
    }

    public void PauseFunction()
    {
        if (!isMenu)
        {
            if (isPaused)
            {
                Time.timeScale = initTimescale;
                allowMovement = hasAllowedMovement;
                pauseButton.SetActive(true);
                pauseCanvas.SetActive(false);
                isPaused = false;
            }
            else
            {
                initTimescale = Time.timeScale;
                hasAllowedMovement = allowMovement;
                Time.timeScale = 0;
                allowMovement = false;
                pauseImage.GetComponent<Image>().sprite = pauseSprites[Random.Range(0, pauseSprites.Length)];
                pauseButton.SetActive(false);
                pauseCanvas.SetActive(true);
                isPaused = true;
            }
        }
    }

    public void ModeSwitch()
    {
        if (isMenu)
        {
            isMenu = false;
            pauseButton.SetActive(true);
            pauseCanvas.SetActive(false);
            scoreImage.SetActive(true);
            Time.timeScale = 1f;
        }
        else
        {
            isMenu = true;
            pauseButton.SetActive(false);
            pauseCanvas.SetActive(false);
            scoreImage.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void SetCharacter(int type)
    {
        charType = type;
    }
}
