using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class WarriorManager : MonoBehaviour
{
	public static WarriorManager instance;
	public WarriorPool pool;
	private Dictionary<Vector3Int, Warrior> summonData = new Dictionary<Vector3Int, Warrior>();
	private Dictionary<WarriorData, int> spawnedWarriorCount = new Dictionary<WarriorData, int>();

	public Dictionary<Grade, int> gradeDamage = new Dictionary<Grade, int>()
	{
	{ Grade.Normal, 0 },
	{ Grade.Rare, 5 },
	{ Grade.Epic, 10 },
	{ Grade.Legendary, 15 }
};

	public List<WarriorData> warriorDatas = new List<WarriorData>();
	public Dictionary<Grade, List<WarriorData>> warriorGrade = new Dictionary<Grade, List<WarriorData>>();
	// Start is called before the first frame update
	void Start()
	{
		instance = this;
		pool = GetComponent<WarriorPool>();
		pool.InitializePool();
		SetGradeDict();

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void UpdateSummonData(Vector3Int tileLocation, Warrior warrior)
	{
		summonData[tileLocation] = warrior;
	}
	public bool CheckTile(Vector3Int tileLocation, out Warrior warrior)
	{
		if (summonData.ContainsKey(tileLocation))
		{
			warrior = summonData[tileLocation];
			return true;
		}
		warrior = null;
		return false;
	}

	public void SpawnWarrior(Vector3 tilePos, Grade warriorGrade, Vector3Int tileLocation)
	{
		WarriorData spawnWarrior = GetRandomWarrior(warriorGrade);
		Warrior warrior = pool.DequeueWarrior(spawnWarrior);
		warrior.transform.position = tilePos;
		if (summonData.ContainsKey(tileLocation))
		{
			summonData[tileLocation] = warrior; // 이미 있으면 교체
		}
		else
		{
			summonData.Add(tileLocation, warrior); // 없으면 추가
		}
		warrior.gameObject.SetActive(true);
		if (spawnedWarriorCount.ContainsKey(spawnWarrior))
		{
			spawnedWarriorCount[spawnWarrior]++; // 키가 있으면 숫자를 올려주기
		}
		else
		{
			spawnedWarriorCount.Add(spawnWarrior, 1); // 없으면 추가하기
		}
	}

	public WarriorData GetRandomWarrior(Grade grade)
	{
		if (warriorGrade == null)
			return null;

		if (warriorGrade.ContainsKey(grade))
		{
			List<WarriorData> spawnList = warriorGrade[grade];
			if (spawnList.Count == 0)
			{
				Debug.LogWarning($"{grade} 등급 리스트가 비어있습니다.");
				return null;
			}
			int index = Random.Range(0, spawnList.Count);
			return spawnList[index];

		}

		return null;
	}


	public void SetGradeDict()
	{
		foreach (Grade grade in System.Enum.GetValues(typeof(Grade)))
		{
			warriorGrade[grade] = new List<WarriorData>();
		}
		foreach (var data in warriorDatas)
		{
			warriorGrade[data.grade].Add(data);
		}
	}
	public bool CheckSpawnedWarrior(WarriorData data)
	{
		if (spawnedWarriorCount.ContainsKey(data))
		{
			if (spawnedWarriorCount[data] > 1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else
		{
			return false;
		}
	}

	public bool FindSameDataWarrior(Vector3Int tilePos, WarriorData data, out Vector3Int _sameWarrior)
	{
		_sameWarrior = Vector3Int.zero;
		foreach (var pair in summonData)
		{
			Vector3Int targetPos = pair.Key;    // 좌표
			Warrior targetWarrior = pair.Value; // 전사 객체

			// 2. 내가 클릭한 타일(tilePos)이 아니고, 데이터(종류)가 같은 녀석을 찾음
			if (targetPos != tilePos && targetWarrior.data == data)
			{
				_sameWarrior = targetPos; // 찾은 녀석의 좌표를 저장!
				return true;
			}
		}

		return false;
	}
	public void RemoveSameWarrior(Vector3Int tilePos)
	{
		if (summonData.ContainsKey(tilePos))
		{
			summonData[tilePos].ReturnWarrior();
			summonData.Remove(tilePos);
		}
	}

	public float GetGradeDamage(Grade grade)
	{
		return gradeDamage[grade];
	}

	//클릭한 위치에 영웅이 소환 되어있는지
}
