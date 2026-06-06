using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Control_Player_info : MonoBehaviour
{
    public TextMeshProUGUI tmpText; 
    public TMP_InputField Field;
    public int playerScore;

    // パスを何度も使うので、クラスの共通変数（プロパティ）にしておくと便利です
    private string FilePath => Path.Combine(Application.dataPath, "UI_assets", "Player Info", "data.txt");

    void Start()
    {
        readFile();
        playerScore = 0; // スコアの初期値を設定
    }

    void Update()
    {
        
    }

    // ファイル読み込み
    void readFile() 
    {
        // 起動時にファイルがない場合のエラーを防ぐため、存在チェックを入れる
        if (File.Exists(FilePath))
        {
            StreamReader reader = new StreamReader(FilePath, Encoding.Default);
            var text = reader.ReadToEnd();
            reader.Close();
            tmpText.text = text;
        }
        else
        {
            tmpText.text = "データがありません。";
        }
    }

    // ボタンを押したときに実行するデータ保存処理
    public void inputdata()
    {
        // すでにインスペクターで TMP_InputField を取得しているので、GetComponent は不要です
        string input = Field.text;

        // 空っぽの状態で保存されないように一応チェック
        if (string.IsNullOrEmpty(input)) return;

        try
        {
            // StreamWriter を使ってファイルに書き込む（Encoding.Default で文字化け防止）
            // ※ false にすると上書き保存になります。毎回追記したい場合は true にしてください。
            StreamWriter writer = new StreamWriter(FilePath, true, Encoding.Default);
          writer.Write(input);
           writer.Close();
          //inputScore_rand(); // ランダムな数値も一緒に保存する
           inputscore_fix();

            Debug.Log("データを保存しました: " + input);

            // 保存した後にファイルを再読み込みして、画面のテキスト(tmpText)を更新する！
            readFile();

            // 入力欄をパッと空っぽにする（お好みで）
            Field.text = "";
        }
        catch (Exception e)
        {
            Debug.LogError("保存に失敗しました: " + e.Message);
        }
    }

    void inputScore_rand()
    {
         StreamWriter writer = new StreamWriter(FilePath, true, Encoding.Default);
        int randomNumber = UnityEngine.Random.Range(1, 101);
        writer.Write(" ");
        writer.WriteLine(randomNumber);
        writer.Close();
    }
    void inputscore_fix()
    {
        StreamWriter writer = new StreamWriter(FilePath, true, Encoding.Default);
        writer.Write(" ");
        writer.WriteLine(playerScore);
        writer.Close();
    }
    public void SetScore(int newScore)
    {
        playerScore = newScore;       // もらったスコアを代入
    }
}
