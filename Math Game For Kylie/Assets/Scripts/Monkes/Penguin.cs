using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Penguin : MonoBehaviour
{
	private Transform target;
	private BloonCode enemyScript;
	public Range rangeObj;
	[Header("General")]

	public float range = 1.35f;
	public string targeting = "first";

	public float fireRate = 0.75f;
	private float fireCountdown = 0f;

	private GameManager gameManager;
	private GameObject upgradeMenu;
	private Upgrades upgradeScript;
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
	int buffAm;
	public GameObject babyPenguin;
	private GameObject tempObject;
	private Transform placeToSpawnBaby;
	private BabyPenguin permaPeng;
	private int stunBloons;
	private Image rangeImg;
	bool isFastChecking = false;
	// Use this for initialization
	void Start()
	{
		rangeImg = rangeObject.GetComponent<Image>();
		stunBloons = 0;
		canSeeCamo = false;
		canPopWhite = false;
		canPopLead = false;
		canPopBlack = false;
		canPopOrange = false;
		canPopPurple = false;
		extraDamage = false;
		rangeImg.enabled = false;
		gameManager = GameManager.SharedInstance;
		gameManager.penguins.Add(this);
		upgradeMenu = GameObject.FindGameObjectWithTag("UpgradeMenu");
		upgradeScript = upgradeMenu.GetComponent<Upgrades>();
		placeToSpawnBaby = GameObject.FindGameObjectWithTag("shootingtower").transform;
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
					if(rangeObj.activeList.Count >= 1)
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
		if (targeting.Equals("close"))
		{
			float shortestDistance = Mathf.Infinity;
			GameObject nearestEnemy = null;
			foreach (GameObject enemy in rangeObj.activeList)
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
				return nearestEnemy;
			}
			else
			{
				return null;
			}
		}
		else if (targeting.Equals("strong"))
		{
			int strongest = 0;
			GameObject bestEnemy = null;
			int enemyHealth = 0;
			foreach (GameObject enemy in rangeObj.activeList)
			{
				float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
				if (distanceToEnemy <= range)
				{
					enemyHealth = enemy.GetComponent<BloonCode>().health;
					if (enemyHealth >= strongest)
					{
						bestEnemy = enemy;
						strongest = enemyHealth;
					}
				}
			}
			if (bestEnemy != null)
			{
				return bestEnemy;
			}
			else
			{
				return null;
			}
		}
		else if (targeting.Equals("first"))
		{
			return rangeObj.activeList[0];
		}
		else if (targeting.Equals("last"))
		{
			return rangeObj.activeList[rangeObj.activeList.Count - 1];
		}
		return null;
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
		WaitForSeconds timeToWait1 = new WaitForSeconds(0.3f);
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
        if(gameManager.upgrades.activeSelf == false)
        {
			rangeImg.enabled = false;
        }
    }
    void Shoot()
	{
		if(target != null)
        {
			bloonCode = enemyScript;
			if(bloonCode.gameObject.activeSelf == false)
            {
				target = null;
				return;
            }
			if (extraDamage)
			{
				bloonCode.RemoveHealth(damage, canSeeCamo, canPopBlack, canPopLead, canPopOrange, canPopPurple, canPopWhite);
				if (bloonCode.bloonType <= 11)
				{
					bloonCode.SlowDown(1.2f, 4);
					bloonCode.extraDamageTaken = Mathf.RoundToInt(damage * 1.2f);
				}
			}
			else if (stunBloons == 1)
			{
				bloonCode.RemoveHealth(damage, canSeeCamo, canPopBlack, canPopLead, canPopOrange, canPopPurple, canPopWhite);
				if (bloonCode.bloonType <= 11)
				{
					bloonCode.Stop(1f);
				}
			}
			else if (stunBloons == 2)
			{
				bloonCode.RemoveHealth(damage, canSeeCamo, canPopBlack, canPopLead, canPopOrange, canPopPurple, canPopWhite);
				if (bloonCode.bloonType <= 11)
                {
					bloonCode.Stop(1.5f);
				}
			}
			else if(stunBloons == 3)
            {
				bloonCode.RemoveHealth(damage, canSeeCamo, canPopBlack, canPopLead, canPopOrange, canPopPurple, canPopWhite);
				if (bloonCode.bloonType <= 14)
				{
					bloonCode.Stop(1.5f);
				}
			}
			else if (stunBloons == 3)
			{
                if (bloonCode.bloonType <= 11)
                {
					popCount += bloonCode.health - damage;
					bloonCode.RemoveHealth(bloonCode.health, canSeeCamo, canPopBlack, canPopLead, canPopOrange, canPopPurple, canPopWhite);
				}
				else if (bloonCode.bloonType <= 13)
				{
					bloonCode.RemoveHealth(75, canSeeCamo, canPopBlack, canPopLead, canPopOrange, canPopPurple, canPopWhite);
				}
				if (bloonCode.bloonType <= 16)
				{
					bloonCode.RemoveHealth(50, canSeeCamo, canPopBlack, canPopLead, canPopOrange, canPopPurple, canPopWhite);
					bloonCode.Stop(1.5f);
				}
			}
            else
            {
				bloonCode.RemoveHealth(damage, canSeeCamo, canPopBlack, canPopLead, canPopOrange, canPopPurple, canPopWhite);
			}
			popCount += damage;
			anim.Play("Shoot");
		}
	}
	public void Upgrade()
	{
		upgradeLevel++;
        switch (upgradePath)
        {
			case 1:
                switch (upgradeLevel)
                {
					case 1:
						rangeObject.transform.localScale = new Vector3(1.6f, 1.6f, 1f);
						range = 1.78f;
						break;
					case 2:
						rangeObject.transform.localScale = new Vector3(2.16f, 2.16f, 1f);
						range = 2.45f;
						break;
					case 3:
						InvokeRepeating("UpdateSurroundings", 0, 10);
						break;
					case 4:
						ReDoHaveChild1();
						break;
					case 5:
						ReDoHaveChild2();
						break;
					case 6:
						ReDoHaveChild3();
						break;
				}
                break;
			case 2:
				switch (upgradeLevel)
				{
					case 1:
						fireRate = 1.25f;
						break;
					case 2:
						canSeeCamo = true;
						break;
					case 3:
						fireRate = 2.25f;
						break;
					case 4:
						extraDamage = true;
						break;
					case 5:
						ReDoUltraPeck();
						break;
					case 6:
						fireRate = 6f;
						break;
				}
				break;
			case 3:
				switch (upgradeLevel)
				{
					case 1:
						damage += 1;
						break;
					case 2:
						damage += 3;
						break;
					case 3:
						stunBloons = 1;
						canPopLead = true;
						break;
					case 4:
						stunBloons = 2;
						damage += 7;
						break;
					case 5:
						stunBloons = 3;
						fireRate -= 0.25f;
						break;
					case 6:
						stunBloons = 4;
						break;
				}
				break;
        }
    }
	public void OpenUpgradeMenu()
	{
		upgradeScript.CloseUpgrades();
		upgradeScript.penguin = this;
		rangeImg.enabled = true;
		upgradeScript.OpenMenu("penguin", upgradeLevel, upgradePath, "First", penguinObject, popCount);
	}
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
    }
    void UpdateSurroundings()
    {
		range -= (buffAm / 10);
		fireRate -= (buffAm / 10);
		damage -= Mathf.RoundToInt(buffAm / 3);
		buffAm = -1;
		hitColliders = Physics2D.OverlapCircleAll(transform.position, range, mask);
        foreach (Collider2D col in hitColliders)
        {
            if (col.CompareTag("penguin"))
            {
				buffAm += 1;
            }
        }
		range += (buffAm / 10);
		fireRate += (buffAm / 10);
		damage += Mathf.RoundToInt(buffAm / 3);
    }
    private void OnDrawGizmos()
    {
		Gizmos.DrawWireSphere(transform.position, 2.25f);
    }
    public void HaveChild1()
    {
		Invoke("ReDoHaveChild1", 45);
		hitColliders = Physics2D.OverlapCircleAll(transform.position, range, mask);
		foreach (Collider2D col in hitColliders)
		{
			if (col.CompareTag("penguin") && gameObject != col.gameObject)
			{
				tempObject = Instantiate(babyPenguin, new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f)), Quaternion.identity, placeToSpawnBaby);
				tempObject.GetComponent<BabyPenguin>().SetStats(this);
				return;
			}
		}
	}
	public void HaveChild2()
	{
		Invoke("ReDoHaveChild2", 45);
		hitColliders = Physics2D.OverlapCircleAll(transform.position, range, mask);
		foreach (Collider2D col in hitColliders)
		{
			if (col.CompareTag("penguin") && gameObject != col.gameObject)
			{
				Penguin bestPeng = this;
				tempObject = Instantiate(babyPenguin, new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f)), Quaternion.identity, placeToSpawnBaby);
				foreach (Penguin peng in gameManager.penguins)
				{
					if (peng.upgradeLevel >= bestPeng.upgradeLevel && (peng.upgradePath == 2 || peng.upgradePath == 3))
					{
						bestPeng = this;
					}
				}
				tempObject.GetComponent<BabyPenguin>().SetStatsStrong(bestPeng);
				return;
			}
		}
	}
	public void HaveChild3()
	{
		Invoke("ReDoHaveChild3", 30);
		if(permaPeng == null)
        {
			hitColliders = Physics2D.OverlapCircleAll(transform.position, range, mask);
			foreach (Collider2D col in hitColliders)
			{
				if (col.CompareTag("penguin") && gameObject != col.gameObject)
				{
					Penguin bestPeng = this;
					tempObject = Instantiate(babyPenguin, new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f)), Quaternion.identity, placeToSpawnBaby);
					foreach (Penguin peng in gameManager.penguins)
					{
						if (peng.upgradeLevel >= bestPeng.upgradeLevel && (peng.upgradePath == 2 || peng.upgradePath == 3))
						{
							bestPeng = this;
						}
					}
					tempObject.GetComponent<BabyPenguin>().SetStatsPerma(bestPeng);
					return;
				}
			}
		}
        else
        {
			permaPeng.IncreaseStats();
        }
	}
	public void UltraPeck()
	{
		Invoke("ReDoUltraPeck", 30);
		fireRate = 6f;
		Invoke("StopUltraPeck", 10);
	}
	void StopUltraPeck()
    {
		fireRate = 2.25f;
    }
	public void ReDoHaveChild1()
    {
		gameManager.haveChild1.Add(this);
		gameManager.CheckAbility("haveChild1");
    }
	public void ReDoHaveChild2()
	{
		gameManager.haveChild2.Add(this);
		gameManager.CheckAbility("haveChild2");
	}
	public void ReDoHaveChild3()
	{
		gameManager.haveChild3.Add(this);
		gameManager.CheckAbility("haveChild3");
	}
	public void ReDoUltraPeck()
	{
		gameManager.ultraPeck.Add(this);
		gameManager.CheckAbility("ultraPeck");
	}
}
