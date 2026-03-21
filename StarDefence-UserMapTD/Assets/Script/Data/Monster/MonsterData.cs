using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MonsterData", order = 1)]
public class MonsterData : ScriptableObject
{
    public string name;
    public int maxHealthValue;
    public Sprite sprite;
    public int level;
}
public interface OnAttack
{
	public void GetDamage(int value);
}
