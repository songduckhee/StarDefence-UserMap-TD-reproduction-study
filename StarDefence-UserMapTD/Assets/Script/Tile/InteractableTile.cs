using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;



public class InteractableTile : MonoBehaviour
{
	private Tilemap tilemap;

	public int SummonGold = 20;
	public int replaceMineral = 30;
	public int repairMineral = 50;
	public List<Sprite> tileSprite = new List<Sprite>();
	public Tile normalTile;


	//private void OnMouseUpAsButton()
	//{
	//	Vector3 mousePos = Input.mousePosition;
	//	mousePos.z = 10f;
	//	Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	//	Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);


	//	TileBase clickedTile = tilemap.GetTile(cellPosition);
	//	if (clickedTile != null)
	//	{
	//		Tile tile = clickedTile as Tile;

	//		if (tile != null && tile.sprite != null)
	//		{
	//			Vector3 tileWorldPos = tilemap.GetCellCenterWorld(cellPosition);

	//			Vector3 screenPosition = Camera.main.WorldToScreenPoint(tileWorldPos);

	//			bool upgrade = false;
	//			bool replace = false;
	//			bool sum = false;
	//			bool repair = false;
	//			if (tile.sprite.name == "block_normal_stage_11_0")
	//			{

	//				if (WarriorManager.instance.CheckTile(cellPosition, out Warrior warrior)) //타일에 전사가 있냐 없냐
	//				{
	//					if (WarriorManager.instance.FindSameDataWarrior(cellPosition,warrior.data,out Vector3Int _warrior)) // 같은 데이터의 전사가 2개 되는지 안되는지 체크
	//					{
	//						Action action = () =>
	//						{
	//							WarriorManager.instance.RemoveSameWarrior(_warrior);
	//							WarriorManager.instance.RemoveSameWarrior(cellPosition);
	//							WarriorManager.instance.SpawnWarrior(tileWorldPos, warrior.data.grade + 1, cellPosition);
	//							UiManager.instance.SetButtonActive(false,false,false,false);
	//							Debug.Log("업그레이드 버튼 함수 설정됨");
	//						};
	//						UiManager.instance.OpenUpgradeButton(screenPosition, action);
	//						upgrade = true;
	//					}

	//					Action action2 = () =>
	//					{
	//						//미네랄
	//						if (CurrencyManager.instance.CanSpendMineral(replaceMineral))
	//						{
	//							CurrencyManager.instance.SpendMineral(replaceMineral);
	//							WarriorManager.instance.SpawnWarrior(tileWorldPos, warrior.data.grade, cellPosition);
	//						}
	//						else
	//						{
	//							UiManager.instance.WarningMassage("미네랄");
	//						}
	//					};
	//					replace = true;
	//					UiManager.instance.OpenReplaceButton(screenPosition, action2);

	//				}
	//				else
	//				{
	//					Action action = () =>
	//					{
	//						if (CurrencyManager.instance.CanSpendGold(SummonGold))
	//						{
	//							CurrencyManager.instance.SpendGold(SummonGold);
	//							WarriorManager.instance.SpawnWarrior(tileWorldPos, Grade.Normal, cellPosition);
	//						}
	//						else
	//						{
	//							UiManager.instance.WarningMassage("골드");
	//						}
	//					};
	//					sum = true;
	//					UiManager.instance.OpenSummonButton(screenPosition, action);
	//					//소환 버튼
	//				}

	//			}
	//			else if (tile.sprite.name == "block_fix_stage_11_0")
	//			{
	//				Action action = () =>
	//				{
	//					if (CurrencyManager.instance.CanSpendMineral(repairMineral))
	//					{
	//						CurrencyManager.instance.SpendMineral(repairMineral);
	//						tilemap.SetTile(cellPosition,normalTile);
	//					}
	//					else
	//					{
	//						UiManager.instance.WarningMassage("미네랄");
	//					}
	//				};
	//				repair = true;
	//				UiManager.instance.OpenRepairButton(screenPosition, action);

