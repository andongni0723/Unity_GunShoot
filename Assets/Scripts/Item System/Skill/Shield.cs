using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : ThrowItem
{
    public GameObject shieldObject;
    
    public override void Throw(Vector2 startForce)
    {
        base.Throw(startForce);
        shieldObject.SetActive(false);
        AudioManager.Instance.PlayItemAudio(AudioManager.Instance.grenadeThrowAudio);
    }

    protected override void Action()
    {
        base.Action();
        shieldObject.SetActive(true);
        AudioManager.Instance.PlayItemAudio(AudioManager.Instance.shieldOpenAudio);
    }
}
