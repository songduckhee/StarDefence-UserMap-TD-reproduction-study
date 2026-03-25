using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;

    private int gold;
    public int Gold { get { return gold; } set { gold = value; } }
    private int mineral;
    public int Mineral { get { return mineral; } set { mineral = value; } }
    public float miningTime = 2f;
    private float curTime = 0f;
    private bool canMine = false;

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI mineralText;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
		Init();

	}

    // Update is called once per frame
    void Update()
    {
		GetMineral();
	}

    public void GetGold()
    {
        gold++;
		UpdateText();

	}
    public void SpendGold(int _gold)
    {
		gold -= _gold;
		UpdateText();
	}
    
    public bool CanSpendGold(int _gold)
    {
        if(gold - _gold >= 0)
        {
			return true;
        }
        return false;
    }
    public void Init()
    {
        gold = 120;
        mineral = 0;
		UpdateText();
        canMine = true;
        GameManager.Instance.waveEnd += () => canMine = false;
        GameManager.Instance.waveFinish += () => canMine = false;

	}
    public void GetMineral()
    {
       curTime += Time.deltaTime;
        if (curTime >= miningTime)
        {
            curTime = 0f;
            mineral ++;
			UpdateText();

		}
    }
    public void SpendMineral(int _mineral)
    {
        mineral -= _mineral;
		UpdateText();

	}

    public bool CanSpendMineral(int _mineral)
    {
		if (mineral - _mineral >= 0)
		{
			return true;
		}
		return false;
	}
    public void UpdateText()
    {
        goldText.text = gold.ToString();
        mineralText.text = mineral.ToString();
    }
}
