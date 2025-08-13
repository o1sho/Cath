//Это получатель удара реагирует на предмет с учётом его свойств

public interface IThrownItemConsumer
{
    void OnHitBy(ThrownItemRuntime rt); // реагирует при УДАРЕ, с учётом модификаторов
}
