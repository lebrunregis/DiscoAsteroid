public class MediumAsteroid : AsteroidController
{
    public override void OnMouseDown()
    {
        base.OnMouseDown();
        base.SpawnAsteroid();
        base.SpawnAsteroid();
    }
}
