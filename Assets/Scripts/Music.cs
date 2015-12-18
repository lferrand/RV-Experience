using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;



namespace FMODUnity
{
	public class Music : MonoBehaviour {
		
		[EventRef]
		public string Event;
		public float Volume = 1;
		public List<initParam> Param;
		FMOD.Studio.EventInstance _instance; //name the event instance
		FMOD.Studio.ParameterInstance _param; //name the parameter instance
		float intensity=0.1f, timer=-1f, intensityStep;
		string _smoothing;
		bool updateIntensity=false;
		// Use this for initialization
		void Start () {
			_instance = FMODUnity.RuntimeManager.CreateInstance(Event);
			_instance.setVolume (Volume);
			for (int i = 0; i < Param.Count; i++)
			{
				_instance.getParameter (Param[i].name, out _param);
				_param.setValue(Param[i].value);
			}


			_instance.start();
		}
		
		// Update is called once per frame
		void Update () {
			if (updateIntensity) {
				_instance.getParameter (_smoothing, out _param);
				timer -= Time.deltaTime;
				intensity += Time.deltaTime * intensityStep;
				_param.setValue(intensity); //set the LevelIntensity value to x
				if (timer <= 0)
					updateIntensity = false;
			}
		}

		public void SetParam(float lvl, string Param){
			_instance.getParameter (Param, out _param);
			_param.setValue(lvl);
		}

		
		public void SetParam(float lvl, float time, string Param){
			_instance.getParameter (_smoothing, out _param);
			_smoothing = Param;
			_param.getValue (out intensity);
			timer = time;
			intensityStep = (lvl - intensity) / time;
			updateIntensity = true;
		}
	}
}