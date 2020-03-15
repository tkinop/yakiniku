using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentilateController : MonoBehaviour
{
    public GameObject Switch;
    [SerializeField] GameObject Ventilater;

    public float timeOut;
    private float timeElapsed;

    private GameObject Smokes;
    private SwitchController switchController;
    private PlayerController playerController;
    private bool isVentilating = false;

    // Start is called before the first frame update
    void Start()
    {
        switchController = Switch.GetComponent<SwitchController>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        Smokes = GameObject.Find("Smokes");
    }

    // Update is called once per frame
    void Update()
    {
        // スイッチ監視
        if (switchController.OnSwitch())
        {
            playerController.SetActive(false);

            // 換気状態に更新
            if (!isVentilating)
            {
                isVentilating = true;
                // 換気画像表示
                Ventilater.SetActive(true);
            }

            if (isVentilating)
            {
                // TODO 風オブジェクトを揺らす（場所移す？）
                iTween.ShakePosition(Ventilater, iTween.Hash("x", 0.3f, "y", 0.3f, "time", 0.5f));

                // TODO 焼きオブジェクト取れない処理
            }
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= timeOut)
            {
                // 換気処理
                RemoveSmokes();

                timeElapsed = 0.0f;
            }
        } else
        {
            playerController.SetActive(true);

            // 換気停止状態に更新
            if (isVentilating)
            {
                isVentilating = false;
                // 換気画像表示
                Ventilater.SetActive(false);
            }
        }
    }

    // 煙除去
    public void RemoveSmokes()
    {
        // 一定時間毎に煙除去
        // TODO なんかうまくいかない！！
        Transform[] smokeChildren = Smokes.GetComponentsInChildren<Transform>();
        if (smokeChildren.Length > 0)
        {
            var smoke = smokeChildren[Random.Range(0, smokeChildren.Length)];
            if (smoke.gameObject.name != "Smokes") Destroy(smoke.gameObject);
        }
    }
}
