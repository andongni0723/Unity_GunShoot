using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySeePlayer : BaseSeePlayer
{
    [Header("Component")]
    public GameObject enemySpriteObject;
    public GameObject UICanvas;
    protected override void OnSeePlayer()
    {
        enemySpriteObject.SetActive(true);
        UICanvas.SetActive(true);
    }

    protected override void OnPlayerLeft()
    {
        enemySpriteObject.SetActive(false);
        UICanvas.SetActive(false);
    }
}
