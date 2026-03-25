using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;


public class Monster : MonoBehaviour,IDamageable
{
    [Header("Health")]
    public float currentHealth;
    public float maxHealth;

    [Header("data")]
    public MonsterData data;

    public BoxCollider2D boxCollider;
    public SpriteRenderer spriteRenderer;
    public SplineAnimate splineAnim;

	// Start is called before the first frame update
	void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init()
    {
		splineAnim = GetComponent<SplineAnimate>();
		spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
	}

	public void SetData(MonsterData _data)
    {
		data = _data;
		spriteRenderer.sprite = data.sprite;
		maxHealth = data.maxHealthValue;
		currentHealth = maxHealth;
		boxCollider.size = spriteRenderer.sprite.bounds.size;
		boxCollider.offset = spriteRenderer.sprite.bounds.center;
	}
	public void GetDamage(float value)
	{
        currentHealth -= value;
        if (currentHealth <= 0)
        {
            Die();
        }
	}
    public void Die()
    {
        CurrencyManager.instance.GetGold();
        this.gameObject.SetActive(false);
        splineAnim.Pause();
    }
    public void SetSpline(SplineContainer spline,int duration)
    {
        splineAnim.Container = spline;
        splineAnim.Duration = duration;
        splineAnim.Loop = SplineAnimate.LoopMode.Once;
        splineAnim.ElapsedTime = 0;
    }
    public void StartMoving()
    {
		splineAnim.ElapsedTime = 0f;
		splineAnim.Play();
	}
    public float CursplineTime()
    {
        float curtime = splineAnim.ElapsedTime;
        return curtime;
    }

}
