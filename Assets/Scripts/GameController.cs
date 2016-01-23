using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public GameObject[] hazards; // 行星数组
	public Vector3 spawnValues; //随机生成小行星位置
	public int hazardCount;  //每一波小行星数目
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public Text scoreText;
	public Text restartText;
	public Text gameOverText;

	private int score;
	private bool gameOver;
	private bool restart;
	// Use this for initialization
	void Start () {
		restartText.text = " ";
		gameOverText.text = " ";
		gameOver = false;
		restart = false;
		score = 0;
		StartCoroutine (SpawnWave ());
	}
	
	// Update is called once per frame
	void Update () {
		if (restart) {
			if(Input.GetKeyDown(KeyCode.R)){
				Application.LoadLevel(Application.loadedLevel);//加载关卡
			}
		}
	}

	IEnumerator SpawnWave(){
		yield return new WaitForSeconds (startWait);
		while (true) {
			for(int i=0;i<hazardCount;i++){
				GameObject hazard = hazards [Random.Range (0, 3)];
				Vector3 spawnPosition = new Vector3 (Random.Range (spawnValues.x-5, spawnValues.x+5), spawnValues.y, spawnValues.z); //因为没对齐坐标系，所以改成数字
				Instantiate (hazard, spawnPosition, this.transform.rotation);
				if(i%2==0){
					hazard=hazards[3];
					spawnPosition=new Vector3(Random.Range (spawnValues.x-5, spawnValues.x+5), spawnValues.y, spawnValues.z);
					Instantiate(hazard,spawnPosition,Quaternion.identity);
				}
				yield return new WaitForSeconds(spawnWait);
			}

			yield return new WaitForSeconds(waveWait);
			if(gameOver){
				restartText.text="Press 'R' For Restart";
				restart = true;
				break;
			}
		}

	}

	//增加分数
	public void AddScore(int newScoreValues){
		score += newScoreValues;
		UpdateScore ();
	}

	//更新UI界面的分数显示
	void UpdateScore(){
		scoreText.text = "Score:" + score;
	}

	public void GameOver(){
		gameOverText.text = "Game Over";
		gameOver = true;
	}
}
