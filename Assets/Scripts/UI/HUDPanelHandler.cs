using TMPro;
using UnityEngine;

public class HUDPanelHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private void Update() {
        timerText.text = PlayerSpawnController.GetRespawnTimeInSeconds().ToString();
    }
}
