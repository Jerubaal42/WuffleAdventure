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

    private void Awake()
    {
        gameController = this;
    }

    void Update()
    {
        foreach(IUpdate updateItem in updateList)
        {
            updateItem.UpdateFunction();
        }
    }

    void FixedUpdate()
    {
        foreach (IFixedUpdate updateItem in fixedUpdateList)
        {
            updateItem.FixedUpdateFunction();
        }
    }
}
