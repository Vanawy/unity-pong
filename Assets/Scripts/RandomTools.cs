using UnityEngine;

namespace App {
    public static class RandomTools {
        public static T Choose<T>(params T[] options)
        {
            return options[Random.Range(0, options.Length)]; 
        }  
    }
}