using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;
    public WeaponPool weaponPool;
   
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        weaponPool = GetComponent<WeaponPool>();
        if(weaponPool != null)
        {
            weaponPool.InitializePool();
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