	//				//수정 ui버튼 옮기기
	//			}
	//			else if (tile.sprite.name == "block_stage_11_0")
	//			{
	//				//아무 일도 안생길것
	//			}
	//			UiManager.instance.SetButtonActive(sum,repair,upgrade,replace);
	//		}
	//	}
	//}

	public void MouseDownToTile()
	{
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = 10f;
		Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);


		TileBase clickedTile = tilemap.GetTile(cellPosition);

		if (EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}
		if (clickedTile != null)
		{
			Tile tile = clickedTile as Tile;

			if (tile != null && tile.sprite != null)
			{
				Vector3 tileWorldPos = tilemap.GetCellCenterWorld(cellPosition);

				Vector3 screenPosition = Camera.main.WorldToScreenPoint(tileWorldPos);

				bool upgrade = false;
				bool replace = false;
				bool sum = false;
				bool repair = false;
				if (tile.sprite.name == "block_normal_stage_11_0")
				{

					if (WarriorManager.instance.CheckTile(cellPosition, out Warrior warrior)) //타일에 전사가 있냐 없냐
					{
						if (WarriorManager.instance.FindSameDataWarrior(cellPosition, warrior.data, out Vector3Int _warrior)) // 같은 데이터의 전사가 2개 되는지 안되는지 체크
						{
							Action action = () =>
							{
								WarriorManager.instance.RemoveSameWarrior(_warrior);
								WarriorManager.instance.RemoveSameWarrior(cellPosition);
								WarriorManager.instance.SpawnWarrior(tileWorldPos, warrior.data.grade + 1, cellPosition);
								UiManager.instance.SetButtonActive(false, false, false, false);
								Debug.Log("업그레이드 버튼 함수 설정됨");
							};
							UiManager.instance.OpenUpgradeButton(screenPosition, action);
							upgrade = true;
						}

						Action action2 = () =>
						{
							//미네랄
							if (CurrencyManager.instance.CanSpendMineral(replaceMineral))
							{
								CurrencyManager.instance.SpendMineral(replaceMineral);
								WarriorManager.instance.SpawnWarrior(tileWorldPos, warrior.data.grade, cellPosition);
							}
							else
							{
								UiManager.instance.WarningMassage("미네랄");
							}
						};
						replace = true;
						UiManager.instance.OpenReplaceButton(screenPosition, action2);

					}
					else
					{
						Action action = () =>
						{
							if (CurrencyManager.instance.CanSpendGold(SummonGold))
							{
								CurrencyManager.instance.SpendGold(SummonGold);
								WarriorManager.instance.SpawnWarrior(tileWorldPos, Grade.Normal, cellPosition);
							}
							else
							{
								UiManager.instance.WarningMassage("골드");
							}
						};
						sum = true;
						UiManager.instance.OpenSummonButton(screenPosition, action);
						//소환 버튼
					}

				}
				else if (tile.sprite.name == "block_fix_stage_11_0")
				{
					Action action = () =>
					{
						if (CurrencyManager.instance.CanSpendMineral(repairMineral))
						{
							CurrencyManager.instance.SpendMineral(repairMineral);
							tilemap.SetTile(cellPosition, normalTile);
						}
						else
						{
							UiManager.instance.WarningMassage("미네랄");
						}
					};
					repair = true;
					UiManager.instance.OpenRepairButton(screenPosition, action);

					//수정 ui버튼 옮기기
				}
				else if (tile.sprite.name == "block_stage_11_0")
				{
					//아무 일도 안생길것
				}
				UiManager.instance.SetButtonActive(sum, repair, upgrade, replace);
			}
		}
		else
		{

			UiManager.instance.SetButtonActive(false, false, false, false);
		}
	}
	// Start is called before the first frame update
	void Start()
	{
		tilemap = GetComponent<Tilemap>();
	}





}
