using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    [SerializeField] private float seconds;
    //　前のUpdateの時の秒数
    private float oldSeconds;
    //　タイマー表示用テキスト
    private Text timeCount;
    private GameManager gameManager;
    public float initTime = 60f;

    void Start()
    {
        // float initTime = 30f;
        seconds = initTime;
        oldSeconds = initTime;
        timeCount = GameObject.Find("TimeCount").GetComponent<Text>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        timeCount.text = ((int)initTime).ToString("00");

    }

    void Update()
    {
        if (!gameManager.IsBeforeStart())
        {
            seconds -= Time.deltaTime;

            // タイムアップ処理
            if (seconds <= 0) gameManager.TimeUp();

            //　値が変わった時だけテキストUIを更新
            if (seconds > 0 && (int)seconds != (int)oldSeconds)
            {
                timeCount.text = ((int)seconds).ToString("00");
            }
            oldSeconds = seconds;
        }
    }
}
