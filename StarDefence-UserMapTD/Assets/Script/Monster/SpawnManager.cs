using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;


public class SpawnManager : MonoBehaviour
{
	public static SpawnManager instance;
	[SerializeField]
	public SplineContainer curSpline;
	public SpawnPool pool;
	[Range(1, 60)]
	public int duration = 15;
	[Range(1, 60)]
	public int WaveTime = 30;
	public float curTime = 0f;
	public int maxWaveCount = 5;
	public int curWaveCount;

	public bool start = false;

	public event Action<int> changeWave;

	public List<Monster> spawnMonster = new List<Monster>();

	private void Awake()
	{
		instance = this;
	}
	// Start is called before the first frame update
	void Start()
	{
		Init();
	}
	// Update is called once per frame
	void Update()
	{
		if (start)
		{
			CheckWave();
		}
		
	}

	public void Init()
	{
		pool = GetComponent<SpawnPool>();
		curSpline = UnityEngine.Object.FindAnyObjectByType<SplineContainer>();
		curWaveCount = 0;
		curTime = 0f;
		pool.Init(); // 몬스터 스폰, 데이터활성화
		changeWave += pool.SpawnMonster;
		changeWave.Invoke(curWaveCount);
		start = true;
		
		
	}
	void CheckWave()
	{
		curTime += Time.deltaTime;
		if (curTime >= WaveTime)
		{
			curWaveCount++;
			curTime = 0f;
			if(curWaveCount < maxWaveCount)
			{
				changeWave.Invoke(curWaveCount);
			}
			else
			{
				if (GoalTile.instance.IsMainArrive())
				ActivateWave();
				GameManager.Instance.WaveFinish();
			}
			
		}
	}
	public void SetSpline(Monster monster)
	{
		monster.SetSpline(curSpline,duration);
	}
	public void ActivateWave()
	{
		start = !start;
	}
	public void SetSpawnMonster(List<Monster> _monster)
	{
		spawnMonster.AddRange(_monster);
	}
}
