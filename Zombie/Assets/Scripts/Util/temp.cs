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

// 인터페이스 구현
// 다중 상속은 안되지만, 인터페이스는 여러가지 상속받을 수 있다.
public class Bird: IFlyable, IEatable, IWalkable
{
    // Fly 구현
    public void Fly()
    {
        // 난다~ 날아!
    }

    //이름이 겹칠 경우를 대비하여 사용. IFlyable의 Fly를 구현한다는 의미
    //public void IFlyable.Fly(){}

    public void Eat()
    {
        // 와구 와궁
    }

    public void Walk()
    {
        // 집으로 걸어가고 싶다. 아니 사실 그냥 순간이동 했음 좋겠다
    }
}