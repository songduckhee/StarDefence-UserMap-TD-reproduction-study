using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiManager : MonoBehaviour
{
	public static UiManager instance;
	[SerializeField]
	private CurrencyButton summonButton;
	[SerializeField]
	private CurrencyButton repairButton;
	[SerializeField]
	private CurrencyButton upgradeButton;
	[SerializeField]
	private CurrencyButton replaceButton;
	[SerializeField]
	private GameObject warningImage;
	[SerializeField]
	private TextMeshProUGUI warningText;
	public float WarningHidedelay = 4;
	public float ButtonPosoffset = 15;

	public GameObject gameClear;

	public Image HealthBar;
    // Start is called before the first frame update
    void Start()
    {
        Init();
		GameManager.Instance.waveEnd += () => GameClear("게임오버");
		GameManager.Instance.waveFinish += () => GameClear("게임클리어!");
	}

    public void Init()
    {
		instance = this;
		if (summonButton != null) summonButton.gameObject.SetActive(false); summonButton.Init();
		if (repairButton != null) repairButton.gameObject.SetActive(false); repairButton.Init();
		if (upgradeButton != null) upgradeButton.gameObject.SetActive(false); upgradeButton.Init();
		if (replaceButton != null) replaceButton.gameObject.SetActive(false); replaceButton.Init();
		gameClear.SetActive(false);
		warningImage.GetComponentInChildren<TextMeshProUGUI>();
		warningImage.SetActive(false);
	}

    public void OpenSummonButton (Vector3 vectorPos,Action action) // 소환 골드
    {
		summonButton.transform.position = vectorPos ;
        summonButton.gameObject.SetActive (true);
        summonButton.SetAddlistner(action);
    }
	public void OpenRepairButton(Vector3 vectorPos, Action action)// 수리 미네랄
	{
		repairButton.transform.position = vectorPos;
		repairButton.gameObject.SetActive(true);
		repairButton.SetAddlistner(action);
	}
	public void OpenUpgradeButton(Vector3 vectorPos, Action action) // 승급 (새 등급의 몬스터로 바꿔줌) 무료
	{
		upgradeButton.transform.position = vectorPos +new Vector3 (0f, ButtonPosoffset,0f);
		upgradeButton.gameObject.SetActive(true);
		upgradeButton.SetAddlistner(action);
	}
	public void OpenReplaceButton(Vector3 vectorPos, Action action) // 교체 미네랄
	{
		
		replaceButton.transform.position = vectorPos;
		replaceButton.gameObject.SetActive(true);
		replaceButton.SetAddlistner(action);
	}
	public void WarningMassage(string currency)
	{
		
		string particle = currency == "골드" ?"가" : "이";
		float size = currency == "골드" ? 24.31f : 24.31f;
		warningText.text = $"{currency}{particle} 부족합니다! 재화를 채워주세요";
		warningText.fontSize = size;
		warningImage.SetActive(true);
		DOVirtual.DelayedCall(WarningHidedelay, () => warningImage.SetActive(false));
	}

	public void SetButtonActive(bool sum,bool repair,bool upgrade,bool replace)
	{
		summonButton.gameObject.SetActive(sum);
		repairButton.gameObject.SetActive(repair);
		upgradeButton.gameObject.SetActive(upgrade);
		replaceButton.gameObject.SetActive(replace);
	}

	public void GameClear(string _string)
	{
		TextMeshProUGUI text = gameClear.GetComponentInChildren<TextMeshProUGUI>();
		text.text = _string;
		gameClear.SetActive(true);
	}

	public void ChangeFillAmount(float amount)
	{
		HealthBar.fillAmount = amount;
	}

}
