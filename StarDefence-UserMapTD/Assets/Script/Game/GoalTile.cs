using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoalTile : MonoBehaviour
{
	public static GoalTile instance;
	public float health = 100;
	public float maxHealth = 100;


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.TryGetComponent<Monster>(out Monster monster))
		{
			health -= monster.data.damage;
			UiManager.instance.ChangeFillAmount(health / maxHealth);
			monster.gameObject.SetActive(false);
			if (health <= 0)
			{
				GameManager.Instance.GameOver();
			}
		}
	}
	
	// Start is called before the first frame update
	void Start()
	{
		health = maxHealth;
		instance = this;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public bool IsMainArrive()
	{
		if(health >= 1)
		{
			return true;
		}
		return false;
	}
}
