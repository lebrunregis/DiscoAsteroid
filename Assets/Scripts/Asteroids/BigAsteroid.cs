using UnityEngine;

public class BigAsteroid : AsteroidBase
{
	public override void OnDestroy()
	{
		base.OnDestroy();
		base.SpawnAsteroid();
		base.SpawnAsteroid();
	}
	
	
}
