using UnityEngine;
using System.Collections;

public class EvasiveManeuver : MonoBehaviour {
	public float speed;
	public float xMin;
	public float xMax;
	public float zMin;
	public float zMax;
	public float tilt; //倾斜
	public float dodge; //躲闪
	public float smoothing;
	public Vector2 startWait; //开始机动的等待时间
	public Vector2 maneuverTime; //机动时间
	public Vector2 maneuverWait; //机动间隔

	private float currentSpeed; //当前敌机的速度
	private float targetManeuver; //机动目标值
	private float n;
	private float m=0;
	private float xPos;
	private float zPos;
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody> ().velocity = transform.forward * speed;
		currentSpeed = GetComponent<Rigidbody> ().velocity.z;
		StartCoroutine (Evade ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Evade(){
		yield return new WaitForSeconds(Random.Range(startWait.x,startWait.y));

		while (true) {
			n=transform.position.x;
			if(n>-9){
				m=-1;
			}else{
				m=1;
			}
			targetManeuver=Random.Range(1,dodge)*m; //在中心轴一边时，往中心轴另一方向移动
			yield return new WaitForSeconds(Random.Range(maneuverTime.x,maneuverTime.y)); //机动时间
			targetManeuver=0;
			yield return  new WaitForSeconds(Random.Range(maneuverWait.x,maneuverWait.y));
		}

	}

	void FixedUpdate(){
		float newManeuver = Mathf.MoveTowards (GetComponent<Rigidbody> ().velocity.x, targetManeuver, smoothing * Time.deltaTime); //取得敌机移动的移动位置
		GetComponent<Rigidbody> ().velocity = new Vector3 (newManeuver, 0.08f, currentSpeed);

		xPos=Mathf.Clamp(GetComponent<Rigidbody> ().position.x,xMin,xMax);
		zPos = Mathf.Clamp (GetComponent<Rigidbody> ().position.z, zMin, zMax);
		GetComponent<Rigidbody> ().position = new Vector3 (xPos, 0.08f, zPos);
		GetComponent<Rigidbody> ().rotation = Quaternion.Euler (0, 180, GetComponent<Rigidbody> ().velocity.x * -tilt);
	}
}
