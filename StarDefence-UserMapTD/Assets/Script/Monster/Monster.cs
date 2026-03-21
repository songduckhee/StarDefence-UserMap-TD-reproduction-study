using System.Collections;
using System.Collections.Generic;
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

    public Image image;
    public SplineAnimate splineAnim;

	// Start is called before the first frame update
	void Start()
    {
        splineAnim = GetComponent<SplineAnimate>();
        image = GetComponent<Image>();
        image.sprite = data.sprite;
        maxHealth = data.maxHealthValue;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public void GetDamage(int value)
	{
        currentHealth -= value;
	}
    public void Die()
    {
        this.gameObject.SetActive(false);
    }
    public void SetSpline(SplineContainer spline)
    {
        splineAnim.Container = spline;
    }

}
