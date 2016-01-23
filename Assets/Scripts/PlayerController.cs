using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed;
	public float xMin;
	public float xMax;
	public float zMin;
	public float zMax;

	private float xPos;
	private float zPos;

	public GameObject shot; //子弹的预设体
	public Transform shotSpawn; // 子弹出生位置
	public float fireRate; //子弹的发射率

	private float nextFire;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Jump") && Time.time > nextFire) {
			nextFire = Time.time +fireRate ;
			Instantiate(shot,shotSpawn.position,shotSpawn.rotation); //生成一子弹
			GetComponent<AudioSource>().Play();
		}
	}

	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		GetComponent<Rigidbody> ().velocity = movement * speed;
		xPos=Mathf.Clamp(GetComponent<Rigidbody> ().position.x,xMin,xMax);
		zPos = Mathf.Clamp (GetComponent<Rigidbody> ().position.z, zMin, zMax);
		GetComponent<Rigidbody> ().position = new Vector3 (xPos, 0.08f, zPos);
	}
}
