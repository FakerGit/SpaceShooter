using UnityEngine;
using System.Collections;

public class DestroySelf : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy(gameObject,2);//两秒后删除粒子系统
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
