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
        transform.GetChild(0).Find("Lethalscreen_frame_button_revive").GetComponent<Button>().onClick.AddListener(() => {
            Destroy(gameObject);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
