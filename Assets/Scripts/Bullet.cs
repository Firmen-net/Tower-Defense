using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int bulletPower;
    private float bulletSpeed;
    private float bulletSplashRadius;
    private Enemy targetEnemy;

    // FixedUpdate adalah update yang lebih konsisten jeda pemanggilannya
    // cocok digunakan jika karakter memiliki Physic (Rigidbody, dll)
    private void FixedUpdate()
    {
        if (targetEnemy != null)
        {
            if (!targetEnemy.gameObject.activeSelf)
            {
                gameObject.SetActive(false);
                targetEnemy = null;

                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, targetEnemy.transform.position, bulletSpeed * Time.fixedDeltaTime);
            Vector3 direction = targetEnemy.transform.position - transform.position;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, targetAngle - 90f));
        }

        if (LevelManager.Instance.IsOver)
        {
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)

    {
        if (targetEnemy == null)
        {
            return;
        }

        if (collision.gameObject.Equals(targetEnemy.gameObject))
        {
            gameObject.SetActive(false);

            // Bullet yang memiliki efek splash area
            if (bulletSplashRadius > 0f)
            {
                LevelManager.Instance.ExplodeAt(transform.position, bulletSplashRadius, bulletPower);
            }

            // Bullet yang hanya single-target
            else
            {
                targetEnemy.ReduceEnemyHealth(bulletPower);
            }

            targetEnemy = null;
        }

    }

    public void SetProperties(int _bulletPower, float _bulletSpeed, float _bulletSplashRadius)

    {
        bulletPower = _bulletPower;
        bulletSpeed = _bulletSpeed;
        bulletSplashRadius = _bulletSplashRadius;
    }


    public void SetTargetEnemy(Enemy enemy)

    {
        targetEnemy = enemy;
    }
}
