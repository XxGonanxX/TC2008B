using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeveloperText : MonoBehaviour
{
     public TextMeshProUGUI messageText;

    private void Start()
    {
        // Asegurarse de que el mensaje inicial esté vacío
        messageText.text = "";
    }

    public void ShowDeveloperMessage()
    {
        // Mostrar el mensaje "You Win"
        messageText.text = "Alan Patricio González Bernal -A01067546";
    }
}
