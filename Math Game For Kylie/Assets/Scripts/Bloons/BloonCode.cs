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
    [SerializeField]
    private GameObject[] bloons;
    [HideInInspector]
    public Transform whereToSpawn;
    private Image image;
    [SerializeField]
    private Sprite redEnemy;
    [SerializeField]
    private Sprite blueEnemy;
    [SerializeField]
    private Sprite greenEnemy;
    [SerializeField]
    private Sprite yellowEnemy;
    [SerializeField]
    private Sprite pinkEnemy;
    [SerializeField]
    private Sprite blackEnemy;
    [SerializeField]
    private Sprite whiteEnemy;
    [SerializeField]
    private Sprite orangeEnemy;
    [SerializeField]
    private Sprite purpleEnemy;
    [SerializeField]
    private Sprite rainbowEnemy;
    [SerializeField]
    private Sprite ceramicEnemy;
    private float redEnemySpeed = 6;
    private float blueEnemySpeed = 8.4f;
    private float greenEnemySpeed = 10.8f;
    private float yellowEnemySpeed = 19.2f;
    private float pinkEnemySpeed = 21;
    private float whiteEnemySpeed = 12;
    private float blackEnemySpeed = 10.8f;
    private float orangeEnemySpeed = 11;
    private float purpleEnemySpeed = 18;
    private float rainbowEnemySpeed = 13.2f;
    private float ceramicEnemySpeed = 15;
    private int redEnemyHP = 1;
    private int blueEnemyHP = 2;
    private int greenEnemyHP = 3;
    private int yellowEnemyHP = 4;
    private int pinkEnemyHP = 5;
    private int whiteEnemyHP = 11; //splits to 2 pink
    private int blackEnemyHP = 11; //splits to 2 pink
    private int orangeEnemyHP = 16; //splits to 3 pink
    private int purpleEnemyHP = 28; //splits to 1 orange and one black
    private int rainbowEnemyHP = 23; //splits to 1 black and one white
    private int ceramicEnemyHP = 73; //50 hp then one rainbow
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
            image.sprite = redEnemy;
            speed = redEnemySpeed;
            damageBloonDoesToLives = redEnemyHP;
        }
        else if (health == 2)
        {
            image.sprite = blueEnemy;
            speed = blueEnemySpeed;
            damageBloonDoesToLives = blueEnemyHP;
        }
        else if (health == 3)
        {
            image.sprite = greenEnemy;
            speed = greenEnemySpeed;
            damageBloonDoesToLives = greenEnemyHP;
        }
        else if (health == 4)
        {
            image.sprite = yellowEnemy;
            speed = yellowEnemySpeed;
            damageBloonDoesToLives = yellowEnemyHP;
        }
        else if (health == 5)
        {
            image.sprite = pinkEnemy;
            speed = pinkEnemySpeed;
            damageBloonDoesToLives = pinkEnemyHP;
        }
        else if (health == 10)
        {
            image.sprite = pinkEnemy;
            speed = pinkEnemySpeed;
            damageBloonDoesToLives = pinkEnemyHP;
            health = pinkEnemyHP;
            GameObject bloon = ObjectPooler.SharedInstance.GetPooledObject(4);
            bloon.SetActive(true);
            bloon.GetComponent<BloonCode>().wayPointIndex = wayPointIndex;
            bloon.GetComponent<BloonCode>().SlowDown();
            bloon.transform.position = gameObject.transform.position;
        }
        else if (health == 11)
        {
            image.sprite = whiteEnemy;
            speed = whiteEnemySpeed;
            damageBloonDoesToLives = whiteEnemyHP;
            GameObject bloon = ObjectPooler.SharedInstance.GetPooledObject(5);
            bloon.SetActive(true);
            bloon.GetComponent<BloonCode>().wayPointIndex = wayPointIndex;
            bloon.GetComponent<BloonCode>().SlowDown();
            bloon.transform.position = gameObject.transform.position;
        }
        else if (health == 23)
        {
            image.sprite = rainbowEnemy;
            speed = rainbowEnemySpeed;
            damageBloonDoesToLives = rainbowEnemyHP;
        }
        else if (health == 73)
        {
            image.sprite = ceramicEnemy;
            speed = ceramicEnemySpeed;
            damageBloonDoesToLives = ceramicEnemyHP;
        }
        damageBloonDoesToLives = Mathf.RoundToInt(damageBloonDoesToLives * healthMult);
        speed *= speedMult;
        return health;
    }
    private void OnEnable()
    {
        if (bloonType == 0)
        {
            //red
            health = redEnemyHP;
            damageBloonDoesToLives = redEnemyHP;
            image.sprite = redEnemy;
            speed = redEnemySpeed;
        }
        else if (bloonType == 1)
        {
            //blue
            health = blueEnemyHP;
            damageBloonDoesToLives = blueEnemyHP;
            image.sprite = blueEnemy;
            speed = blueEnemySpeed;
        }
        else if (bloonType == 2)
        {
            //green
            health = greenEnemyHP;
            damageBloonDoesToLives = greenEnemyHP;
            image.sprite = greenEnemy;
            speed = greenEnemySpeed;
        }
        else if (bloonType == 3)
        {
            //yellow
            health = yellowEnemyHP;
            damageBloonDoesToLives = yellowEnemyHP;
            image.sprite = yellowEnemy;
            speed = yellowEnemySpeed;
        }
        else if (bloonType == 4)
        {
            //pink
            health = pinkEnemyHP;
            damageBloonDoesToLives = pinkEnemyHP;
            image.sprite = pinkEnemy;
            speed = pinkEnemySpeed;
        }
        else if (bloonType == 5)
        {
            //black
            health = blackEnemyHP;
            damageBloonDoesToLives = blackEnemyHP;
            image.sprite = blackEnemy;
            speed = blackEnemySpeed;
        }
        else if (bloonType == 6)
        {
            //white
            health = whiteEnemyHP;
            damageBloonDoesToLives = whiteEnemyHP;
            image.sprite = whiteEnemy;
            speed = whiteEnemySpeed;
        }
        else if (bloonType == 7)
        {
            //orange
            health = orangeEnemyHP;
            damageBloonDoesToLives = orangeEnemyHP;
            image.sprite = orangeEnemy;
            speed = orangeEnemySpeed;
        }
        else if (bloonType == 8)
        {
            //purple
            health = purpleEnemyHP;
            damageBloonDoesToLives = purpleEnemyHP;
            image.sprite = purpleEnemy;
            speed = purpleEnemySpeed;
        }
        else if (bloonType == 9)
        {
            //rainbow
            health = rainbowEnemyHP;
            damageBloonDoesToLives = rainbowEnemyHP;
            image.sprite = rainbowEnemy;
            speed = rainbowEnemySpeed;
        }
        else if (bloonType == 10)
        {
            //ceramic
            health = ceramicEnemyHP;
            damageBloonDoesToLives = ceramicEnemyHP;
            image.sprite = ceramicEnemy;
            speed = ceramicEnemySpeed;
        }
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
