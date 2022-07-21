public interface IFlyable
{
    void Fly();
}

public interface IEatable
{
    void Eat();
}

public interface IWalkable
{
    void Walk();
}

// �������̽� ����
// ���� ����� �ȵ�����, �������̽��� �������� ��ӹ��� �� �ִ�.
public class Bird: IFlyable, IEatable, IWalkable
{
    // Fly ����
    public void Fly()
    {
        // ����~ ����!
    }

    //�̸��� ��ĥ ��츦 ����Ͽ� ���. IFlyable�� Fly�� �����Ѵٴ� �ǹ�
    //public void IFlyable.Fly(){}

    public void Eat()
    {
        // �ͱ� �ͱ�
    }

    public void Walk()
    {
        // ������ �ɾ�� �ʹ�. �ƴ� ��� �׳� �����̵� ���� ���ڴ�
    }
}