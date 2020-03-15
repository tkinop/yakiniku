using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatController : MonoBehaviour
{
    private bool isEating = false;

    // TODO 暫定食べる所要時間
    public float eatingTime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 自皿に物が置いてある場合
        // →食べてる最中
        // →食べ物の移動、選択ができない
        if (gameObject.transform.childCount > 0 && !isEating)
        {
            isEating = true;

            // 自分の更には1個のオブジェクトのみ
            // 食事中処理
            foreach (Transform child in gameObject.transform)
            {
                RoastController roastController = child.GetComponent<RoastController>();
                StartCoroutine("Eating", roastController);
            }
        }        
    }

    // 食事中処理
    private IEnumerator Eating(RoastController meatController)
    {
        yield return new WaitForSeconds(eatingTime/2);

        // 食べ物画像変更
        meatController.ChangeEatingEmage();

        yield return new WaitForSeconds(eatingTime / 2);

        // 食べる実処理
        meatController.EatObject();

        // 食事完了
        isEating = false;
    }

    public bool IsEating()
    {
        return isEating;
    }
}
