using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpinWheel {

    public class PlayerInventory : MonoBehaviour {

        public static Inventory inventory;
        
        [SerializeField]
        private List<SlotValue> inventoryItems;

        void Awake() {
            inventory = new Inventory(inventoryItems);
        }
    }
    
}
