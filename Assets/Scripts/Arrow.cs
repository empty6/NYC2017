using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Arrow : MonoBehaviour {
	private bool loaded = true;
	private bool sticked_bard = false;
	private bool sticked_card = false;
	private bool gg_f = false;
	private GameObject PlayerW;
	public Vector3 diffPL;
	public float initRot;
	//mouse
	private Vector3 monsePosition;
	//mouse (world
	private Vector3 monsePositionWorld;
	private Vector2 forceDir;
	public float forcePower;
	public float forceScale;

	public Rigidbody2D rb;
	public Vector3 center = new Vector3(0f, -0.2f, 0f);

	public AudioSource[] se_list;

	private GameObject bard;
	private Vector3 bardStickDist;
	private GameObject card;
	private Vector3 cardStickDist;

	// Use this for initialization
	void Start () {
		loaded = true;
		rb = GetComponent<Rigidbody2D>();
		rb.centerOfMass = center;

		//矢取得
		PlayerW = GameObject.FindGameObjectWithTag("PlayerW");
		if(PlayerW == null)
			PlayerW = GameObject.Find("PlayerW");
		if(PlayerW == null)
			SceneManager.LoadScene("Title"); 

		//se
		se_list = GetComponents<AudioSource>();

		//camera
		//Rigidbody camrb = GameObject.Find("Camera").GetComponent<Rigidbody>();
		//camrb.AddForce(new Vector3(0,10,-100));
		//camrb.AddTorque(new Vector3(0,0,0));
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.DrawLine (transform.position , transform.position + transform.rotation * center);

		if(loaded){
			transform.position = PlayerW.transform.position + Quaternion.Euler(0, 0, PlayerW.transform.eulerAngles.z) * diffPL;
			transform.eulerAngles = PlayerW.transform.eulerAngles + new Vector3(0,0,initRot);

			if (Input.GetMouseButtonDown(0)) {
				//Debug.Log("shoot!!");
				loaded = !loaded;

				// Vector3でマウス位置座標を取得する
				monsePosition = Input.mousePosition;
				// マウス位置座標をスクリーン座標からワールド座標に変換する
				monsePositionWorld = Camera.main.ScreenToWorldPoint(monsePosition);
				monsePositionWorld.z = 0;
				//発射力計算
				forceDir = new Vector2(monsePositionWorld.x - transform.position.x, monsePositionWorld.y - transform.position.y);
				forceDir = forceDir.normalized*(float)(forceDir.magnitude*forceScale + forcePower);
				forceDir.x *= 0.75f;

				//発射
				rb.velocity = Vector3.zero;
				rb.angularVelocity = 0;
				rb.AddForce(forceDir, ForceMode2D.Impulse);
				//SE
				se_list[0].PlayOneShot(se_list[0].clip);
			}
		}
		if(sticked_bard && bard != null){
			bard.transform.position = transform.position + bardStickDist;
		}
		if(sticked_card && card != null){
			card.transform.position = transform.position + cardStickDist;
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		//Debug.Log(other.gameObject.name);
		//clear
		if(other.gameObject.name == "Floor" && !gg_f && sticked_bard && sticked_card){
			gg_f = true;
			se_list[3].PlayOneShot(se_list[3].clip);
			GameObject.Find("Text3").transform.Translate(new Vector3(0, 22.5f, 0));
		}
	}
	void OnTriggerEnter2D(Collider2D other) {  
		//Debug.Log(other.name);
		//bard
		if(other.name == "Bard" && !sticked_bard){
			Debug.Log("bard hit!");
			sticked_bard = true;
			bard = other.gameObject;
			bardStickDist = bard.transform.position - transform.position;
			//SE
			se_list[1].PlayOneShot(se_list[1].clip);
		}
		//card
		if(other.name == "Card" && !sticked_card){
			//Debug.Log("card hit!");
			sticked_card = true;
			card = other.gameObject;
			cardStickDist = card.transform.position - transform.position;
			//SE
			se_list[2].PlayOneShot(se_list[2].clip);
		}
	}  

	/*
	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawSphere (transform.position + transform.rotation * center, 0.1f);
	}
	*/

}
