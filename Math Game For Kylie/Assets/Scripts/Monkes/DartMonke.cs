using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartMonke : MonoBehaviour
{
	/*private Transform target;
	private BloonCode enemyScript;

	[Header("General")]

	public float range = 15f;
	public string targeting = "close";

	[Header("Use Bullets (default)")]
	public GameObject bulletPrefab;
	public float fireRate = 0.75f;
	private float fireCountdown = 0f;
	public float speed = 0f;

	public Transform firePoint;
	private GameManager gameManager;
	private Transform instantiatePos;
	private GameObject upgradeMenu;
	private Upgrades upgradeScript;
	private int upgradePath = 1;
	private int upgradeLevel = 0;
	public TowerUpgradeScriptableObject dartObject;

	// Use this for initialization
	void Start()
	{
		instantiatePos = GameObject.FindGameObjectWithTag("ProjectileInstantiatePos").GetComponent<Transform>();
		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		upgradeMenu = GameObject.FindGameObjectWithTag("UpgradeMenu");
		upgradeScript = upgradeMenu.GetComponent<Upgrades>();
		InvokeRepeating("UpdateTarget", 0.1f, fireRate);
	}

	void UpdateTarget()
	{
		if(gameManager.enemies.Count != 0)
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
		if(target != null)
        {
			transform.up = target.position - transform.position;
		}
	}
	void Shoot()
	{
		GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation, instantiatePos);
		Rigidbody2D bullet = bulletGO.GetComponent<Rigidbody2D>();
		bullet.AddForce(firePoint.up * speed, ForceMode2D.Impulse);
	}
	public void Upgrade()
    {
		upgradeLevel++;
    }
	public void OpenUpgradeMenu()
    {
		//upgradeScript.dart = this;
		//upgradeScript.OpenMenu("dart", upgradeLevel, upgradePath, "First", dartObject);
    }*/
}
