using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = System.Random;

public static class ExtensionMethods 
{
    
    public static Random random = new Random();  
    
    public static T Random<T>(this IEnumerable<T> list)  
    {  
        return list.ToArray().Random();  
    }  
  
    public static T Random<T>(this T[] array)  
    {  
        return array[random.Next(0, array.Length)];  
    }
}
