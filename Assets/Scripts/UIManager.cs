using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private List<LevelSettings> levelSettings;
    [SerializeField]
    private int silverPeriod;
    [SerializeField]
    private int goldPeriod;
    [SerializeField]
    private GameObject bronzePrefab;
    [SerializeField]
    private GameObject silverPrefab;
    [SerializeField]
    private GameObject goldPrefab;

    
    private GameObject currentLevelInstance;

    //Level number that increases each level starting from level 1
    private int currentLevelNo = 1;

    private Inventory rewardsInventory = new Inventory();
    void Start() {

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

        Debug.Log("Loading" + levelNo);

        levelSettings[levelNo - 1].ShuffleRewards();

        if(levelNo % goldPeriod == 0) currentLevelInstance = Instantiate(goldPrefab, transform);
        else if(levelNo % silverPeriod == 0) currentLevelInstance = Instantiate(silverPrefab, transform);
        else currentLevelInstance = Instantiate(bronzePrefab, transform);

        Transform baseTransform = currentLevelInstance.transform.GetChild(0);

        for(int i = 0; i < baseTransform.childCount; i++) {
            Transform childTransform = baseTransform.GetChild(i);
            childTransform.GetComponentInChildren<Image>().sprite = levelSettings[levelNo - 1].Rewards[i].SlotItem.ItemIcon;
            int amount = levelSettings[levelNo - 1].Rewards[i].Amount;
            string suffix = "";
            if(amount > 999999) {
                //Million
                amount /= 1000000;
                suffix = "M";
            }
            else if(amount > 999) {
                //Thousand
                amount /= 1000;
                suffix = "K";
            }
            childTransform.GetComponentInChildren<TextMeshProUGUI>().text = "x" + amount.ToString() + suffix;
        }

        //Add spin button callback
        currentLevelInstance.GetComponentInChildren<Button>().onClick.AddListener(SpinWheel);
    }

    private void SpinAnimationOverCallback() {
        Destroy(currentLevelInstance);
        if(currentLevelNo < levelSettings.Count) {
            currentLevelNo++;
            InitLevel(currentLevelNo);
        }
        else {
            Debug.Log("There are no more levels!");
        }
    }

    public void SpinWheel() {
        int randomInt = UnityEngine.Random.Range(levelSettings[currentLevelNo].MinimumRoll, levelSettings[currentLevelNo].MaximumRoll);
        Debug.Log("You've earned " + levelSettings[currentLevelNo].Rewards[randomInt % 8].Amount + " amount of item " + levelSettings[currentLevelNo].Rewards[randomInt % 8].SlotItem.ItemName);
        rewardsInventory.AddItem(levelSettings[currentLevelNo].Rewards[randomInt % 8]);
        for(int i = 0; i < rewardsInventory.InventoryLength; i++) {
            Debug.Log(rewardsInventory.InventoryList[i].SlotItem.ItemName + " : " + rewardsInventory.InventoryList[i].Amount);
        }
        currentLevelInstance.transform.GetChild(0).transform.DORotate(new Vector3(0, 0, randomInt * 45), 1 + randomInt / 5, RotateMode.LocalAxisAdd)
        .OnComplete(SpinAnimationOverCallback);

        //Change this from being hardcoded
        transform.GetChild(2).GetComponentInChildren<Button>().interactable = false;
    }
}
