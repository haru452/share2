using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI tmpText;
    public Control_Player_info playerInfoController;
   public int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //---------------------------------------------------------------------------------------
    //スコアを制御しているファイルにこの関数を張り付ければいいだけ
    public void displayScore()
    {
       // DisplayRandomNumber();//ここは後で消す。実験用にランダムな数を出しただけ
        if (playerInfoController != null)
        {
            playerInfoController.SetScore(score);//これでplaerINfoのほうにプログラムを渡してるから、ここを変える
        }
        
        if (tmpText != null)
        {
            tmpText.text = score.ToString(); //scoreがintなら
        }
    }
    //-------------------------------------------------------------------------------------------------------
    //ここから下は関係ないけど、ランダムな数値を生成してスコアに代入する関数
    //実際はscoreに点数が入ってばいいだけ
    public void SetScore(int newScore)
    {
        score = newScore;       // もらったスコアを代入
    }
/*
スコアを計算するプログラムに追加してもらうものとして
変数として public ScoreController scoreController; を宣言してもらって、そこにこのスクリプトをアタッチしたオブジェクトを入れてもらう
あとは、スコアを計算するプログラムの中で、scoreController.SetScore(計算したスコア); と呼び出してもらえれば、スコアが更新されるようになります。
*/














    public void DisplayRandomNumber()
    {
        // 1から100までのランダムな整数を生成
        int randomNumber = Random.Range(1, 101);

        // テキストコンポーネントに文字として代入
        if (tmpText != null)
        {
           score = randomNumber;
        }
    }
}
