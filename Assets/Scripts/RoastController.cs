﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoastController : MonoBehaviour, IPointerClickHandler
{
    // 焼きスピード
    // 移動火で増減するイメージ
    public float roastSpeed = 1.0f;
    public float roastedCount = 0.0f;
    public Sprite[] RoastedImageSpriteArray;
    public Sprite[] EatingImageSpriteArray;
    public float ChangeSpriteCounte = 100.0f;

    SpriteRenderer SpriteRenderer;
    private int ArrayIndex;
    private int CurrentArrayIndex;
    private PlayerController PlayerController;
    private EatController EatController;
    private AudioSource audioSource;
    private SmokeController SmokeController;
    private VentilateController ventilateController;
    private EatController eatController;

    // TODO privateの方がいいかも
    public bool IsRoasted = false;
    public bool IsNotOnPlate = false;
    public bool OnFireArea = false;

    // スコア（焼き状態で得点を変更）
    // TODO 抽象化により分離予定
    // TODO スコア算出方法を変更するかも
    public int[] ScoreForStatusArray = {50, 200, -10};

    // Start is called before the first frame update
    void Start()
    {
        ArrayIndex = 0;
        SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        PlayerController = GameObject.Find("Player").GetComponent<PlayerController>();
        EatController = GameObject.Find("Own").GetComponent<EatController>();
        audioSource = gameObject.GetComponent<AudioSource>();
        SmokeController = GameObject.Find("Smokes").GetComponent<SmokeController>();

        ventilateController = GameObject.Find("Ventilation").GetComponent<VentilateController>();
        eatController = GameObject.Find("Own").GetComponent<EatController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsRoasted)
        {
            // 焼き音再生
            if (!audioSource.isPlaying) audioSource.Play();

            // 焼きスペースにある場合は焼き値を加えていく
            // TODO 加算タイミングが一定じゃない
            float roastedNum = roastSpeed;
            if (OnFireArea) {
                // TODO 加算倍率
                roastedNum = roastSpeed * 1.2f;
            }
            roastedCount += roastedNum;
            SmokeController.AddRoastedCount(roastedNum);
            Debug.Log(roastedCount);

            // あるタイミングで表示グラフィックを変えていく（焼きグラ）
            // TODO タイミングは要変更
            if (roastedCount > ChangeSpriteCounte + (ArrayIndex * ChangeSpriteCounte) && RoastedImageSpriteArray.Length > ArrayIndex)
            {
                CurrentArrayIndex = ArrayIndex;
                // 表示グラ変更
                SpriteRenderer.sprite = RoastedImageSpriteArray[ArrayIndex];

                // 焼き音量変更
                audioSource.volume = (float)(audioSource.volume * 0.7);

                ArrayIndex++;
            }
        }
    }

    // 鉄板上クリック時イベント
    public void OnPointerClick(PointerEventData eventData)
    {
        if (PlayerController.IsActive() && IsNotOnPlate && !eatController.IsEating())
        {
            // 食べ終わるまで他操作はできない（ゲーム内時間は進行）
            PlayerController.SetActive(false);

            // 焼き音停止
            audioSource.Stop();

            // 焼き終了
            IsRoasted = false;

            // 自分のお皿に移動
            Transform EatControllerTransform = EatController.transform;
            transform.parent = EatControllerTransform;
            transform.position = new Vector3(EatControllerTransform.position.x, EatControllerTransform.position.y - 2, 0.0f);
        }
    }

    // 食べる処理
    public void EatObject()
    {
        // オブジェクトを消して得点／能力の変換処理
        PlayerController.AddPoint(ScoreForStatusArray[CurrentArrayIndex]);
        // TODO 能力変換は保留
        //PlayerController.UpdateStatus();

        // 操作可能にする
        PlayerController.SetActive(true);

        // オブジェクト削除
        Destroy(this.gameObject);
    }

    // 食べてる時のグラ変更
    public void ChangeEatingEmage()
    {
        // 表示グラ変更
        SpriteRenderer.sprite = EatingImageSpriteArray[CurrentArrayIndex];
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "FireArea" && !OnFireArea)
        {
            Debug.Log("移動火に接触");
            OnFireArea = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "FireArea" && OnFireArea)
        {
            Debug.Log("移動火外");
            OnFireArea = false;
        }
    }
}
