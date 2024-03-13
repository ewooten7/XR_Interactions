using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CombinationLock : MonoBehaviour
{
    [SerializeField] XRButtonInteractable[] comboButtons;
    void Start()
    {
        for (int i=0; i< comboButtons.Length; i++)
        {
            comboButtons[i].selectEntered.AddListener(OnComboButtonPressed);
        }
        
    }

    private void OnComboButtonPressed(SelectEnterEventArgs arg0)
    {
        for (int i = 0; i < comboButtons.Length; i++)
        {
            if(arg0.interactableObject.transform.name == comboButtons[i].transform.name)
            {
                
            }
        }
    }

    void Update()
    {
        
    }
}
