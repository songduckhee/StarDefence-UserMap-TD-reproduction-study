using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponData", order = 3)]
public class WeaponData : ScriptableObject
{
	public string name;
	public string description;
	public float damage;
	public float speed;
	public Sprite sprite;
	public float damageGrowthRate;
	public float speedGrowthRate; 
}
