using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rainbow : Collectible
{
    public int score;
    protected override void Collect()
    {
        GameController.gameController.UpdateScore(score);
        PlayerObject.playerObject.transform.Find("Rainbow1").GetComponent<GameObject>().SetActive(true);
        Destroy(gameObject);
    }
}
