using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FeedBacksManager : MonoBehaviour
{
	public EntityDetectionManager Detector;

	public Slider[] _Sliders = new Slider[3];

	public float _StrongValue = 1.0f;
	public float _WeakValue = 1.0f;

	// Use this for initialization
	void Start ()
	{
		Detector = GetComponent<EntityDetectionManager>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		for (int i = 0; i < 3; i++)
		{
			float total = Detector._ZoneCounterE[i]*_WeakValue + Detector._ZoneCounterA[i]*_StrongValue;
			_Sliders[i].value = (Detector._ZoneCounterA[i]*_StrongValue / total)*100;
        }
	}
}
