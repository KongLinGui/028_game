//#define ITWEEN


using UnityEngine;


using System.Collections;
namespace InaneGames {

public class FlyToPortal : MonoBehaviour {
	public GameObject fireWorks;
	// Use this for initialization
	void Start () {
		Teleporter teleporter = getClosestTeleporter();
		if(teleporter)
		{
			StartCoroutine(flyToTeleport(transform.position,teleporter.transform.position));
		}
	}
	void destroy()
	{
		BaseGameManager.suckedIntoPortal(gameObject);


		Destroy(gameObject);
		
	}
	public IEnumerator flyToTeleport(Vector3 startPos,
	                                 Vector3 endPos)
	{
		Vector3 midPos =Vector3.Lerp(startPos, endPos,0.5f);
		midPos.y += 120;
		Vector3[] path = new Vector3[3];
		path[0] = startPos;
		path[1] = midPos;
		path[2] = endPos;

		yield return new WaitForSeconds(2);
#if ITWEEN
		iTween.MoveTo(gameObject,iTween.Hash ("path",path,
		                                      "speed",280,
		                                      "oncomplete","destroy",
		                                      "oncompletetarget",gameObject,
		                                      "easeType",iTween.EaseType.linear));
#endif
		yield return new WaitForSeconds(.5f);

		Rotator rot = gameObject.AddComponent<Rotator>();

		if(rot){
			rot.rotationSpeed = 555;
			rot.upVec = new Vector3(1,1,1);
		}
	}
	Teleporter getClosestTeleporter()
	{
		Teleporter target = null;
		float d0 = Mathf.Infinity;
		Teleporter[] teleporters = (Teleporter[])GameObject.FindObjectsOfType(typeof(Teleporter));
		
		
		
		for(int i=0; i<teleporters.Length; i++)
		{
			float d1 = (teleporters[i].transform.position - transform.position).magnitude;
				
			if(d1<d0)
			{
				target =  teleporters[i];
				d0 = d1;
			}
		}
		return target;
	}


}
}