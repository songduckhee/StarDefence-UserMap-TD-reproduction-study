using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;


public class Monster : MonoBehaviour,OnAttack
{
    [Header("Health")]
    public int currentHealth;
    public int maxHealth;

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
	public void GetDamage(int value)
	{
        currentHealth -= value;
        if (currentHealth <= 0)
        {
            Die();
        }
	}
    public void Die()
    {
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

}
