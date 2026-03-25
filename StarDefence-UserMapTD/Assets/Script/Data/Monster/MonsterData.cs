using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MonsterData", order = 1)]
public class MonsterData : ScriptableObject
{
    public string name;
    public int maxHealthValue;
    public Sprite sprite;
    public int waveCount;
    public int damage;
}
public interface IDamageable
{
	public void GetDamage(float value);
}
public interface IAttacker
{
    float GetTotalDamage();
}

