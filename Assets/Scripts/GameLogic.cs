using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    [SerializeField] NumberSlotrScript player1, player2;
    [SerializeField] int player1Points, player2Points;
    [SerializeField] TextMeshProUGUI winner;
    [SerializeField] TextMeshPro player1Text, player2Text;
    [SerializeField] int player1Score, player2Score;
    int round;
    float time,hold;
    bool roundDone;
    [SerializeField] Button exit, playAgain;
    // Start is called before the first frame update
    void Start()
    {
        hold = 1.5f;
        time = 0;
        round = 1;
        player1Points = 0;
        player2Points = 0;
        winner.text = "Round " + round;
        exit.gameObject.SetActive(false);
        playAgain.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (roundDone)
        {
            time += Time.deltaTime;
            if (time > hold)
            {
                player1.spinDone = false;
                player2.spinDone = false;
                player1.spinable = true;
                player2.spinable = true;
                round++;
                winner.text = "Round " + round;
                player1Text.text = player1Points.ToString();
                player2Text.text = player2Points.ToString();
                roundDone = false;
            }

        }
        else
        {
            if (player1.spinDone)
            {
                player1Score = player1.numberResult;
            }

            if (player2.spinDone)
            {
                player2Score = player2.numberResult;
            }

            if (player1.spinDone && player2.spinDone)
            {
                roundDone = true;
                if (IsTie())
                {
                    winner.text = "Tie";
                    NewRound();
                    time = 0;

                }
                else if (player1Score != 0 && player2Score != 0)
                {
                    //player1 won the round
                    if (HigherOrLower())
                    {
                        player1.RoundWon();
                        player1Points++;
                        player2.RoundLost();
                        winner.text = "player1 Higher";
                        NewRound();
                        time = 0;
                    }
                    //player2 won the round
                    else
                    {
                        player2.RoundWon();
                        player2Points++;
                        player1.RoundLost();
                        winner.text = "player2 Higher";
                        NewRound();
                        time = 0;
                    }
                }
            }
            //player1 won the game
            else if (player1Points >= 5)
            {
                //player1 won the game
                player1.GameWon();
                player2.GameLost();
                winner.text = "Player1 Win";
                player1.spinable = false;
                player2.spinable = false;
                exit.gameObject.SetActive(false);
                playAgain.gameObject.SetActive(false);
            }
            //player2 won the game
            else if (player2Points >= 5)
            {
                //player2 won the game
                player2.GameWon();
                player2.GameLost();
                winner.text = "Player2 Win";
                player1.spinable = false;
                player2.spinable = false;
                exit.gameObject.SetActive(true);
                playAgain.gameObject.SetActive(true);
            }
        }
    }

    public bool IsTie()
    {
        if(player1Score == player2Score && player1Score != 0)
        {
            return true;
        }
        else { return false; }
    }
    
    //check if player1 number is higher then player2
    public bool HigherOrLower()
    {
        bool win = (player2Score < player1Score);
        return win;
    }

    public void NewRound()
    {
        player1Score = 0;
        player2Score = 0;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
