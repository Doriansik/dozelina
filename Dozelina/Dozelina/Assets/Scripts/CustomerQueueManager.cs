using UnityEngine;

public class CustomerQueueManager : MonoBehaviour
{
    [System.Serializable]
    public class CustomerOrder
    {
        public GameObject customer;
        public GameObject weaponModel;
    }

    [Header("Order (customers + weapons in scene)")]
    [SerializeField] private CustomerOrder[] order;

    [Header("Points")]
    [SerializeField] private Transform customerStandPoint;
    [SerializeField] private Transform weaponStandPoint;

    private int _index = -1;
    private GameObject _currentCustomer;
    private GameObject _currentWeapon;

    private bool _waitingForMinigameResult;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        for (int i = 0; i < order.Length; i++)
        {
            if (order[i].customer) order[i].customer.SetActive(false);
            if (order[i].weaponModel) order[i].weaponModel.SetActive(false);
        }

        Next();
    }

    private void Next()
    {
        if (_currentCustomer) _currentCustomer.SetActive(false);
        if (_currentWeapon) _currentWeapon.SetActive(false);

        _index++;

        if (_index >= order.Length)
        {
            Debug.Log("[Queue] Koniec kolejki klientów.");
            return;
        }

        _currentCustomer = order[_index].customer;
        _currentWeapon = order[_index].weaponModel;

        if (_currentCustomer)
        {
            _currentCustomer.SetActive(true);

            if (customerStandPoint)
                _currentCustomer.transform.SetPositionAndRotation(
                    customerStandPoint.position,
                    customerStandPoint.rotation
                );
        }

        if (_currentWeapon)
        {
            _currentWeapon.SetActive(true);

            if (weaponStandPoint)
                _currentWeapon.transform.position = weaponStandPoint.position; // bez zmiany rotacji
        }

        _waitingForMinigameResult = true;

        Debug.Log($"[Queue] Klient #{_index + 1} | Broñ: {_currentWeapon?.name}");
    }

    // wo³ane po sukcesie minigry
    public void AdvanceQueueFromMinigame()
    {
        if (!_waitingForMinigameResult)
        {
            Debug.LogWarning("[Queue] AdvanceQueueFromMinigame() ale nie czekamy na minigrê.");
            return;
        }

        _waitingForMinigameResult = false;

        Debug.Log("[Queue] Minigra ukoñczona -> nastêpny klient");
        Next();
    }

    public GameObject CurrentWeapon => _currentWeapon;
}