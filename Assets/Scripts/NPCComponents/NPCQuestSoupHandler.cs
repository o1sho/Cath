using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCQuestSoupHandler : MonoBehaviour, INPCComponent
{
    private NPC _npc;

    [Header("��������� �������")]
    [SerializeField] private List<ThrowableItemData> acceptedIngredients;
    [SerializeField] private int neededCount = 2;

    [Header("����-�������")]
    [SerializeField] private float timerBeforeStart = 10f;
    [SerializeField] private string nextSceneName = "";

    private int _currentCount = 0;
    private bool _locked = false;


    //---------------
    public void Init(NPC npc) {
        _npc = npc;
    }
    //---------------

    public void IngredientHasBeenAdded(IThrowableItem item) {
        if (_locked) return;

        var data = item as ThrowableItemData;
        if (data == null || !acceptedIngredients.Contains(data)) {
            Debug.Log("����� ������ ����������.");
            return;
        }

        _currentCount++;
        Debug.Log($"������������ � �����: {_currentCount}/{neededCount}");

        if (_currentCount >= neededCount) {
            _locked = true;
            StartCoroutine(FinaleRoutine());
        }
    }

    private IEnumerator FinaleRoutine() {
        // 1. ������� ������
        yield return new WaitForSeconds(timerBeforeStart);

        // 2. ������
        // WeatherManager.Instance.SetStorm(true);

        // 3. ��������� ���������� ������
        // Player.Instance.SomeStat.AddDebuff(...);

        // 4. ����������
        //if (ScreenFader.Instance != null)
        //    yield return ScreenFader.Instance.FadeOut(1f);

        // 5. ����� ����
        if (!string.IsNullOrEmpty(nextSceneName)) {
            SceneManager.LoadScene(nextSceneName);
        }
        //else if (teleportTarget != null) {
        //    Player.Instance.transform.position = teleportTarget.position;
        //}

        // 6. ����-�� ����� ��������/���������
        //if (ScreenFader.Instance != null)
        //    yield return ScreenFader.Instance.FadeIn(0.8f);
    }
}
