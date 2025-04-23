using UnityEngine;

public class MediumAsteroid : AsteroidBase
{
	public override void OnMouseDown()
	{
		base.OnMouseDown();
		base.SpawnAsteroid();
		base.SpawnAsteroid();
	}
}
