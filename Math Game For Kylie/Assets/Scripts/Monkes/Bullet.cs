using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public int damage = 1;
	public int pierce = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("red") || collision.gameObject.CompareTag("blue"))
        {
            collision.gameObject.GetComponent<BloonCode>().RemoveHealth(damage, false, false, false, false, false, false);
			pierce--;
			if(pierce <= 0)
            {
				Destroy(gameObject);
            }
		}
        else if (collision.gameObject.CompareTag("MapBorder"))
        {
            Destroy(gameObject);
        }
    }
}
