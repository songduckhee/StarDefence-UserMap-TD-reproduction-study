using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [Range(0,1.5f)]
    public float spawnCooltime = 1f;
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
			}
            monsterPool.Add(monsters[i].waveCount,list);  
        }     
    }


    public void SpawnMonster(int curWaveCount)
    {
        if (monsterPool.ContainsKey(curWaveCount))
        {
			if (monsterPool[curWaveCount] != null)
			{
				List<GameObject> list = monsterPool[curWaveCount];
				StartCoroutine(Spawn(list));
			}
		}
        else
        {
            List<GameObject>list = monsterPool[monsterPool.Count -1];
			StartCoroutine(Spawn(list));
			Debug.Log("리스트가 비어있음!");
        }
       
    }

    IEnumerator Spawn(List<GameObject> spawnList)
    {
        int spawnedCount = 0;
        List<Monster> _monsters = new List<Monster>();
        while (true)
        {
			spawnList[spawnedCount].SetActive(true);
            Monster monster = spawnList[spawnedCount].GetComponent<Monster>();
            _monsters.Add(monster);
            monster.StartMoving();
            spawnedCount++;
            if( spawnedCount >= spawnAmount)
            {
                break;
            }
			yield return new WaitForSecondsRealtime(spawnCooltime);
		}
    }




}
