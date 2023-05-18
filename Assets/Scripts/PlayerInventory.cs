using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpinWheel {

    public class PlayerInventory : MonoBehaviour {
        
        [SerializeField]
        private List<SlotValue> inventoryItems;
        private Inventory _inventory;

        public Inventory Inventory {
            get {return _inventory;}
        }

        void Awake() {
            _inventory = new Inventory(inventoryItems);
            DontDestroyOnLoad(this.gameObject);
        }
    }
    
}
