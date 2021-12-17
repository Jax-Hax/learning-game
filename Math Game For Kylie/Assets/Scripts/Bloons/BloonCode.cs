using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BloonCode : MonoBehaviour
{
    public int health;
    private GameManager gameManager;
    Transform[] waypoints;
    public float speed;
    private float redEnemySpeed = 90;
    private float blueEnemySpeed;
    private int wayPointIndex = 0;
    public int damageBloonDoesToLives;
    [SerializeField]
    private GameObject[] bloons;
    [SerializeField]
    private Sprite redEnemy;
    private Image image;
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        waypoints = gameManager.mapPositions;
        image = GetComponent<Image>();
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[wayPointIndex].position, speed * Time.deltaTime);
        if(transform.position == waypoints[wayPointIndex].position)
        {
            wayPointIndex++;
        }
        if(wayPointIndex == waypoints.Length)
        {
            gameManager.health -= damageBloonDoesToLives;
            gameManager.enemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }
    public void RemoveHealth(int healthRemoved)
    {
        health -= healthRemoved;
        if (health <= 0)
        {
            gameManager.enemies.Remove(gameObject);
            Destroy(gameObject);
        }
        else if(health == 1)
        {
            image.sprite = redEnemy;
            speed = redEnemySpeed;
        }
    }
}
