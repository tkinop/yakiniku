using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlateController : MonoBehaviour
{
    // 焼き物補充制御全般
    public float ReplenishingTime = 1.0f;
    // TODO 複数種類対応
    // public GameObject RoastedObjectPrefab;
    public GameObject[] RoastedObjectPrefabArray;
    // TODO 確率は選択キャラクターで補正できるようにする
    public float[] RoastedObjectProbabilittyArray;
    // お皿に乗せる物の最大数
    public int PlateSize = 3;
    // TODO 暫定お皿上位置(チョーイマイチ)
    // (0.1,0.6)(-0.5,0.1)(0.6,-0.3)
    private float[] ObjectPositionXList = {0.1f, -0.5f, 0.6f};
    private float[] ObjectPositionYList = {0.6f, 0.1f, -0.3f};

    // タイマー
    private bool IsReplenishing = false;

    // TODO 最終的にprivateに変更
    public bool IsOnObject = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // お皿からものがなくなったら一定時間後補充
        if (!IsOnObject && !IsReplenishing)
        {
            IsReplenishing = true;

            // 補充処理
            StartCoroutine("ReplenishObject");
        }
    }

    // 補充処理
    private IEnumerator ReplenishObject()
    {
        yield return new WaitForSeconds(ReplenishingTime);

        // 補充処理
        for (int i = 0; i < PlateSize; i++)
        {
            // TODO 場所指定
            // とりあえず座標固定で対応
            // (0.1,0.6)(-0.5,0.1)(0.6,-0.3)
            // GameObject obj = Instantiate(RoastedObjectPrefab);
            GameObject obj = Instantiate(RoastedObjectPrefabArray[SelectRoastObject()]);

            // 子指定
            obj.transform.parent = transform;
            obj.transform.localPosition = new Vector3(ObjectPositionXList[i], ObjectPositionYList[i], 0.0f);
        }

        // 初期化
        IsReplenishing = false;
        IsOnObject = true;
    }

    // お皿に物が残っているか判定
    // TODO お皿から物が移動したタイミングで実行されるイメージ
    public void OnObject()
    {
        // TODO 判定処理
        // 子要素にのオブジェクトがすべてお皿にないこと（フラグが立っている）
        IsOnObject = false;
        foreach (Transform child in gameObject.transform)
        {
            // TODO 鉄板移動で子オブジェクトから外した方がいいかも
            if (!child.GetComponent<RoastController>().IsNotOnPlate)
            {
                IsOnObject = true;
            }
        }
    }

    // 補充食べ物選択
    public int SelectRoastObject()
    {
        // 登録されている食べ物の中から抽選選択
        float ProbabilittySum = RoastedObjectProbabilittyArray.Sum();

        float CheckWeight = Random.Range(0, ProbabilittySum);

        for (int i = 0; i < RoastedObjectProbabilittyArray.Length; i++)
        {
            if (CheckWeight <= RoastedObjectProbabilittyArray[i])
            {
                return i;
            }
            CheckWeight -= RoastedObjectProbabilittyArray[i];
        }

        return 0;
    }
}
