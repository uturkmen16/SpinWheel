using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpinWheel {
    public static class Utils {
        //Takes integer value and returns string using K for thousands and M for millions
        public static string AbbreviateInteger(int val) {
            string suffix = "";
            if(val > 999999) {
                //Million
                val /= 1000000;
                suffix = "M";
            }
            else if(val > 999) {
                //Thousand
                val /= 1000;
                suffix = "K";
            }
            return val.ToString() + suffix;
        }
    }
}
