using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
	[SerializeField] private float attackDamage;
	[SerializeField] private float enragedAttackDamage;

	[SerializeField] private Vector3 attackOffset;
	[SerializeField] private float attackRange;
	[SerializeField] private LayerMask attackMask;

	[Header("Attack Sound")]
	[SerializeField] private AudioClip attackSound;

	private Health playerHealth;

	public void Attack()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
		if (colInfo != null)
		{
			playerHealth = colInfo.transform.GetComponent<Health>();
			DamagePlayer(attackDamage);
		}
	}

	public void EnragedAttack()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
		if (colInfo != null)
		{
			playerHealth = colInfo.transform.GetComponent<Health>();
			DamagePlayer(enragedAttackDamage);
		}
	}

	void OnDrawGizmosSelected()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Gizmos.DrawWireSphere(pos, attackRange);
	}

	private void DamagePlayer(float _dmg)
	{
		SoundManager.instance.PlaySound(attackSound);
		playerHealth.TakeDamage(_dmg);
	}
}
