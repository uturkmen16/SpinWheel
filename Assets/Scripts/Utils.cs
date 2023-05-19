using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace SpinWheel {
    public static class Utils {

        static UtilsSettings _utilsSettings;

        static public UtilsSettings UtilsSettings {
            get {return _utilsSettings;}
            set {_utilsSettings = value;}
        }
        //Takes integer value and shortens it with string values instead of zeroes
        public static string ShortenInteger(int value) {
            List<ThresholdValue> thresholdValues = _utilsSettings.ThresholdValues;
            thresholdValues = thresholdValues.OrderByDescending(obj => obj.threshold).ToList();
            for(int i = 0; i < thresholdValues.Count; i++) {
                int threshold = thresholdValues[i].threshold;
                if(value >= threshold) {
                    return (value /= threshold).ToString() + thresholdValues[i].notation;
                }
            }
            return value.ToString();
        }
    }
}
