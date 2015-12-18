using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;

namespace FMODUnity
{
	public class SoundGrid : MonoBehaviour {
		[EventRef]
		public string Event;
		public float xmin, xmax, ymin, ymax,z, bandwith;
		FMOD.ATTRIBUTES_3D loc;
		List<FMOD.Studio.EventInstance> _instances;
		// Use this for initialization
		void Start () {
			for (int i = 0; i < (int) (xmax-xmin)/bandwith; i++)
			{
				for (int j = 0;j<(int) (ymax-ymin)/bandwith;j++){
					print ("here");
					loc.position.x = xmin+i*bandwith;
					loc.position.y = ymin+j*bandwith;
					loc.position.z=z;


					_instances.Add(FMODUnity.RuntimeManager.CreateInstance(Event));
					_instances[_instances.Count - 1].set3DAttributes(loc);
					_instances[_instances.Count - 1].start();
				}
			}
				                             
		}
				                              
				                              // Update is called once per frame
		void Update () {
					
		}
	}
}
