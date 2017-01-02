using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartButton : MonoBehaviour
{

    /// ボタンをクリックした時の処理
    public void OnClick()
    {
        //Debug.Log("Button click!");
		// 引数にシーン名を指定する
		// Build Settings で確認できる sceneBuildIndex を指定しても良い
		SceneManager.LoadScene("Stage1"); 
    }
}
