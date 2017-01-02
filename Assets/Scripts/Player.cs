using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof (Camera))]

public class Player : MonoBehaviour {
	//mouse
	private Vector3 monsePosition;
	//mouse (world
	private Vector3 monsePositionWorld;
	//angle
	private float mouseAngle;
	private bool loaded = true;


	// Use this for initialization
	void Start () {
		loaded = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(loaded){
			// Vector3でマウス位置座標を取得する
			monsePosition = Input.mousePosition;
			// マウス位置座標をスクリーン座標からワールド座標に変換する
			monsePositionWorld = Camera.main.ScreenToWorldPoint(monsePosition);
			monsePositionWorld.z = 0;

			//マウスの向き計算
			Vector3 targetDir = (monsePositionWorld - transform.position);
			/*
			Vector3 arrowDir = (Arrow.transform.position - transform.position).normalized;
			Vector3.Cross(targetDir, arrowDir);
			mouseAngle = Mathf.Asin(Vector3.Cross(targetDir, arrowDir).z)*Mathf.Rad2Deg*-1;
			*/
			mouseAngle = Mathf.Atan2(targetDir.y, targetDir.x)*Mathf.Rad2Deg;

			//回転
			this.transform.eulerAngles = new Vector3(0, 0, mouseAngle);

		}
	}
}
