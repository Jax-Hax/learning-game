using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BloonCode : MonoBehaviour
{
    [HideInInspector]
    public int health;
    public int bloonType;
    private GameManager gameManager;
    Vector3[] waypoints;
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public int wayPointIndex = 0;
    [HideInInspector]
    public int damageBloonDoesToLives;
    [HideInInspector]
    public Transform whereToSpawn;
    private Image image;
    public Sprite[] enemySprites;
    public float[] enemySpeeds;
    public int[] enemyHealths;
    public float healthMult;
    public float speedMult;
    private bool isPinkSplit;
    private bool isBWSplit;
    private bool isRedSplit;
    private float tempSpeed;
    private bool isBlueSplit;
    private bool isGreenSplit;
    private bool isYellowSplit;
    private bool isMushroomSplit;
    public int roundNum;
    public bool isCamo;
    public bool isPurple;
    public bool isLead;
    public bool isOrange;
    public bool isWhite;
    public bool isBlack;
    public Image spriteThing;
    bool isBeingSlowed;
    public int extraDamageTaken;
    private Color color = new Color(1f, 1f, 1f, 0.6f);
    private Color color1 = new Color(1f, 1f, 1f, 1f);
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    private void Start()
    {
        isBeingSlowed = false;
        isPinkSplit = false;
        isBWSplit = false;
        isRedSplit = false;
        isBlueSplit = false;
        isGreenSplit = false;
        isYellowSplit = false;
        isMushroomSplit = false;
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        waypoints = gameManager.mapPositions;
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[wayPointIndex], speed * Time.deltaTime);
        if(transform.position == waypoints[wayPointIndex])
        {
            wayPointIndex++;
        }
        if(wayPointIndex == waypoints.Length)
        {
            gameManager.UpdateHealth(-damageBloonDoesToLives);
            gameManager.enemies.Remove(gameObject);
            gameObject.SetActive(false);
        }
    }
    public void RemoveHealth(int healthRemoved, bool seeCamo, bool popPurple, bool popOrange, bool popLead, bool popWhite, bool popBlack)
    {
        if(isCamo && !seeCamo || isLead && !popLead || isWhite && !popWhite || isBlack && !popBlack || isOrange && !popOrange || isPurple && !popPurple)
        {
            return;
        }
        health -= healthRemoved + extraDamageTaken;
        if(roundNum < 70)
        {
            if (health <= 0)
            {
                gameManager.enemies.Remove(gameObject);
                gameObject.SetActive(false);
            }
            else if (health == 1)
            {
                image.sprite = enemySprites[0];
                speed = enemySpeeds[0];
                damageBloonDoesToLives = enemyHealths[0];
            }
            else if (health == 2)
            {
                image.sprite = enemySprites[1];
                speed = enemySpeeds[1];
                damageBloonDoesToLives = enemyHealths[1];
            }
            else if (health == 3)
            {
                image.sprite = enemySprites[2];
                speed = enemySpeeds[2];
                damageBloonDoesToLives = enemyHealths[2];
            }
            else if (health == 4)
            {
                image.sprite = enemySprites[3];
                speed = enemySpeeds[3];
                damageBloonDoesToLives = enemyHealths[3];
            }
            else if (health == 5)
            {
                image.sprite = enemySprites[4];
                speed = enemySpeeds[4];
                damageBloonDoesToLives = enemyHealths[4];
            }
            else if (health <= 10 && health >= 6 && !isPinkSplit)
            {
                isPinkSplit = true;
                image.sprite = enemySprites[4];
                speed = enemySpeeds[4];
                damageBloonDoesToLives = enemyHealths[4];
                health = enemyHealths[4];
                GameObject bloon = ObjectPooler.SharedInstance.GetPooledObject(4);
                bloon.SetActive(true);
                bloon.GetComponent<BloonCode>().wayPointIndex = wayPointIndex;
                bloon.GetComponent<BloonCode>().SlowDown(2, 0.5f);
                bloon.transform.position = gameObject.transform.position;
            }
            else if (health >= 11 && health <= 22 && !isBWSplit)
            {
                isBWSplit = true;
                image.sprite = enemySprites[6];
                speed = enemySpeeds[6];
                damageBloonDoesToLives = enemyHealths[6];
                health = enemyHealths[6];
                GameObject bloon = ObjectPooler.SharedInstance.GetPooledObject(5);
                bloon.SetActive(true);
                bloon.GetComponent<BloonCode>().wayPointIndex = wayPointIndex;
                bloon.GetComponent<BloonCode>().SlowDown(2, 0.5f);
                bloon.transform.position = gameObject.transform.position;
            }
            else if (health >= 23 && health <= 63)
            {
                if (image.sprite == enemySprites[10])
                {
                    health = enemyHealths[9];
                    GameObject bloon = ObjectPooler.SharedInstance.GetPooledObject(9);
                    bloon.SetActive(true);
                    bloon.GetComponent<BloonCode>().wayPointIndex = wayPointIndex;
                    bloon.GetComponent<BloonCode>().SlowDown(2, 0.5f);
                    bloon.transform.position = gameObject.transform.position;
                }
                image.sprite = enemySprites[9];
                speed = enemySpeeds[9];
                damageBloonDoesToLives = enemyHealths[9];
            }
        }
        else if (health >= 73 && health <= 218 && !isRedSplit)
        {
            isRedSplit = true;
            image.sprite = enemySprites[10];
            speed = enemySpeeds[10];
            damageBloonDoesToLives = enemyHealths[10];
            health = enemyHealths[10];
            GameObject bloon = ObjectPooler.SharedInstance.GetPooledObject(10);
            bloon.SetActive(true);
            bloon.GetComponent<BloonCode>().wayPointIndex = wayPointIndex;
            bloon.GetComponent<BloonCode>().SlowDown(2, 0.5f);
            bloon.transform.position = gameObject.transform.position;
            bloon = ObjectPooler.SharedInstance.GetPooledObject(10);
            bloon.SetActive(true);
            bloon.GetComponent<BloonCode>().wayPointIndex = wayPointIndex;
            bloon.GetComponent<BloonCode>().SlowDown(4, 0.5f);
            bloon.transform.position = gameObject.transform.position;
        }
        else if(health >= 219 && health <= 1276 && !isBlueSplit)
        {
            isBlueSplit = true;
            image.sprite = enemySprites[12];
            speed = enemySpeeds[12];
            damageBloonDoesToLives = enemyHealths[12];
            health = enemyHealths[12];
            GameObject bloon = ObjectPooler.SharedInstance.GetPooledObject(12);
            bloon.SetActive(true);
            bloon.GetComponent<BloonCode>().wayPointIndex = wayPointIndex;
            bloon.GetComponent<BloonCode>().SlowDown(2, 0.5f);
            bloon.transform.position = gameObject.transform.position;
            bloon = ObjectPooler.SharedInstance.GetPooledObject(12);
            bloon.SetActive(true);
            bloon.GetComponent<BloonCode>().wayPointIndex = wayPointIndex;
            bloon.GetComponent<BloonCode>().SlowDown(4, 0.5f);
            bloon.transform.position = gameObject.transform.position;
            bloon = ObjectPooler.SharedInstance.GetPooledObject(12);
            bloon.SetActive(true);
            bloon.GetComponent<BloonCode>().wayPointIndex = wayPointIndex;
            bloon.GetComponent<BloonCode>().SlowDown(0.5f, 0.5f);
            bloon.transform.position = gameObject.transform.position;
        }
        else if(health >= 1277 && health <= 10204 && !isGreenSplit && bloonType == 17)
        {
            isGreenSplit = true;
            image.sprite = enemySprites[13];
            speed = enemySpeeds[13];
            damageBloonDoesToLives = enemyHealths[13];
            health = enemyHealths[13];
            GameObject bloon = ObjectPooler.SharedInstance.GetPooledObject(13);
            bloon.SetActive(true);
            bloon.GetComponent<BloonCode>().wayPointIndex = wayPointIndex;
            bloon.GetComponent<BloonCode>().SlowDown(2, 0.5f);
            bloon.transform.position = gameObject.transform.position;
            bloon = ObjectPooler.SharedInstance.GetPooledObject(13);
            bloon.SetActive(true);
            bloon.GetComponent<BloonCode>().wayPointIndex = wayPointIndex;
            bloon.GetComponent<BloonCode>().SlowDown(4, 0.5f);
            bloon.transform.position = gameObject.transform.position;
            bloon = ObjectPooler.SharedInstance.GetPooledObject(13);
            bloon.SetActive(true);
            bloon.GetComponent<BloonCode>().wayPointIndex = wayPointIndex;
            bloon.GetComponent<BloonCode>().SlowDown(0.5f, 0.5f);
            bloon.transform.position = gameObject.transform.position;
        }
        else if(health >= 1277 && health <= 1700 && !isYellowSplit && bloonType == 16)
        {
            isYellowSplit = true;
            image.sprite = enemySprites[15];
            speed = enemySpeeds[15];
            damageBloonDoesToLives = enemyHealths[15];
            health = enemyHealths[15];
            GameObject bloon = ObjectPooler.SharedInstance.GetPooledObject(15);
            bloon.SetActive(true);
            bloon.GetComponent<BloonCode>().wayPointIndex = wayPointIndex;
            bloon.GetComponent<BloonCode>().SlowDown(2, 0.5f);
            bloon.transform.position = gameObject.transform.position;
            bloon = ObjectPooler.SharedInstance.GetPooledObject(15);
            bloon.SetActive(true);
            bloon.GetComponent<BloonCode>().wayPointIndex = wayPointIndex;
            bloon.GetComponent<BloonCode>().SlowDown(4, 0.5f);
            bloon.transform.position = gameObject.transform.position;
        }
        else if(health <= 40500 && health >= 10205 && !isMushroomSplit)
        {
            isMushroomSplit = true;
            image.sprite = enemySprites[14];
            speed = enemySpeeds[14];
            damageBloonDoesToLives = enemyHealths[14];
            health = enemyHealths[14];
            GameObject bloon = ObjectPooler.SharedInstance.GetPooledObject(14);
            bloon.SetActive(true);
            bloon.GetComponent<BloonCode>().wayPointIndex = wayPointIndex;
            bloon.GetComponent<BloonCode>().SlowDown(2, 0.5f);
            bloon.transform.position = gameObject.transform.position;
            bloon = ObjectPooler.SharedInstance.GetPooledObject(14);
            bloon.SetActive(true);
            bloon.GetComponent<BloonCode>().wayPointIndex = wayPointIndex;
            bloon.GetComponent<BloonCode>().SlowDown(4, 0.5f);
            bloon.transform.position = gameObject.transform.position;
            bloon = ObjectPooler.SharedInstance.GetPooledObject(16);
            bloon.SetActive(true);
            bloon.GetComponent<BloonCode>().wayPointIndex = wayPointIndex;
            bloon.GetComponent<BloonCode>().SlowDown(0.5f, 0.5f);
            bloon.transform.position = gameObject.transform.position;
        }
        damageBloonDoesToLives = Mathf.RoundToInt(damageBloonDoesToLives * healthMult);
        speed *= speedMult;
    }
    private void OnEnable()
    {
        isBeingSlowed = false;
        extraDamageTaken = 0;
        isBWSplit = false;
        isPinkSplit = false;
        isYellowSplit = false;
        isRedSplit = false;
        isBlueSplit = false;
        isGreenSplit = false;
        isMushroomSplit = false;
        isLead = false;
        isWhite = false;
        isBlack = false;
        isPurple = false;
        isOrange = false;
        if (bloonType == 11)
        {
            isLead = true;
        }
        else if (bloonType == 11)
        {
            isWhite = true;
        }
        else if (bloonType == 11)
        {
            isBlack = true;
        }
        else if (bloonType == 11)
        {
            isOrange = true;
        }
        else if (bloonType == 11)
        {
            isPurple = true;
        }
        health = enemyHealths[bloonType];
        damageBloonDoesToLives = enemyHealths[bloonType];
        image.sprite = enemySprites[bloonType];
        speed = enemySpeeds[bloonType];
        Invoke("AddModifiers", 0.1f);
    }
    void AddModifiers()
    {
        health = Mathf.RoundToInt(health * healthMult);
        damageBloonDoesToLives = Mathf.RoundToInt(damageBloonDoesToLives * healthMult);
        speed *= speedMult;
        if (isCamo)
        {
            spriteThing.color = color;
        }
        else
        {
            spriteThing.color = color1;
        }
    }
    public void SlowDown(float curSpeed, float timeTillSpeedUp)
    {
        if (!isBeingSlowed)
        {
            isBeingSlowed = true;
            tempSpeed = curSpeed;
            speed /= curSpeed;
            Invoke("SpeedUp", timeTillSpeedUp);
        }
    }
    private void SpeedUp()
    {
        speed *= tempSpeed;
        isBeingSlowed = false;
    }
}
