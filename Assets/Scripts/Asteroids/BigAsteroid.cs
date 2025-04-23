using UnityEngine;

public class BigAsteroid : AsteroidBase
{
	public override void OnMouseDown()
	{
		base.OnMouseDown();
		base.SpawnAsteroid();
		base.SpawnAsteroid();
	}
	
	
}
