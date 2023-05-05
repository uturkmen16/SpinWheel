using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitScreenPopUp : MonoBehaviour
{
    public Inventory earnedItems;
    void Start()
    {
        this.transform.GetChild(0).Find("Exitscreen_button_exit").GetComponent<Button>().onClick.AddListener(() => {
            GameObject.Find("PlayerInventory").GetComponent<PlayerInventory>().Inventory.AddInventory(earnedItems);
            SceneManager.LoadScene("MainMenuScreen");
        });
        this.transform.GetChild(0).Find("Exitscreen_button_goback").GetComponent<Button>().onClick.AddListener(() => {
            Destroy(gameObject);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
