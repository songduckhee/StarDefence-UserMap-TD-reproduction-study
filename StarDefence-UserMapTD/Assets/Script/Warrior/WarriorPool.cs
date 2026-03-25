using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorPool : MonoBehaviour
{
    public GameObject warriorPrafeb;
    public int spawnCount;
    public Queue<Warrior> warriorPool = new Queue<Warrior>();
    public GameObject poolParents;
    public WarriorData EmptyData;

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
        for (int i = 0;  i < spawnCount; i++)
        {
            Warrior warrior = Instantiate(warriorPrafeb,poolParents.transform).GetComponent<Warrior>();
            warrior.gameObject.SetActive(false);
            warriorPool.Enqueue(warrior);
        }
    }
    public Warrior DequeueWarrior(WarriorData warriorData)
    {
        if(warriorPool.Count > 0)
        {
            Warrior warrior = warriorPool.Dequeue();
            warrior.SetData(warriorData);
            return warrior;
        }
        return null;
    }
    public void EnqueueWarrior (Warrior warrior)
    {
        warriorPool.Enqueue (warrior);
        warrior.gameObject.SetActive(false);
    }



}
