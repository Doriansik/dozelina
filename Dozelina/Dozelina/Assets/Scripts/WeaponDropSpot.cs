using UnityEngine;

public class WeaponDropSpot : MonoBehaviour
{
    [SerializeField] private Transform weaponAnchor; // gdzie broñ ma le¿eæ
    [SerializeField] private MinigameController minigame;

    private GameObject _currentWeaponInstance;
    private GameObject _weaponPrefab;
    private bool _locked = true;

    public void AssignWeapon(GameObject weaponPrefab)
    {
        _weaponPrefab = weaponPrefab;

        // wyczyœæ poprzedni¹ broñ
        if (_currentWeaponInstance) Destroy(_currentWeaponInstance);

        if (_weaponPrefab && weaponAnchor)
        {
            _currentWeaponInstance = Instantiate(_weaponPrefab, weaponAnchor.position, Quaternion.identity, weaponAnchor);
        }
    }

    public void SetLocked(bool locked) => _locked = locked;

    private void OnMouseDown()
    {
        if (_locked) return;
        if (!minigame) return;

        // start minigry dla tej broni
        minigame.StartMinigame(_currentWeaponInstance);
        _locked = true; // blokada ¿eby nie spamowaæ startem
    }
}