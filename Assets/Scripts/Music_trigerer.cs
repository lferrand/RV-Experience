﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;


namespace FMODUnity
{
	public class Music_trigerer : MonoBehaviour {
		public List<initParam> Param;
		public float smoothTime;
		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}
		
		void OnTriggerEnter(Collider other){
			print ("triggered");
			if (other.gameObject.tag == "Player")
			{

				foreach (initParam p in Param){
					Component[] musicList=other.gameObject.GetComponents<Music>();
					foreach (Music m in musicList){
						if (!p.smooth) m.SetParam(p.value, p.name);
						if (p.smooth) m.SetParam(p.value,smoothTime, p.name);
					}
					/*
				if (!p.smooth) other.gameObject.GetComponent<Music>().SetParam(p.value, p.name);
				if (p.smooth) other.gameObject.GetComponent<Music>().SetParam(p.value,smoothTime, p.name);
				*/
				}
			}	
		}
	}
}