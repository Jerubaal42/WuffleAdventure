using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool allowMovement = true;
    public int score = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (gameController == null)
        {
            gameController = this;
        }
        else { Destroy(this); }
    }

    void Update()
    {
        foreach(IUpdate updateItem in updateList)
        {
            updateItem.UpdateFunction();
        }
        foreach(IUpdate updateRemove in removeUpdateList)
        {
            updateList.Remove(updateRemove);
        }
        removeUpdateList.Clear();
    }

    void FixedUpdate()
    {
        foreach (IFixedUpdate updateItem in fixedUpdateList)
        {
            updateItem.FixedUpdateFunction();
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
    }
}
