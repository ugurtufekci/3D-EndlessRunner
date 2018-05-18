using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private const int COIN_SCORE_AMOUNT = 5;
	public static GameManager Instance { set; get; }

    private PlayerMotor motor;
    private bool isGameStarted = false;

    //UI and the UI fields
    public Text scoreText, coinText, modifierText;
    private float score, coinScore, modifierScore;
    private int lastScore;

    private void Awake()
    {
        Instance = this;
        modifierScore = 1;
        motor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();

        modifierText.text = "x" + modifierScore.ToString("0.0");
        coinText.text = coinScore.ToString("0");
        scoreText.text = score.ToString("0");
    }

    private void Update()
    {
        if(MobileInput.Instance.Tap && !isGameStarted)
        {
            isGameStarted = true;
            motor.StartRunning();
        }

        if (isGameStarted)
        {
            //score up
            score += (Time.deltaTime * modifierScore);
            if(lastScore != (int)score)
            {
                lastScore = (int)score;
                //Debug.Log(lastScore);
                scoreText.text = score.ToString("0");
            }
            
        }
    }

   
    public void GetCoin()
    {
        coinScore++;
        coinText.text = coinScore.ToString("0");
        score += COIN_SCORE_AMOUNT;
        scoreText.text = score.ToString("0");

    }

    public void UpdateModifier(float modifierAmount)
    {
        modifierScore = 1.0f + modifierAmount;
        modifierText.text = "x" + modifierScore.ToString("0.0");
    }
}
