using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LethalScreenPopUp : MonoBehaviour
{
    [SerializeField]
    private SlotValue slotValue;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).Find("Lethalscreen_frame_button_revive").GetChild(2).GetComponent<TextMeshProUGUI>().text = slotValue.Amount.ToString();
        transform.GetChild(0).Find("Lethalscreen_frame_button_revive").GetChild(1).GetComponent<Image>().sprite = slotValue.SlotItem.ItemIcon;

        transform.GetChild(0).Find("Lethalscreen_frame_button_exit").GetComponent<Button>().onClick.AddListener(() => {
            SceneManager.LoadScene("MainMenuScreen");
        });        

        Inventory playerInventory = GameObject.Find("PlayerInventory").GetComponent<PlayerInventory>().Inventory;
        if(playerInventory.ItemAmount(slotValue.SlotItem) >= slotValue.Amount) {
            //There are enough currency to spend
            transform.GetChild(0).Find("Lethalscreen_frame_button_revive").GetComponent<Button>().onClick.AddListener(() => {
                playerInventory.DeleteItem(slotValue);
                GameObject canvas = GameObject.Find("Canvas");
                canvas.transform.GetChild(canvas.transform.childCount - 2).GetComponentInChildren<Button>().interactable = true;
                Destroy(gameObject);
            });
        }

        else {
            //Not enough currency
            transform.GetChild(0).Find("Lethalscreen_frame_button_revive").GetComponent<Button>().interactable = false;
            transform.GetChild(0).Find("Lethalscreen_frame_button_revive").GetComponentInChildren<TextMeshProUGUI>().text = "Not enough Currency";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
