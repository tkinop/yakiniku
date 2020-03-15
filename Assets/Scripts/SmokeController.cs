using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeController : MonoBehaviour
{
    public float roastedTotalCount = 0.0f;
    public float smokeAppearTime = 1000000f;

    public Sprite[] SmokeSpriteArray;
    public GameObject SmokePrefab;

    // TODO とてもイマイチ
    private float[] ObjectPositionXList = { -4f, 4f };
    private float[] ObjectPositionYList = { -2.5f, 2.5f };

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 焼き物を焼いた時間で煙が発生
        // 全ての焼き時間の総計で一定間隔で発生させる
        // 換気スイッチで排気可能、排気中はすべての操作ができなくなる
        if (roastedTotalCount > smokeAppearTime)
        {
            // 煙発生
            AppearSmoke();
            // 初期化
            roastedTotalCount = 0;
        }
    }

    // 焼きカウントアップ
    public void AddRoastedCount(float roastedCount)
    {
        roastedTotalCount += roastedCount;
    }

    // 煙発生
    private void AppearSmoke()
    {
        // イメージの中からランダムで表示
        int ImageIndex = Random.Range(0, SmokeSpriteArray.Length);
        GameObject obj = Instantiate(SmokePrefab);
        SpriteRenderer SpriteRenderer = obj.GetComponent<SpriteRenderer>();
        SpriteRenderer.sprite = SmokeSpriteArray[ImageIndex];

        // 子指定
        obj.transform.parent = transform;

        float x = Random.Range(ObjectPositionXList[0], ObjectPositionXList[1]);
        float y = Random.Range(ObjectPositionYList[0], ObjectPositionYList[1]);

        obj.transform.localPosition = new Vector3(x, y, 0.0f);

    }
}