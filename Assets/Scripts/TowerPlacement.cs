using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    private Tower placedTower;


    // Fungsi yang terpanggil sekali ketika ada object Rigidbody yang menyentuh area collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (placedTower != null)
        {
            return;
        }

        Tower tower = collision.GetComponent<Tower>();
        if (tower != null)
        {
            tower.SetPlacePosition(transform.position);
            placedTower = tower;
        }
    }


    // Kebalikan dari OnTriggerEnter2D, fungsi ini terpanggil sekali ketika object tersebut meninggalkan area collider
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (placedTower == null)
        {
            return;
        }

        placedTower.SetPlacePosition(null);
        placedTower = null;
    }
}
