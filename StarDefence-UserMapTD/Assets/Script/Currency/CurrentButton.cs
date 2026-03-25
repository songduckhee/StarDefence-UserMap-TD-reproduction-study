using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ButtonType
{
	summon,
	repair,
	upgrade,
	replace
}


public class CurrencyButton : MonoBehaviour
{
	private Button button;
	[SerializeField]
	private TextMeshProUGUI tmp;
	public ButtonType type;
	public int amount;
	// Start is called before the first frame update
	void Start()
	{
		Init();
	}

	public void SetAddlistner(Action action)
	{
		button.onClick.RemoveAllListeners();
		button.onClick.AddListener(() => { action?.Invoke(); UiManager.instance.SetButtonActive(false,false,false,false); });
	}

	public void Init()
	{
		if(tmp != null)
		{
			tmp.text = amount.ToString();
		}
		button = GetComponent<Button>();
	}

}
