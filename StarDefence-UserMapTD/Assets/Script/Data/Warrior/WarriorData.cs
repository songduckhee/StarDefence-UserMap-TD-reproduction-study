using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WarriorData", order = 2)]
public class WarriorData : ScriptableObject
{
	public string name;
	public string description;
	public Sprite sprite;
	public Grade grade;
	public WeaponData weaponData;
	[Range(0f, 2f)]
	public float attackDelay;
}

public enum Grade
{
	//grade = damage
	Normal = 0,
	Rare = 1,
	Epic = 2,
	Legendary = 3
}
