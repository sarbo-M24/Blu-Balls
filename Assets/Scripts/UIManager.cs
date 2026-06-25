using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{

    public TMP_Text scoreTxt;
    int score = 0;
    private void OnEnable() {
    
        bluBall.OnBlueballCollision += increaseScore;
        RedBall.OnRedballCollision += decreaseScore;
    }


    void increaseScore()
    {
        score++;

        scoreTxt.text = "Score: " + score;

        Debug.Log("Blue ball collided with line!");
    }

    void decreaseScore()
    {
        score--;
        scoreTxt.text = "Score: " + score;
        Debug.Log("Red ball collided with line!");
    }
    private void OnDisable()
    {

        bluBall.OnBlueballCollision -= increaseScore;
        RedBall.OnRedballCollision -= decreaseScore;
    }
}
