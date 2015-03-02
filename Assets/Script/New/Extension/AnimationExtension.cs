using UnityEngine;
using System.Collections;

public static class AnimationExtension{

	public static IEnumerator WhilePlaying( this Animation animation )
    {
        do
        {
            yield return null;
        } while ( animation.isPlaying );
    }

    public static IEnumerator WhilePlaying( this Animation animation,
    string animationName )
    {
        animation.PlayQueued(animationName);
        yield return animation.WhilePlaying();
    }
	
	/*
	public static IEnumerator hideGameObjectWhenAnimIsFinished( this Animation animation ){
		while(animation.isPlaying){
			yield return null;
		}
		animation.gameObject.SetActive(false);
	}
	*/
}
