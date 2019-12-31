using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    // ゲーム全般管理
    private Text ScorePointText;
    private PlayerController PlayerController;
    private EventSystem eventSystemGameObject;

    // TODO もっといい方法ある？
    public GameObject readyObject;
    public GameObject goObject;
    public GameObject timeUpGameObject;

    public bool isbeforeStart = true;
    public float waitTimeForStart = 1.0f;
    public float waitTimeForResult = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        ScorePointText = GameObject.Find("ScorePoint").GetComponent<Text>();
        PlayerController = GameObject.Find("Player").GetComponent<PlayerController>();

        Ready();
    }

    // Update is called once per frame
    void Update()
    {
        // ポイントを反映
        ScorePointText.text = PlayerController.TotalPoint.ToString();
    }

    public bool IsBeforeStart()
    {
        return isbeforeStart;
    }

    private void Ready()
    {
        eventSystemGameObject = GameObject.FindObjectOfType<EventSystem>();
        eventSystemGameObject.enabled = false;

        // 開始表示
        StartCoroutine("BeforeStart");
    }

    private IEnumerator BeforeStart()
    {
        // ready表示
        readyObject.SetActive(true);
        yield return new WaitForSeconds(waitTimeForStart);
        readyObject.SetActive(false);

        // go表示
        goObject.SetActive(true);
        yield return new WaitForSeconds(waitTimeForStart);
        goObject.SetActive(false);

        // 操作可能になる
        isbeforeStart = false;
        eventSystemGameObject.enabled = true;
    }

    // タイムアップ処理
    public void TimeUp()
    {
        timeUpGameObject.SetActive(true);

        // 一定時間経過後リザルトシーンへ遷移
        StartCoroutine("GoResult");
    }

    private IEnumerator GoResult()
    {
        yield return new WaitForSeconds(waitTimeForResult);

        // 一定時間経過後リザルトシーンへ遷移
        SceneManager.LoadScene("result");
    }
}
