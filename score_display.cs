using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class score_display : MonoBehaviour
{
    public TextMeshProUGUI tmpText;
    int score;
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
        DisplayRandomNumber();
        if (tmpText != null)
        {
            tmpText.text = score.ToString(); //scoreがintなら
        }
    }
    //-------------------------------------------------------------------------------------------------------
    //ここから下は関係ないけど、ランダムな数値を生成してスコアに代入する関数
    //実際はscoreに点数が入ってばいいだけ
















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
