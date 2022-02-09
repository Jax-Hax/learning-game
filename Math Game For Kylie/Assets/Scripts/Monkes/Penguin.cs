using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penguin : MonoBehaviour
{
	private Transform target;
	private BloonCode enemyScript;

	[Header("General")]

	public float range = 15f;
	public string targeting = "close";

	public float fireRate = 0.75f;
	private float fireCountdown = 0f;

	private GameManager gameManager;
	private GameObject upgradeMenu;
	private Upgrades upgradeScript;
	public int upgradePath = 4;
	public int upgradeLevel = 0;
	public TowerUpgradeScriptableObject penguinObject;
	public Animator anim;

	// Use this for initialization
	void Start()
	{
		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		upgradeMenu = GameObject.FindGameObjectWithTag("UpgradeMenu");
		upgradeScript = upgradeMenu.GetComponent<Upgrades>();
		InvokeRepeating("UpdateTarget", 0.1f, fireRate);
	}

	void UpdateTarget()
	{
		if (gameManager.enemies.Count != 0)
		{
			GameObject[] enemies = gameManager.enemies.ToArray();
			if (targeting == "close")
			{
				float shortestDistance = Mathf.Infinity;
				GameObject nearestEnemy = null;
				foreach (GameObject enemy in enemies)
				{
					float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
					if (distanceToEnemy < shortestDistance)
					{
						shortestDistance = distanceToEnemy;
						nearestEnemy = enemy;
					}
				}

				if (nearestEnemy != null && shortestDistance <= range)
				{
					target = nearestEnemy.transform;
					enemyScript = nearestEnemy.GetComponent<BloonCode>();
				}
				else
				{
					target = null;
				}
			}
		}
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (target == null)
		{
			return;
		}
		if (fireCountdown <= 0f)
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
	void Shoot()
	{
		if(target != null)
        {
			target.GetComponent<BloonCode>().RemoveHealth(1);
			anim.Play("Shoot");
		}
	}
	public void Upgrade()
	{
		upgradeLevel++;
	}
	public void OpenUpgradeMenu()
	{
		upgradeScript.penguin = this;
		upgradeScript.OpenMenu("penguin", upgradeLevel, upgradePath, "First", penguinObject);
	}
}
