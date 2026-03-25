using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Warrior : MonoBehaviour,IAttacker
{
    public WarriorData data;
    public SpriteRenderer spriteRenderer;
    public CircleCollider2D circleCollider;


    public List<Monster> HitMonList;
    public int level;

    public float attackDamage;
    public float curWeaponSpeed;

    public bool attackStart;
    public float attackDelay;
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        if(data != null)
        {
			Init();

		}
	}

    // Update is called once per frame
    void Update()
    {
        if (attackStart)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime >= attackDelay)
            {
                elapsedTime = 0;
                if(data != null)
                {
					InstantiateWeapon();
				}
			}
        }
    }


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.TryGetComponent<Monster>(out Monster _monster))
		{
			HitMonList.Add(_monster);
			if (attackStart != true)
			{
				attackStart = true;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.TryGetComponent<Monster>(out Monster _monster))
		{
			if (HitMonList.Contains(_monster))
			{
				HitMonList.Remove(_monster);
				if (HitMonList.Count == 0)
				{
					attackStart = false;
				}
			}
		}
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.TryGetComponent<Monster>(out Monster _monster))
        {
            HitMonList.Add(_monster);
            if(attackStart != true)
            {
                attackStart = true;
			}
        }
	}
	private void OnCollisionExit2D(Collision2D collision)
	{
		if(collision.gameObject.TryGetComponent<Monster>(out Monster _monster))
        {
            if (HitMonList.Contains(_monster))
            {
                HitMonList.Remove(_monster);
                if (HitMonList.Count == 0)
                {
                    attackStart = false;
                }
            }
        }
	}

    public void Init()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        attackStart = false;
        level = 0;
        if(data != null)
        {
            attackDelay = data.attackDelay;
            spriteRenderer.sprite = data.sprite;
        }
    }

	public float GetTotalDamage()
	{
		attackDamage = data.weaponData.damage + WarriorManager.instance.GetGradeDamage(data.grade);
		return attackDamage;
	}

    public void InstantiateWeapon()
    {
        Monster _monster = HitMonList[0];
        foreach(Monster monster in HitMonList)
        {
            if(monster.splineAnim.ElapsedTime > _monster.splineAnim.ElapsedTime)
            {
                _monster = monster;
            }
        }
        Weapon weapon = WeaponManager.instance.weaponPool.DequeueWeapon();
        weapon.transform.position = this.transform.position;
        weapon.FireWeapon(data.weaponData,this,_monster);
	}
    
    public void SetData(WarriorData _data)
    {
        data = _data;
		Init();
	}
    public void ReturnWarrior()
    {
        attackStart = false;
        this.transform.position = Vector3.zero;
        this.gameObject.SetActive(false);
        WarriorManager.instance.pool.EnqueueWarrior(this);
    }
}
