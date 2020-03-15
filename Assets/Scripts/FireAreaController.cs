using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAreaController : MonoBehaviour
{
    // 移動火コントローラ
    public float moveTime = 3.0f;
    private float movingTime = 0.0f;

    private bool isArraival = false;
    private Vector2 moveTarget;
    private GameObject roastedField;

    // TODO 取得方法
    public float moveTargetMaxX =  2.5f;
    public float moveTargetMinX = -2.5f;
    public float moveTargetMaxY =  1.0f;
    public float moveTargetMinY = -1.0f;

    public float moveSpeed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        this.ChangeMoveDirection();
        roastedField = GameObject.Find("RoastedField");
    }

    // Update is called once per frame
    void Update()
    {
        // 鉄板上を移動（鉄板から出ない）
        // ランダム移動
        // 一定時間同じ方向に移動
        // 端にぶつかったら時間を待たずに移動方向を修正
        movingTime += Time.deltaTime;
        if (movingTime >= moveTime)
        {
            this.ChangeMoveDirection();
            movingTime = 0.0f;
        }

        transform.position = Vector2.MoveTowards(transform.position, moveTarget, moveSpeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "OutOfRoastedField")
        {
            Debug.Log("端に接触");

            // 方向変換処理
            this.ChangeMoveDirection();
        }
    }

    public void ChangeMoveDirection()
    {
        // 方向変換処理
        // 方向変更
        // 移動時間の初期化

        // TODO 変えるかも）方針 行き先を指定して移動
        if (!isArraival)
        {
            moveTarget = new Vector2(Random.Range(moveTargetMinX, moveTargetMaxX), Random.Range(moveTargetMinX, moveTargetMaxX));
        }
    }
}
