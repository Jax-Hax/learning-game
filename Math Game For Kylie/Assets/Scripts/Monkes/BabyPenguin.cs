using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BabyPenguin : MonoBehaviour
{
	private Transform target;
	private BloonCode enemyScript;
	public Sprite pengNorm;
	public Sprite pengStrong;
	public Sprite pengPerma;
	public Range rangeObj;
	[Header("General")]

	public float range = 1.35f;
	public string targeting = "first";

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
	private Collider2D[] hitColliders;
	public LayerMask mask;
	private WaitForSeconds timeToWait1 = new WaitForSeconds(0.2f);
	public bool canSeeCamo;
	bool canPopWhite;
	bool canPopLead;
	bool canPopBlack;
	bool canPopOrange;
	bool canPopPurple;
	bool extraDamage;
	private BloonCode bloonCode;
	private Image rangeImg;
	bool isFastChecking = false;
	// Use this for initialization
	void Start()
	{
		rangeImg = rangeObject.GetComponent<Image>();
		canSeeCamo = false;
		canPopWhite = false;
		canPopLead = false;
		canPopBlack = false;
		canPopOrange = false;
		canPopPurple = false;
		extraDamage = false;
		rangeImg.enabled = false;
		gameManager = GameManager.SharedInstance;
		StartCoroutine(UpdatePlantTarget());
	}
	IEnumerator UpdatePlantTarget()
	{
		while (!gameManager.won)
		{
			if (gameManager.enemies.Count != 0)
			{
				if (isFastChecking)
				{
					if (rangeObj.activeList.Count >= 1)
					{
						target = rangeObj.activeList[0].transform;
						enemyScript = rangeObj.activeList[0].GetComponent<BloonCode>();
					}
				}
				GameObject enemy = GetEnemy();
				if (enemy != null)
				{
					target = enemy.transform;
					enemyScript = enemy.GetComponent<BloonCode>();
				}
				else
				{
					target = null;
					enemyScript = null;
				}
			}
			yield return timeToWait1;
		}
	}
	GameObject GetEnemy()
	{
		return rangeObj.activeList[0];
	}
	public void HideRange()
	{
		rangeImg.enabled = false;
	}
	public void ShowRange()
	{
		rangeImg.enabled = true;
	}
	// Update is called once per frame
	void FixedUpdate()
	{
		if (fireCountdown <= 0f && target != null)
		{
			LockOnTarget();
			Shoot();
			fireCountdown = 0.6525f / fireRate;
			isFastChecking = false;
		}
		else if (fireCountdown <= 0f)
		{
			timeToWait1 = new WaitForSeconds(0.05f);
			isFastChecking = true;
		}
		fireCountdown -= Time.deltaTime;
	}

	void LockOnTarget()
	{
		timeToWait1 = new WaitForSeconds(0.3f);
		if (target != null)
		{
			transform.up = target.position - transform.position;
		}
	}
	private void OnMouseDown()
	{
		rangeImg.enabled = true;
	}
	private void OnMouseUp()
	{
		if (gameManager.upgrades.activeSelf == false)
		{
			rangeImg.enabled = false;
		}
	}
	void Shoot()
	{
		if (target != null)
		{
			bloonCode = enemyScript;
			if (bloonCode.gameObject.activeSelf == false)
			{
				target = null;
				return;
			}
			bloonCode.RemoveHealth(damage, canSeeCamo, canPopBlack, canPopLead, canPopOrange, canPopPurple, canPopWhite);
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
		canSeeCamo = statBruh.canSeeCamo;
		GetComponent<Image>().sprite = pengNorm;
	}
	public void SetStatsStrong(Penguin statBruh)
	{
		Invoke("KillMe", 20);
		range = statBruh.range;
		damage = statBruh.damage;
		fireRate = statBruh.fireRate;
		canSeeCamo = statBruh.canSeeCamo;
		GetComponent<Image>().sprite = pengStrong;
	}
	public void SetStatsPerma(Penguin statBruh)
	{
		range = statBruh.range;
		damage = statBruh.damage;
		fireRate = statBruh.fireRate;
		canSeeCamo = statBruh.canSeeCamo;
		GetComponent<Image>().sprite = pengPerma;
	}
	public void IncreaseStats()
	{
		if (range <= 7)
		{
			range += 0.5f;
		}
		if (damage <= 10)
		{
			damage += 1;
		}
		if (fireRate <= 4)
		{
			fireRate += 0.1f;
		}
		canSeeCamo = true;
		canPopWhite = true;
		canPopLead = true;
		canPopBlack = true;
		canPopOrange = false;
		canPopPurple = true;
	}
	public void KillMe()
	{
		Destroy(gameObject);
	}
}
