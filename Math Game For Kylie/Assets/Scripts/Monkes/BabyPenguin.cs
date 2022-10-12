using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyPenguin : MonoBehaviour
{
	private Transform target;
	private BloonCode enemyScript;

	[Header("General")]

	public float range = 1.35f;
	public string targeting = "close";

	public float fireRate = 0.75f;
	private float fireCountdown = 0f;

	private GameManager gameManager;
	public int popCount;
	public int upgradePath = 4;
	public int upgradeLevel = 0;
	public TowerUpgradeScriptableObject penguinObject;
	public Animator anim;
	public GameObject rangeObject;
	public int damage = 1;
	public LayerMask mask;
	private WaitForSeconds timeToWait1 = new WaitForSeconds(0.3f);
	bool canSeeCamo;
	bool canPopWhite;
	bool canPopLead;
	bool canPopBlack;
	bool canPopOrange;
	bool canPopPurple;
	bool extraDamage;
	private BloonCode bloonCode;
	// Use this for initialization
	void Start()
	{
		canSeeCamo = false;
		canPopWhite = false;
		canPopLead = false;
		canPopBlack = false;
		canPopOrange = false;
		canPopPurple = false;
		extraDamage = false;
		rangeObject.SetActive(false);
		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		StartCoroutine(UpdatePlantTarget());
	}
	IEnumerator UpdatePlantTarget()
	{
		while (!gameManager.won)
		{
			if (gameManager.enemies.Count != 0)
			{
				GameObject enemy = gameManager.GetEnemy(range, targeting, gameObject.transform);
				if (enemy != null)
				{
					target = enemy.transform;
					enemyScript = enemy.GetComponent<BloonCode>();
				}
				else
				{
					target = null;
				}
			}
			yield return timeToWait1;
		}
	}
	public void HideRange()
	{
		rangeObject.SetActive(false);
	}
	public void ShowRange()
	{
		rangeObject.SetActive(true);
	}
	// Update is called once per frame
	void FixedUpdate()
	{
		if (target == null)
		{
			return;
		}
		if (fireCountdown <= 0f && target != null)
		{
			LockOnTarget();
			Shoot();
			fireCountdown = fireRate;
		}

		fireCountdown -= Time.deltaTime;

	}

	void LockOnTarget()
	{
		if (target != null)
		{
			transform.up = target.position - transform.position;
		}
	}
	private void OnMouseDown()
	{
		rangeObject.SetActive(true);
	}
	private void OnMouseUp()
	{
		if (gameManager.upgrades.activeSelf == false)
		{
			rangeObject.SetActive(false);
		}
	}
	void Shoot()
	{
		if (target != null)
		{
			bloonCode = enemyScript;
			bloonCode.RemoveHealth(damage, canSeeCamo, canPopBlack, canPopLead, canPopOrange, canPopPurple, canPopWhite);
			if (extraDamage)
			{
				if (bloonCode.bloonType <= 11)
				{
					bloonCode.SlowDown(1.2f, 4);
					bloonCode.extraDamageTaken = Mathf.RoundToInt(damage * 1.2f);
				}
			}
			popCount += damage;
			anim.Play("Shoot");
		}
	}
	public void SetStats(Penguin statBruh)
	{
		Invoke("KillMe", 20);
		range = statBruh.range;
		damage = statBruh.damage;
		fireRate = statBruh.fireRate;
	}
	public void KillMe()
	{
		Destroy(gameObject);
	}
}
