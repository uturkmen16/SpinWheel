using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private List<SlotValue> inventoryItems;
    private Inventory _inventory;
    // Start is called before the first frame update
    public Inventory Inventory {
        get {return _inventory;}
    }

    void Awake() {
        _inventory = new Inventory(inventoryItems);
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
