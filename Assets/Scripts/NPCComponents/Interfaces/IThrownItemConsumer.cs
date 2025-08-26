//Это получатель удара реагирует на предмет с учётом его свойств

public enum HitOutcome {
    DestroyProjectile, //уничтожаем
    ContinueFlight // пролетает дальше
}

public interface IThrownItemConsumer
{
    HitOutcome OnHitBy(ThrownItemRuntime rt); // реагирует при УДАРЕ, с учётом модификаторов
}
