using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private int maxHealth = 1;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private SpriteRenderer healthBar;
    [SerializeField] private SpriteRenderer healthFill;

    private int currentHealth;
    public Vector3 TargetPosition { get; private set; }
    public int CurrentPathIndex { get; private set; }


    // Fungsi ini terpanggil sekali setiap kali menghidupkan game object yang memiliki script ini
    private void OnEnable()
    {
        currentHealth = maxHealth;
        healthFill.size = healthBar.size;
    }


    public void MoveToTarget()

    {
        transform.position = Vector3.MoveTowards(transform.position, TargetPosition, moveSpeed * Time.deltaTime);
    }


    public void SetTargetPosition(Vector3 targetPosition)

    {
        TargetPosition = targetPosition;
        healthBar.transform.parent = null;

        // Mengubah rotasi dari enemy
        Vector3 distance = TargetPosition - transform.position;
        if (Mathf.Abs(distance.y) > Mathf.Abs(distance.x))
        {
            // Menghadap atas
            if (distance.y > 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
            }
            // Menghadap bawah
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, -90f));
            }
        }
        else
        {
            // Menghadap kanan (default)
            if (distance.x > 0)

            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            }

            // Menghadap kiri
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
            }
        }
        healthBar.transform.parent = transform;
    }


    public void ReduceEnemyHealth(int damage)

    {
        currentHealth -= damage;
        AudioPlayer.Instance.PlaySFX("hit-enemy");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            gameObject.SetActive(false);
            Debug.Log("Enemy Destroyed");
            AudioPlayer.Instance.PlaySFX("enemy-die");
        }

        float healthPercentage = (float)currentHealth / maxHealth;
        healthFill.size = new Vector2(healthPercentage * healthBar.size.x, healthBar.size.y);
    }


    // Menandai indeks terakhir pada path
    public void SetCurrentPathIndex(int currentIndex)

    {
        CurrentPathIndex = currentIndex;
    }
}
