using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class YouWin : MonoBehaviour
{
    public TextMeshProUGUI messageText;

    private void Start()
    {
        // Asegurarse de que el mensaje inicial esté vacío
        messageText.text = "";
    }

    public void ShowWinMessage()
    {
        // Mostrar el mensaje "You Win"
        messageText.text = "You Win!";
    }
}
