using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;
	public BoardManager boardScript;
	public float turnDelay = .1f;
	public int playerFoodPoints = 100;
	[HideInInspector] public bool isPlayersTurn = true;

	private int level = 3;
	private List<Enemy> enemies;
	private bool enemiesMoving;

	// Use this for initialization
	void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
		enemies = new List<Enemy>();
		boardScript = GetComponent<BoardManager>();

		InitGame();
	}

	public void GameOver() {
		enabled = false;
	}

	void InitGame() {
		enemies.Clear();
		boardScript.SetupScene(level);
	}

	void Update() {
		if (isPlayersTurn || enemiesMoving) {
			return;
		}
		StartCoroutine(MoveEnemies());
	}

	public void AddEnemeyToList(Enemy script) {
		enemies.Add(script);
	}
	
	IEnumerator MoveEnemies() {
		enemiesMoving = true;
		if (enemies.Count == 0) {
			yield return new WaitForSeconds(turnDelay);
		}
		foreach (Enemy e in enemies) {
			e.MoveEnemy();
			yield return new WaitForSeconds(turnDelay);
		}
		isPlayersTurn = true;
		enemiesMoving = false;
	}
}
