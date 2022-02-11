using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BloonCode : MonoBehaviour
{
    public int health;
    public int bloonType;
    private GameManager gameManager;
    Vector3[] waypoints;
    public float speed;
    private float redEnemySpeed = 6;
    private float blueEnemySpeed = 10;
    public int wayPointIndex = 0;
    public int damageBloonDoesToLives;
    [SerializeField]
    private GameObject[] bloons;
    public Transform whereToSpawn;
    [SerializeField]
    private Sprite redEnemy;
    [SerializeField]
    private Sprite blueEnemy;
    private Image image;
    private void OnEnable()
    {
        if(bloonType == 0)
        {
            health = 1;
        }
        else if(bloonType == 1)
        {
            health = 2;
            damageBloonDoesToLives = 2;
            image.sprite = blueEnemy;
            speed = blueEnemySpeed;
        }
    }
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
    public void RemoveHealth(int healthRemoved)
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
            damageBloonDoesToLives = 1;
            /*GameObject bloon = ObjectPooler.SharedInstance.GetPooledObject(0);
            bloon.SetActive(true);
            bloon.GetComponent<BloonCode>().wayPointIndex = wayPointIndex;
            bloon.transform.position = gameObject.transform.position;*/
        }
    }
}
