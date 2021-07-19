using UnityEngine;

public class AttackHandler : MonoBehaviour, IBufHandler
{
    // �� �Լ��� ����Ƽ �����忡�� ȣ��Ǵ� ���� �ƴ� WebSocket �����忡�� ȣ��˴ϴ�.
    public void HandleBuffer(string payload)
    {
        AttackVO vo = JsonUtility.FromJson<AttackVO>(payload);

        // ��ǥ�� CharactorBase �� ���� ���� ��ǥ�� OnSkillHit �� ȣ���մϴ�.
        UserManager.GetPlayerBase(vo.target)?.OnSkillHit(vo.skillEnum);
    }
}