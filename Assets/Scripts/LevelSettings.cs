using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "SpinWheel/LevelSettings")]
public class LevelSettings : ScriptableObject
{
    [Serializable]
    public struct SlotValue {
        public ItemType itemtype;
        public int amount;
    }

    const int size = 8;
    [SerializeField] private int frequency;


    [SerializeField] private GameObject _prefab;

    public GameObject Prefab { get {return _prefab;}}


    [SerializeField] private SlotValue[] _rewards = new SlotValue[size];

    public SlotValue[] Rewards { get {return _rewards;}}
    

    void OnValidate()
    {
        if (_rewards.Length != size) {
            Debug.LogWarning("Don't change the rewards array size!");
            Array.Resize(ref _rewards, size);
        }

        if (frequency < 1) {
            Debug.LogWarning("Frequency value must be positive");
            frequency = 1;
        }

        for(int i = 0; i < _rewards.Length; i++) {
            if(_rewards[i].amount < 1) {
                _rewards[i].amount = 1;
            }
        }
    }
}