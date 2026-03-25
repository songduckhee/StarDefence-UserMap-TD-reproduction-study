using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public event Action waveFinish;
    public event Action waveEnd;

	// Start is called before the first frame update

	private void Awake()
	{
		Instance = this;
	}
	void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WaveStart()
    {

    }

    public void WaveFinish()
    {
        waveFinish.Invoke();
    }
    public void GameOver()
    {
		waveEnd.Invoke();
		Debug.Log("게임 오버!");
        Time.timeScale = 0f;
    }
    
}
