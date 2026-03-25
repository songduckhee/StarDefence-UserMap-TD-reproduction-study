using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPool : MonoBehaviour
{
	public GameObject weaponPrafeb;
	[Range(0, 100)]
	public int weaponCount = 50;
    public Queue<Weapon> weaponPool = new Queue<Weapon>();
    public GameObject poolParents;
	// Start is called before the first frame update

	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializePool()
    {
        for (int i = 0; i < weaponCount; i++)
        {
            Weapon weapon = Instantiate(weaponPrafeb,poolParents.transform).GetComponent<Weapon>();
            weapon.gameObject.SetActive(false);
            weaponPool.Enqueue(weapon);
        }
    }
    public Weapon DequeueWeapon()
    {
        Weapon weapon = weaponPool.Dequeue();
        weapon.gameObject.SetActive(true);
        return weapon;
    }
    public void EnqueueWeapon(Weapon weapon)
    {
        weapon.gameObject.SetActive(false);
        weapon.transform.position = Vector3.zero;
        weaponPool.Enqueue((Weapon)weapon);
    }
}
