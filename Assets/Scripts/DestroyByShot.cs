using UnityEngine;
using System.Collections;

public class DestroyByShot : MonoBehaviour {
	public GameObject explosion;
	public GameObject playerExplosion;
	public AudioSource[] arrAudio;
	public GameController gameController;
	public int scoreValue;
	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController=gameControllerObject.GetComponent<GameController>();
		}

		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' Scripts");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.tag != "Boundary"&&other.tag!="Enemy") {
			if(explosion != null){
				Instantiate(explosion,transform.position,transform.rotation);   //行星爆炸效果
			}
			if(other.tag=="Player"){
				Instantiate(playerExplosion,other.transform.position,other.transform.rotation);  //玩家爆炸效果
				gameController.GameOver();
			}

			gameController.AddScore(scoreValue);
			Destroy (other.gameObject);  //摧毁与行星碰撞的物体
			Destroy (this.gameObject);   //销毁行星
		}

	}
}
