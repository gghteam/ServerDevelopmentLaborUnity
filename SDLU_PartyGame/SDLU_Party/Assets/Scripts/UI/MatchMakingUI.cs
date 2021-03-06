using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchMakingUI : MonoBehaviour
{
    [SerializeField] private Button btnMatch;
    [SerializeField] private Text text;


    private void Start()
    {
        text.text = MatchMakingManager.Instance.OnMatch ? "메치메이킹 중지하기" : "메치메이킹 시작하기";

        btnMatch.onClick.AddListener(() => {
            MatchMakingManager.Instance.OnMatch = !MatchMakingManager.Instance.OnMatch;
            text.text = MatchMakingManager.Instance.OnMatch ? "메치메이킹 중지하기" : "메치메이킹 시작하기";
            SocketClient.Instance.Send(new DataVO("MatchMaking", JsonUtility.ToJson(new { })));
        });
    }
}
