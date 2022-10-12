using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penguin : MonoBehaviour
{
	private Transform target;
	private BloonCode enemyScript;

	[Header("General")]

	public float range = 1.35f;
	public string targeting = "close";

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
	private WaitForSeconds timeToWait1 = new WaitForSeconds(0.3f);
	bool canSeeCamo;
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
        if(gameManager.upgrades.activeSelf == false)
        {
			rangeObject.SetActive(false);
        }
    }
    void Shoot()
	{
		if(target != null)
        {
			bloonCode = enemyScript;
			bloonCode.RemoveHealth(damage, canSeeCamo, canPopBlack, canPopLead, canPopOrange, canPopPurple, canPopWhite);
            if (extraDamage)
            {
				if(bloonCode.bloonType <= 11)
                {
					bloonCode.SlowDown(1.2f, 4);
					bloonCode.extraDamageTaken = Mathf.RoundToInt(damage * 1.2f);
				}
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
						break;
					case 6:
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
						
						break;
					case 6:
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
						break;
					case 4:
						break;
					case 5:
						break;
					case 6:
						break;
				}
				break;
        }
    }
	public void OpenUpgradeMenu()
	{
		upgradeScript.CloseUpgrades();
		upgradeScript.penguin = this;
		rangeObject.SetActive(true);
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

	}
	public void HaveChild3()
	{

	}
	public void UltraPeck()
	{

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
