using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Weapon : MonoBehaviour
{
	public WeaponData weaponData;
	public SpriteRenderer spriteRenderer;
	public PolygonCollider2D polygonCollider;
	public IAttacker attacker;
	public Monster targetMonster;
	public bool moveStart = false;
	public Vector3 dir;
	// Start is called before the first frame update

	private void Awake()
	{
		Init();
	}
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		MoveToTarget();
		// 생성되자마자 타겟에게 날아가야함
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
		{
			damageable.GetDamage(attacker.GetTotalDamage());
			MoveStop();
			WeaponManager.instance.weaponPool.EnqueueWeapon(this);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		//가장 먼저 닿은 몬스터에게 충돌
		// 데미지 계산 후 몬스터에게 데미지
		// 데미지 계산 공식 = 웨폰 데미지 + 등급데미지 + 레벨 x 워리어 레벨계수 (10,20,30,40,50 등등등.......)
		if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
		{
			damageable.GetDamage(attacker.GetTotalDamage());
			MoveStop();
			WeaponManager.instance.weaponPool.EnqueueWeapon(this);
		}
	}
	void OnEnable()
	{
		// 2초 뒤에 알아서 풀로 돌아가라! (Invoke 또는 Coroutine)
		Invoke("ReturnToPool", 2.0f);
	}
	private void OnDisable()
	{
		MoveStop();
	}

	public void Init()
	{
		polygonCollider = GetComponent<PolygonCollider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		if (weaponData != null)
		{
			UpdatePolygonCollider();
		}
	}

	private void UpdatePolygonCollider()
	{
		spriteRenderer.sprite = weaponData.sprite;
		polygonCollider.pathCount = 0;
		polygonCollider.pathCount = spriteRenderer.sprite.GetPhysicsShapeCount();
		System.Collections.Generic.List<Vector2> path = new System.Collections.Generic.List<Vector2>();

		for (int i = 0; i < polygonCollider.pathCount; i++)
		{
			path.Clear();
			spriteRenderer.sprite.GetPhysicsShape(i, path);
			polygonCollider.SetPath(i, path.ToArray());
		}
	}
	public void FireWeapon(WeaponData _weaponData, IAttacker _attacker, Monster target)
	{
		SetData(_weaponData);
		SetAttacker(_attacker);
		SetTarget(target);
	}

	public void SetData(WeaponData _weaponData)
	{
		weaponData = _weaponData;
		UpdatePolygonCollider();
	}

	public void SetAttacker(IAttacker _attacker)
	{
		attacker = _attacker;
	}

	public void SetTarget(Monster target)
	{
		targetMonster = target;
		Vector3 targetPos = target.transform.position;
		dir = targetPos - this.gameObject.transform.position;
		moveStart = true; // 이 줄에서 업데이트의 MoveToTarget이 실행 시작됨
	}
	public void MoveToTarget()
	{
		if (moveStart)
		{
			transform.Translate(dir * Time.deltaTime * weaponData.speed);
		}
	}
	public void MoveStop()
	{
		moveStart = false;
		targetMonster = null;
	}
	void ReturnToPool()
	{
		MoveStop();
		CancelInvoke(); // 혹시 모르니 예약 취소
		gameObject.SetActive(false);
		WeaponManager.instance.weaponPool.EnqueueWeapon(this);
	}
}
