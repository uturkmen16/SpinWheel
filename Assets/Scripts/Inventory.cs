using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory {
    
    public Inventory() {
        _inventory = new List<SlotValue>();
    }

    private List<SlotValue> _inventory;

    public List<SlotValue> InventoryList {
        get {return _inventory;}
    }

    //Returns the number of type of items in inventory
    public int InventoryLength {
        get {return _inventory.Count;}
    }

    public void AddItem(SlotValue slotValue) {
        for(int i = 0; i < _inventory.Count; i++) {
            if(slotValue.SlotItem.ItemName == _inventory[i].SlotItem.ItemName) {
                //Item already exists so increase the amount
                _inventory[i].Amount += slotValue.Amount;
                return;
            }
        }

        //Item doesn't exists so add the item to inventory
        _inventory.Add(slotValue);
    }

    //Returns false if delete item amount is higher than what is available
    public bool DeleteItem(SlotValue slotValue) {
        for(int i = 0; i < _inventory.Count; i++) {
            if(slotValue.SlotItem.ItemName == _inventory[i].SlotItem.ItemName) {
                if(_inventory[i].Amount >= slotValue.Amount) {
                    //There is enough amount of item to be deleted
                    _inventory[i].Amount -= slotValue.Amount;
                    return true;

                }

                else {
                    //There is not enough amount of item to be deleted
                    return false;
                }
            }
        }
        //There is none of the item to be deleted
        return false;
    }

    //Deletes all of the existing items
    public void DeleteInventory() {
        _inventory = new List<SlotValue>();
    }
}