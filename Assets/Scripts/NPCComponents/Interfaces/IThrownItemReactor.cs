//Это реактор пролёта меняет предмет (не останавливает его)

public interface IThrownItemReactor
{
    void OnThrownItemPassed(ThrownItemRuntime rt); // изменяет предмет на ПРОЛЁТЕ
}
