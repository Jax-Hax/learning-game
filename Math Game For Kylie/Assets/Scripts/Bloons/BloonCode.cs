using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BloonCode : MonoBehaviour
{
    public int health;
    private GameManager gameManager;
    Vector3[] waypoints;
    public float speed;
    private float redEnemySpeed = 90;
    private float blueEnemySpeed;
    public int wayPointIndex = 0;
    public int damageBloonDoesToLives;
    [SerializeField]
    private GameObject[] bloons;
    public Transform whereToSpawn;
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
        transform.position = Vector3.MoveTowards(transform.position, waypoints[wayPointIndex], speed * Time.deltaTime);
        if(transform.position == waypoints[wayPointIndex])
        {
            wayPointIndex++;
        }
        if(wayPointIndex == waypoints.Length)
        {
            gameManager.UpdateHealth(-damageBloonDoesToLives);
            gameManager.enemies.Remove(gameObject);
            Debug.Log("issue");
            gameObject.SetActive(false);
        }
    }
    public void RemoveHealth(int healthRemoved)
    {
        Debug.Log("e");
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
            GameObject bloon = ObjectPooler.SharedInstance.GetPooledObject(0);
            bloon.SetActive(true);
            bloon.GetComponent<BloonCode>().wayPointIndex = wayPointIndex;
            bloon.transform.position = gameObject.transform.position;
        }
    }
}
