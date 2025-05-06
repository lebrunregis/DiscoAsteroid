public class BigAsteroid : AsteroidController
{
    public override void OnMouseDown()
    {
        base.OnMouseDown();
        base.SpawnAsteroid();
        base.SpawnAsteroid();
    }


}
