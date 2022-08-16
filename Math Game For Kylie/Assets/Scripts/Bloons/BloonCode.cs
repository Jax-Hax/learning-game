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
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    private void Start()
    {
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
    public int RemoveHealth(int healthRemoved)
    {
        health -= healthRemoved;
        if (health <= 0)
        {
            gameManager.enemies.Remove(gameObject);
            gameObject.SetActive(false);
        }
        else if(health == 1)
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
        else if (health <= 10 && health >= 6)
        {
            image.sprite = enemySprites[4];
            speed = enemySpeeds[4];
            damageBloonDoesToLives = enemyHealths[4];
            health = enemyHealths[4];
            GameObject bloon = ObjectPooler.SharedInstance.GetPooledObject(4);
            bloon.SetActive(true);
            bloon.GetComponent<BloonCode>().wayPointIndex = wayPointIndex;
            bloon.GetComponent<BloonCode>().SlowDown();
            bloon.transform.position = gameObject.transform.position;
        }
        else if (health == 11)
        {
            image.sprite = enemySprites[6];
            speed = enemySpeeds[6];
            damageBloonDoesToLives = enemyHealths[6];
            GameObject bloon = ObjectPooler.SharedInstance.GetPooledObject(5);
            bloon.SetActive(true);
            bloon.GetComponent<BloonCode>().wayPointIndex = wayPointIndex;
            bloon.GetComponent<BloonCode>().SlowDown();
            bloon.transform.position = gameObject.transform.position;
        }
        else if (health >= 23 && health <= 72)
        {
            image.sprite = enemySprites[9];
            speed = enemySpeeds[9];
            damageBloonDoesToLives = enemyHealths[9];
        }
        else if (health >= 73 && health <= 415)
        {
            image.sprite = enemySprites[10];
            speed = enemySpeeds[10];
            damageBloonDoesToLives = enemyHealths[10];
        }
        else if(health == 416)
        {

        }
        damageBloonDoesToLives = Mathf.RoundToInt(damageBloonDoesToLives * healthMult);
        speed *= speedMult;
        return health;
    }
    private void OnEnable()
    {
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
    }
    public void SlowDown()
    {
        speed /= 2;
        Invoke("SpeedUp", 0.5f);
    }
    private void SpeedUp()
    {
        speed *= 2;
    }
}
