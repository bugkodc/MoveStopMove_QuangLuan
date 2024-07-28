using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache : MonoBehaviour
{
    private static Dictionary<Collider, Character> characters = new Dictionary<Collider, Character>();
    private static Dictionary<Collider, IHit> iHits = new Dictionary<Collider, IHit>();
  
    
    public static Character GetCharacter(Collider collider)
    {
        if (!characters.ContainsKey(collider))
        {
            characters.Add(collider, collider.GetComponent<Character>());
        }

        return characters[collider];
    } 

    public static IHit GetIHit(Collider collider)
    {
        if (!iHits.ContainsKey(collider))
        {
            iHits.Add(collider, collider.GetComponent<Character>());
        }

        return iHits[collider];
    }

    

    

}
