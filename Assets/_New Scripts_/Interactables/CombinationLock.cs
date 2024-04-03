using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events; 

public class CombinationLock : MonoBehaviour
{   
    public UnityAction UnlockAction;
    private void OnUnlocked() => UnlockAction?.Invoke();
    public UnityAction LockAction;
    private void OnLocked() => LockAction?.Invoke();  
    public UnityAction ComboButtonPressed;
    private void OnComboButtonPress() => ComboButtonPressed?.Invoke();
    [SerializeField] TMP_Text userInputText;
    [SerializeField] XRButtonInteractable[] comboButtons;
    [SerializeField] TMP_Text infoText;
    private const string Start_String = "Enter 3 Digit Combo";
    private const string Reset_String = "Enter 3 Digits To Reset Combo";
    [SerializeField] Image lockedPanel;
    [SerializeField] Color unlockedColor;
    [SerializeField] Color lockedColor;
    [SerializeField] TMP_Text lockedText;
    private const string Unlocked_String = "Unlocked";
    private const string Locked_String = "Locked";
    [SerializeField] bool isLocked;
    [SerializeField] bool isResettable;
    private bool resetCombo;
    [SerializeField] int[] comboValues = new int[3];
    [SerializeField] int[] inputValues;
    [SerializeField] AudioClip lockComboClip;
    public AudioClip GetLockClip => lockComboClip;
    [SerializeField] AudioClip unlockComboClip;
    public AudioClip GetUnlockClip => unlockComboClip;
    [SerializeField] AudioClip comboButtonPressedClip;
    public AudioClip GetComboPressedClip => comboButtonPressedClip;
    private int maxButtonPresses;
    private int buttonPresses;
    void Start()
    {
        maxButtonPresses = comboValues.Length;
        inputValues = new int[comboValues.Length];
        userInputText.text = "";
        for (int i = 0; i < comboButtons.Length; i++)
        {
            comboButtons[i].selectEntered.AddListener(OnComboButtonPressed);
        }
    }
    private void OnComboButtonPressed(SelectEnterEventArgs arg0)
    {
        if (buttonPresses >= maxButtonPresses)
        {
            //TOO MANY BUTTON PRESSES
        }
        else
        {
            for (int i = 0; i < comboButtons.Length; i++)
            {
                if (arg0.interactableObject.transform.name == comboButtons[i].transform.name)
                {
                    userInputText.text += i.ToString();
                    inputValues[buttonPresses] = i;
                }
                else
                {
                    comboButtons[i].ResetColor();
                }
            }
            buttonPresses++;
            if (buttonPresses == maxButtonPresses)
            {
                //CHECK COMBO
                CheckCombo();
            }
            else
            {
                OnComboButtonPress();
            }
        }
    }
    private void CheckCombo()
    {
        if (resetCombo)
        {
            resetCombo = false;
            LockCombo();
            return;
        }
        int matches = 0;

        for (int i = 0; i < maxButtonPresses; i++)
        {
            if (inputValues[i] == comboValues[i])
            {
                matches++;
            }
        }
        if (matches == maxButtonPresses)
        {
            UnlockCombo();
        }
        else
        {
            ResetUserValues();
        }
    }
    private void UnlockCombo()
    {
        isLocked = false;
        OnUnlocked();
        lockedPanel.color = unlockedColor;
        lockedText.text = Unlocked_String;
        if (isResettable)
        {
            ResetCombo();
        }
    }
    private void LockCombo()
    {
        isLocked = true;
        OnLocked();
        lockedPanel.color = lockedColor;
        lockedText.text = Locked_String;
        infoText.text = Start_String;
        for (int i = 0; i < maxButtonPresses; i++)
        {
            comboValues[i] = inputValues[i];
        }
        ResetUserValues();
    }
    private void ResetCombo()
    {
        infoText.text = Reset_String;
        ResetUserValues();
        resetCombo = true;
    }
    private void ResetUserValues()
    {   
        if(isLocked)
        {
            OnLocked();
        }     
        inputValues = new int[maxButtonPresses];
        userInputText.text = "";
        buttonPresses = 0;
    }
}
