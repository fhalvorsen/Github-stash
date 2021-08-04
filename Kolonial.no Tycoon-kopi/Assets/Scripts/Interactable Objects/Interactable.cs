using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Interactable : MonoBehaviour 
{
    public Dictionary<Character, bool> CharactersThatFocus = new Dictionary<Character, bool>();
    public float radius = 3f;
    public Transform interactionTransform;


    private void Update()
    {   
        if(CharactersThatFocus.Count < 0)
        {
            return;
        }
        foreach (Character character in CharactersThatFocus.Keys)
        {
            
            if(!CharactersThatFocus[character])
            {
                float distance = Vector3.Distance(character.transform.position, interactionTransform.position);
                if(distance <= radius)
                {
                    CharactersThatFocus[character] = true;
                    Debug.Log(character.characterData.name + " interacting with " + gameObject.name);
                    return;
                }
            }
        }
    }

    public bool HasInteractedWith(Character character)
    {
        return CharactersThatFocus[character];
    }

    public virtual void OnFocused(Character character)
    {
        if(!CharactersThatFocus.ContainsKey(character))
            CharactersThatFocus.Add(character, false);
    }

    public virtual void OnDefocused(Character character)
    {
        CharactersThatFocus.Remove (character);
    }

    private void OnDrawGizmosSelected()
    {
        if(interactionTransform == null)
            interactionTransform = transform;
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
