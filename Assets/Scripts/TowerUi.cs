using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TowerUi : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image towerIcon;

    private Tower towerPrefab;
    private Tower currentSpawnedTower;


    public void SetTowerPrefab(Tower tower)
    {
       towerPrefab = tower;
       towerIcon.sprite = tower.GetTowerHeadIcon();
    }


    // Implementasi dari Interface IBeginDragHandler
    // Fungsi ini terpanggil sekali ketika pertama men-drag UI
    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject newTowerObj = Instantiate(towerPrefab.gameObject);
        currentSpawnedTower = newTowerObj.GetComponent<Tower>();
        currentSpawnedTower.ToggleOrderInLayer(true);
    }


    // Implementasi dari Interface IDragHandler
    // Fungsi ini terpanggil selama men-drag UI
    public void OnDrag(PointerEventData eventData)
    {
        Camera mainCamera = Camera.main;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -mainCamera.transform.position.z;
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        currentSpawnedTower.transform.position = targetPosition;
    }


    // Implementasi dari Interface IEndDragHandler
    // Fungsi ini terpanggil sekali ketika men-drop UI ini
    public void OnEndDrag(PointerEventData eventData)
    {
        if (currentSpawnedTower.PlacePosition == null)
        {
            Destroy(currentSpawnedTower.gameObject);
        }
        else
        {
            currentSpawnedTower.LockPlacement();
            currentSpawnedTower.ToggleOrderInLayer(false);
            LevelManager.Instance.RegisterSpawnedTower(currentSpawnedTower);
            currentSpawnedTower = null;
        }
    }
}
