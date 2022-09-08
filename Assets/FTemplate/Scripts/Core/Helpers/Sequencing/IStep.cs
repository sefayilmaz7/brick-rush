
public interface IStep
{
    bool IsComplete { get; set; }

    void Start();
    void Tick(float deltaTime);
}
