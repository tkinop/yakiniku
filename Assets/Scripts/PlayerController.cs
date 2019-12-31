using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // 得点・能力管理
    // 得点
    public int TotalPoint = 0;
    // 能力
    public int Hp = 1;
    public int Mp = 0;
    public int Strength = 0;
    public int Constitution = 0;
    public int Dexterity = 0;
    public int Inteligence = 0;
    public int Lucky = 0;

    public int EatingCount = 0;

    // TODO 名前がいまいち
    private bool Enable = true;

    // Start is called before the first frame update
    void Start()
    {
        // result引継ぎ用（resultで破棄）
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ポイント加算
    public void AddPoint(int AddPoint)
    {
        TotalPoint += AddPoint;
        EatingCount++;
    }

    // 能力値加算
    public void UpdateStatus(Dictionary<string, int> AddStatusDictionary)
    {
        foreach (KeyValuePair<string, int> item in AddStatusDictionary)
        {
            if (item.Key == "Hp") Hp += item.Value;
            if (item.Key == "Mp") Mp += item.Value;

            if (item.Key == "Strength") Strength += item.Value;
            if (item.Key == "Constitution") Constitution += item.Value;
            if (item.Key == "Dexterity") Dexterity += item.Value;
            if (item.Key == "Inteligence") Inteligence += item.Value;
            if (item.Key == "Lucky") Lucky += item.Value;
        }
    }

    // 操作制御
    // TODO 名前がいまいち
    public void SetActive(bool CurrentActive)
    {
        Enable = CurrentActive;
    }
    public bool IsActive()
    {
        return Enable;
    }

}
