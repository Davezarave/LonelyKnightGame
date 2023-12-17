using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{

	[SerializeField] private float startingHealth;
	[SerializeField] private float enrageAt;
	public float currentHealth { get; private set; }

	[Header("Death Sound")]
	[SerializeField] private AudioClip deathSound;
	[SerializeField] private AudioClip hurtSound;

	public GameObject deathEffect;

	public bool isInvulnerable = false;

	private void Awake()
	{
		currentHealth = startingHealth;
		Debug.Log(currentHealth);
	}

	public void TakeDamage(int _damage)
	{
		if (isInvulnerable)
			return;

		currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
		if (currentHealth > 0)
		{
			SoundManager.instance.PlaySound(hurtSound);
		}

		Debug.Log(currentHealth);

		if (currentHealth == enrageAt)
		{
			GetComponent<Animator>().SetBool("IsEnraged", true);
		}

		if (currentHealth <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		SoundManager.instance.PlaySound(deathSound);
		Instantiate(deathEffect, transform.position, Quaternion.identity);
		gameObject.SetActive(false);
	}

}
