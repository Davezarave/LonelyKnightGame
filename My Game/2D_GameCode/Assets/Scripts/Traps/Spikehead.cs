using UnityEngine;

public class Spikehead : EnemyDamage
{
    [Header("SpikeHead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask groundLayer;
    private Vector3[] directions = new Vector3[4];
    private Vector3 destination;
    private float checkTimer;
    private bool attacking;

    [Header("SFX")]
    [SerializeField] private AudioClip impactSound;

    private float distanceToWall;
    private float distanceToPlayer;

    private void OnEnable()
    {
        Stop();
    }
    private void Update()
    {
        //Move spikehead to destination only if attacking
        if (attacking)
            transform.Translate(destination * Time.deltaTime * speed);
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
                CheckForPlayer();
        }
    }
    private void CheckForPlayer()
    {
        CalculateDirections();

        //Check if spikehead sees player in all 4 directions
        for (int i = 0; i < directions.Length; i++)
        {
            //Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);
            RaycastHit2D ghit = Physics2D.Raycast(transform.position, directions[i], range * 2, groundLayer);
            distanceToWall = ghit.distance == 0 ? range : ghit.distance;
            distanceToPlayer = hit.distance;

            if (hit.collider != null && !attacking && (distanceToWall > distanceToPlayer))
            {
                //Debug.Log("distance to wall " + distanceToWall);
                //Debug.Log("distance to player " + distanceToPlayer);
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }
    private void CalculateDirections()
    {
        directions[0] = transform.right * range; //Right direction
        directions[1] = -transform.right * range; //Left direction
        directions[2] = transform.up * range; //Up direction
        directions[3] = -transform.up * range; //Down direction
    }
    private void Stop()
    {
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Trap")
        {
            Physics2D.IgnoreLayerCollision(8, 12, true);
            base.OnTriggerEnter2D(collision);
        }
        else
        {
            SoundManager.instance.PlaySound(impactSound);
            Stop(); //Stop spikehead once he hits something
            WhatSideOfTheColliderWasHit();

        }
    }
    private void WhatSideOfTheColliderWasHit()
    {
        if (destination.x == directions[0].x && destination.y == directions[0].y)
        {
            transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y);
            //Debug.Log("Right");
        }
        else if (destination.x == directions[1].x && destination.y == directions[1].y)
        {
            transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y);
            //Debug.Log("Left");
        }
        else if (destination.x == directions[2].x && destination.y == directions[2].y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f);
            //Debug.Log("Up");
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f);
            //Debug.Log("Down");
        }
    }

}
