using UnityEngine;

public class CustomerController : MonoBehaviour
{
    [Header("Customer weapon")]
    [SerializeField] private GameObject weaponPrefab;
    public GameObject WeaponPrefab => weaponPrefab;
}