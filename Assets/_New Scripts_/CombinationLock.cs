using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class CombinationLock : MonoBehaviour
{
    [SerializeField] TMP_Text userInputText;
    [SerializeField] XRButtonInteractable[] comboButtons;
    [SerializeField] bool isLocked;
    [SerializeField] int[] comboValues = new int [3];
    [SerializeField] int[] inputValues;
    private int maxButtonPresses;
    private int buttonPresses;
    
    void Start()
    {
        maxButtonPresses = comboValues.Length;
        inputValues = new int[comboValues.Length];
        userInputText.text = "";
        for (int i=0; i< comboButtons.Length; i++)
        {
            comboButtons[i].selectEntered.AddListener(OnComboButtonPressed);
        }
        
    }

    private void OnComboButtonPressed(SelectEnterEventArgs arg0)
    {
        if(buttonPresses >= maxButtonPresses) //TOO MANY BUTTON PRESS CHECK
        {

        }
        else
        {
        for (int i = 0; i < comboButtons.Length; i++)
        {
            if(arg0.interactableObject.transform.name == comboButtons[i].transform.name)
            {
                userInputText.text = i.ToString();
            }
            else
            {
                comboButtons[1].ResetColor();
            }
        }
        buttonPresses++;
        }
    }

}
