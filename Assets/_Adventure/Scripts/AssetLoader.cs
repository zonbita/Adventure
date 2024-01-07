using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssetLoader : MonoBehaviour
{
    private void Start()
    {
        LoadScriptObject("ItemData");
    }

    public void LoadScriptObject(string folder)
    {
        ScriptableObject[] scriptableObjects = Resources.LoadAll<ScriptableObject>(folder);

        foreach (ScriptableObject scriptableObject in scriptableObjects)
        {

            GameObject prefab = Resources.Load<GameObject>(folder + scriptableObject.name);

            if (prefab != null)
            {
                GameObject instantiatedObject = Instantiate(prefab);

                //  ItemData
                ItemData itemData = scriptableObject as ItemData;

                if (itemData != null)
                {
                    print(itemData.baseDamage);
                }
                else
                {
                    Debug.LogWarning("ItemData not found on instantiated object: " + instantiatedObject.name);
                }

                Debug.Log("Instantiated object: " + instantiatedObject.name);
            }
            else
            {
                Debug.LogError("Prefab not found for ScriptableObject: " + scriptableObject.name);
            }
        }
    }
}