using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float startingTimer;
    [SerializeField] private Transform  firepoint;
    [SerializeField] private GameObject[]  arrows;
    private float cooldownTimer;
    private float temp = 0;

    [Header ("SFX")]
    [SerializeField] private AudioClip arrowSound;
    [SerializeField] private AudioSource audiosrc;

    private void Attack()
    {
        cooldownTimer = 0;

        // SoundManager.instance.PlaySound(arrowSound);
        audiosrc.PlayOneShot(arrowSound);
        arrows[FindArrow()].transform.position = firepoint.position;
        arrows[FindArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void Update()
    {
        if (temp == 0)
        {
            cooldownTimer += startingTimer;
            temp += 1;
        }

        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
            Attack();
    }
}
