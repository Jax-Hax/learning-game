using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    public List<GameObject> activeList = new List<GameObject>();
    public List<GameObject> notActiveList = new List<GameObject>();
    private void Start()
    {
        GameManager.SharedInstance.ranges.Add(this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (notActiveList.Contains(collision.gameObject))
        {
            activeList.Insert(0, collision.gameObject);
        }
        else
        {
            activeList.Add(collision.gameObject);
            notActiveList.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        activeList.Remove(collision.gameObject);
    }
}
