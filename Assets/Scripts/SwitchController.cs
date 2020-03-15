using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public Sprite SwitchOnSprite;
    public Sprite SwitchOffSprite;

    private bool onSwitch = false;
    private SpriteRenderer SpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    // クリック時にスイッチオン／オフ
    public void ChangeSwitch()
    {
        if (onSwitch)
        {
            SpriteRenderer.sprite = SwitchOffSprite;
        } else
        {
            SpriteRenderer.sprite = SwitchOnSprite;
        }
        onSwitch = !onSwitch;
    }

    // スイッチ状態取得
    public bool OnSwitch()
    {
        return onSwitch;
    }
}
