//��� ���������� ����� ��������� �� ������� � ������ ��� �������

public enum HitOutcome {
    DestroyProjectile, //����������
    ContinueFlight // ��������� ������
}

public interface IThrownItemConsumer
{
    HitOutcome OnHitBy(ThrownItemRuntime rt); // ��������� ��� �����, � ������ �������������
}
