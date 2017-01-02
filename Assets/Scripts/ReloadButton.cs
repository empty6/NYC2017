using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ReloadButton : MonoBehaviour
{

	/// ボタンをクリックした時の処理
	public void OnClick()
	{
		Debug.Log("Button click!");

		// 引数にシーン名を指定する
		SceneManager.LoadScene("Stage1"); 
	}
	void Update () {
		//debug
		if (Input.GetKey(KeyCode.R)) {
			SceneManager.LoadScene("Stage1"); 
		} 
	}
}
	