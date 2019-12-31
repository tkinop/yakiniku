using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultController : MonoBehaviour
{
    public GameObject Meat;
    public GameObject Score;
    public GameObject Buttons;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        PlayerController playerController = player.GetComponent<PlayerController>();

        Text meatText = GameObject.Find("MeatText").GetComponent<Text>();
        meatText.text = "x " + playerController.EatingCount.ToString();

        Text scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        scoreText.text = ": " + playerController.TotalPoint.ToString();

        Destroy(player);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Retry()
    {
        SceneManager.LoadScene("main");
    }

    public void Title()
    {
        SceneManager.LoadScene("title");
    }
}
