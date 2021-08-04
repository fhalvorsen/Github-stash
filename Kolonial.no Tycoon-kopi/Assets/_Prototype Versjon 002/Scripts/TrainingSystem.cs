using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingSystem {

	// Øker motivasjons stat
	public IEnumerator IncreaseMotivation(CharacterData character, int amount)
	{
        Debug.Log("Runnning coroutine..");
        yield return new WaitForSeconds(5);
		character.motivationStat += amount;

		// Passer på at stats ikke kan gå over en limit på 100
		if (character.motivationStat > 100)
		{
			character.motivationStat = 100;
		}
	}

	// Øker og trener speed stat
	public IEnumerator TrainSpeed(CharacterData character, int amount)
	{
        Debug.Log("Runnning coroutine..");
        yield return new WaitForSeconds(10);
        character.speedStat += amount;

		if (character.speedStat > 100)
		{
			character.speedStat = 100;
		}
	}

	// Øker og trener research stat
	public IEnumerator TrainResearch(CharacterData character, int amount)
	{
        Debug.Log("Runnning coroutine..");
        yield return new WaitForSeconds(10);
        character.researchStat += amount;

		if (character.researchStat > 100)
		{
			character.researchStat = 100;
		}
	}

	// Øker og trener quality stat
	public IEnumerator TrainQuality(CharacterData character, int amount)
	{
        Debug.Log("Runnning coroutine..");
        yield return new WaitForSeconds(10);
        character.qualityStat += amount;

		if (character.qualityStat > 100)
		{
			character.qualityStat = 100;
		}
	}

	// Øker og trener design stat
	public IEnumerator TrainDesign(CharacterData character, int amount)
	{
        Debug.Log("Runnning coroutine..");
        yield return new WaitForSeconds(10);
        character.designStat += amount;

		if (character.designStat > 100)
		{
			character.designStat = 100;
		}
	}
}
