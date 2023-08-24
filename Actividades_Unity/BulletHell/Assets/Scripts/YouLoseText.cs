using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class YouLose : MonoBehaviour
{
    public TextMeshProUGUI messageText;

    private void Start()
    {
        // Asegurarse de que el mensaje inicial esté vacío
        messageText.text = "";
    }
    public void ShowLoseMessage()
    {
        // Mostrar el mensaje "You Lose"
        messageText.text = "You Lose!";
    }
}
