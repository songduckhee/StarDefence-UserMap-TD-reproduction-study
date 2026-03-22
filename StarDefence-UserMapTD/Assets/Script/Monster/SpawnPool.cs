using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPool : MonoBehaviour
{
	public List<MonsterData> monsters = new List<MonsterData>();
    public Dictionary<int,List<GameObject>> monsterPool = new Dictionary<int,List<GameObject>>();
	public int DefaultReadyAmount = 100;
    [Range(0,100)]
    public int spawnAmount = 50;
    public GameObject poolParents; 
    public GameObject monsterPrafeb;

    [Range(0,1.0f)]
    public float spawnOffset = 1f;
	// Start is called before the first frame update
	void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        SetDict();
    }

    public void SetDict()
    {	
		for(int i = 0; i < monsters.Count; i++)
        {
            List<GameObject> list = new List<GameObject>();
            for (int q = 0; q < DefaultReadyAmount; q++)
            {
                GameObject monster = Instantiate(monsterPrafeb, poolParents.transform);
				monster.SetActive(false);
				Monster monsterBase = monster.GetComponent<Monster>();
                monsterBase.SetData(monsters[i]);
                SpawnManager.instance.SetSpline(monsterBase);
                list.Add(monster);
               
                Debug.Log($"몬스터생성{q} ");
			}
            monsterPool.Add(monsters[i].waveCount,list);  
        }     
    }


    public void SpawnMonster(int curWaveCount)
    {
        if (monsterPool[curWaveCount] != null)
        {
			List<GameObject> list = monsterPool[curWaveCount];
			StartCoroutine(Spawn(list));
		}
        else
        {
            Debug.Log("리스트가 비어있음!");
        }
       
    }

    IEnumerator Spawn(List<GameObject> spawnList)
    {
        int spawnedCount = 0;
        while (true)
        {
			spawnList[spawnedCount].SetActive(true);
            Monster monster = spawnList[spawnedCount].GetComponent<Monster>();
            monster.StartMoving();
            spawnedCount++;
            if( spawnedCount >= spawnAmount)
            {
                break;
            }
			yield return new WaitForSecondsRealtime(spawnOffset);
		}
    }





}
