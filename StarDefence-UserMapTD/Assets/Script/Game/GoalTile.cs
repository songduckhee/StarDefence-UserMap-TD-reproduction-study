using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTile : MonoBehaviour
{
	public int Health = 1000;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.TryGetComponent<Monster>(out Monster monster))
		{
			Health -= monster.data.Damage;
			monster.gameObject.SetActive(false);
			if(Health <= 0)
			{
				GameManager.Instance.GameOver();
			}
		}
	}
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}
