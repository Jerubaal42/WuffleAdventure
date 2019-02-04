using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectible
{
    public int score;
    protected override void Collect()
    {
        GameController.gameController.UpdateScore(score);
        Destroy(gameObject);
    }
}
