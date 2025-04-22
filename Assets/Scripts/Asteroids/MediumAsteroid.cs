using UnityEngine;

public class MediumAsteroid : AsteroidBase
{
	public override void OnDestroy()
	{
		base.OnDestroy();
		base.SpawnAsteroid();
		base.SpawnAsteroid();
	}
}
