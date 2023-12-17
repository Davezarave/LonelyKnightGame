using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header ("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header ("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
            SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");

                // //Player
                if (GetComponent<PlayerMovement>() != null)
                // {
                //     GetComponent<PlayerMovement>().enabled = false;
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
                // }

                // //Enemy
                // if (GetComponentInParent<EnemyPatrol>() != null)
                //     GetComponentInParent<EnemyPatrol>().enabled = false;

                // if (GetComponent<MeleeEnemy>() !=null)
                //     GetComponent<MeleeEnemy>().enabled = false;

                foreach (Behaviour components in components)
                    components.enabled = false;

                dead = true;
                SoundManager.instance.PlaySound(deathSound);

                if (gameObject.tag == "Player")
                    StartCoroutine(Restart());

            }
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(8, 9, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
        invulnerable = false;
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(3);    //Wait one frame
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
