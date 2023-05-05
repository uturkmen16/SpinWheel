using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private List<LevelSettings> levelSettings;
    [SerializeField]
    private int silverPeriod;
    [SerializeField]
    private int goldPeriod;
    [SerializeField]
    private GameObject slotValuePrefab;
    [SerializeField]
    private GameObject bronzePrefab;
    [SerializeField]
    private GameObject silverPrefab;
    [SerializeField]
    private GameObject goldPrefab;
    [SerializeField]
    private GameObject exitScreenPrefab;
    [SerializeField]
    private GameObject lethalScreenPrefab;
    [SerializeField]
    private GameObject levelLabelPrefab;    
    [SerializeField]
    private List<Sprite> levelLabelSprites;

    
    private GameObject currentLevelInstance;

    //Level number that increases each level starting from level 1
    private int currentLevelNo = 1;

    private Inventory rewardsInventory = new Inventory();
    void Start() {
        GenerateLevelLabels();
        GameObject.Find("Rewards_frame_button_exit").GetComponent<Button>().onClick.AddListener(ExitButtonCallback);
        InitLevel(currentLevelNo);
    }

    void Update() {
    }

    void OnValidate() {
        if(silverPeriod < 2) {
            Debug.LogWarning("Silver period must be higher than 1!");
            silverPeriod = 2;
        }
        if(goldPeriod <= silverPeriod) {
            Debug.LogWarning("Gold Period must be higher than silver period!");
            goldPeriod = silverPeriod + 1;
        }
    }

    private void InitLevel(int levelNo) {

        levelSettings[levelNo - 1].ShuffleRewards();

        if(levelNo % goldPeriod == 0) currentLevelInstance = Instantiate(goldPrefab, transform);
        else if(levelNo % silverPeriod == 0) currentLevelInstance = Instantiate(silverPrefab, transform);
        else currentLevelInstance = Instantiate(bronzePrefab, transform);

        Transform baseTransform = currentLevelInstance.transform.GetChild(0);

        for(int i = 0; i < baseTransform.childCount; i++) {
            Transform childTransform = baseTransform.GetChild(i);
            childTransform.GetComponentInChildren<Image>().sprite = levelSettings[levelNo - 1].Rewards[i].SlotItem.ItemIcon;
            int amount = levelSettings[levelNo - 1].Rewards[i].Amount;
            childTransform.GetComponentInChildren<TextMeshProUGUI>().text = "x" + Utils.AbbreviateInteger(amount);
        }

        //Add spin button callback
        currentLevelInstance.GetComponentInChildren<Button>().onClick.AddListener(SpinWheel);
    }

    private void DisplayInventory() {

        GameObject container = GameObject.Find("Rewards_frame_display_container");
        //Delete all of the children
        foreach (Transform child in container.transform) {
            GameObject.Destroy(child.gameObject);
        }


        for(int i = 0; i < rewardsInventory.InventoryLength; i++) {
            Sprite sprite = rewardsInventory.ItemAt(i).SlotItem.ItemIcon;
            GameObject slotValueObject = Instantiate(slotValuePrefab, container.transform);
            slotValueObject.GetComponentInChildren<Image>().sprite = rewardsInventory.ItemAt(i).SlotItem.ItemIcon;
            slotValueObject.GetComponentInChildren<TextMeshProUGUI>().text = rewardsInventory.ItemAt(i).Amount.ToString();
            float height = slotValueObject.GetComponentInChildren<RectTransform>().sizeDelta.y;
            slotValueObject.GetComponentInChildren<RectTransform>().anchoredPosition = new Vector2(0, - height * i / 2);
            if(i >= rewardsInventory.InventoryLength - 1) {
                //Last child
                //Expand container according to the children
                container.GetComponent<RectTransform>().sizeDelta = new Vector2(container.GetComponent<RectTransform>().sizeDelta.x, height * (i + 1) / 2);
            }
        }
    }

    private void SpinWheel() {
        //Exit button is inactive since game started
        GameObject.Find("Rewards_frame_button_exit").GetComponent<Button>().interactable = false;

        int randomInt = UnityEngine.Random.Range(levelSettings[currentLevelNo - 1].MinimumRoll, levelSettings[currentLevelNo - 1].MaximumRoll);
        currentLevelInstance.transform.GetChild(0).transform.DORotate(new Vector3(0, 0, randomInt * 45), 1 + randomInt / 5, RotateMode.LocalAxisAdd)
        .OnComplete(() => {
            //Spin animation is over
            if(levelSettings[currentLevelNo - 1].Rewards[randomInt % 8].SlotItem.ItemType == ItemType.Lethal) {
                //Item is a lethal bomb
                Destroy(currentLevelInstance);

                if(currentLevelNo <= levelSettings.Count) {
                    currentLevelNo++;
                    if(currentLevelNo % silverPeriod == 0 || currentLevelNo % goldPeriod == 0) {
                        //Exit button is active
                        GameObject.Find("Rewards_frame_button_exit").GetComponent<Button>().interactable = true;
                    }
                    GenerateLevelLabels();
                    InitLevel(currentLevelNo);
                }
                Instantiate(lethalScreenPrefab, this.transform);
                //Change this from being hardcoded
                //transform.GetChild(transform.childCount - 2).GetComponentInChildren<Button>().interactable = false;
            }

            else {
                //Item is an inventory item
                rewardsInventory.AddItem(levelSettings[currentLevelNo - 1].Rewards[randomInt % 8]);

                DisplayInventory();

                Destroy(currentLevelInstance);

                if(currentLevelNo <= levelSettings.Count) {
                    currentLevelNo++;
                    if(currentLevelNo % silverPeriod == 0 || currentLevelNo % goldPeriod == 0) {
                        //Exit button is active
                        GameObject.Find("Rewards_frame_button_exit").GetComponent<Button>().interactable = true;
                    }
                    GenerateLevelLabels();
                    InitLevel(currentLevelNo);
                }

                else {
                    //There are no more levels
                    GameObject.Find("PlayerInventory").GetComponent<PlayerInventory>().Inventory.AddInventory(rewardsInventory);
                    SceneManager.LoadScene("MainMenuScreen");
                }
            }
        });

        //Change this from being hardcoded
        transform.GetChild(transform.childCount - 1).GetComponentInChildren<Button>().interactable = false;
    }

    private void ExitButtonCallback() {

        GameObject exitPopup = Instantiate(exitScreenPrefab, this.transform);
        exitPopup.GetComponent<ExitScreenPopUp>().earnedItems = rewardsInventory;
    }

    private void GenerateLevelLabels() {

        float labelWidth = levelLabelPrefab.GetComponent<RectTransform>().sizeDelta.x + 20;

        GameObject container = GameObject.Find("Levels_frame_container_levellabels");

        //Delete existing childs
        foreach (Transform transform in container.transform) {
            Destroy(transform.gameObject);
        }

        //Set container size
        //container.GetComponent<RectTransform>().sizeDelta = new Vector2(labelWidth * levelSettings.Count, container.GetComponent<RectTransform>().sizeDelta.y);
        //container.GetComponent<RectTransform>().anchoredPosition = new Vector2(labelWidth * levelSettings.Count / 2, container.GetComponent<RectTransform>().anchoredPosition.y);
        for(int i = 0; i < levelSettings.Count; i++) {
            GameObject levelLabel = Instantiate(levelLabelPrefab, container.transform);
            levelLabel.GetComponent<RectTransform>().anchoredPosition = new Vector2(labelWidth * (i - (currentLevelNo - 1)), levelLabel.GetComponent<RectTransform>().anchoredPosition.y);
            levelLabel.GetComponentInChildren<TextMeshProUGUI>().text = (i + 1).ToString();
            if((i + 1) % silverPeriod == 0) {
                levelLabel.GetComponent<Image>().sprite = levelLabelSprites[1];
            }
            else if((i + 1) % goldPeriod == 0) {
                levelLabel.GetComponent<Image>().sprite = levelLabelSprites[2];
            }
            else {
                levelLabel.GetComponent<Image>().sprite = levelLabelSprites[0];
            }
        }
    }
}
