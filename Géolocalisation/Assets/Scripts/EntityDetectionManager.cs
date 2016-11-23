using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EntityDetectionManager : MonoBehaviour
{
	public float[] _steps = new float[4];

	public int[] _ZoneCounterE = new int[3];
	public int[] _ZoneCounterA = new int[3];

	private int _Seconds;
	private int _Minutes;
	private float counter;
	

	public int _DeathTimer = 15;

	public List<GameObject> _ClosestEnities = new List<GameObject>();
	
	public GameObject _Avatar;
	public GameObject _EntitiesContainer;

	public bool _FollowAvatar;
	public bool _Loosed;


	public Text _NumberEntitiesText;
	public Text _DeathTimerText;
	public Text _StatusText;

	public Slider _DeathSlider;

	public Animator _DeathSignal;

	// Use this for initialization
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{

		DetectionEntities();
		DeathTimer();
		_DeathTimerText.text = _Minutes + " : " + _Seconds;

		if (_FollowAvatar)
			_Avatar.transform.position = GPSTools.toLocalCoordinates(Input.location.lastData.latitude, Input.location.lastData.longitude) - Vector3.forward;
		else
			Camera.main.transform.position = GPSTools.toLocalCoordinates(Input.location.lastData.latitude, Input.location.lastData.longitude) - Vector3.forward;
			

		_NumberEntitiesText.text = "E1 : " + _ZoneCounterE[0] + " -- A1 : " + _ZoneCounterA[0] +
									"\nE2 : " + _ZoneCounterE[1] + " -- A2 : " + _ZoneCounterA[1] +
									"\nE3 : " + _ZoneCounterE[2] + " -- A3 : " + _ZoneCounterA[2] +
									"\nLattitude : " + Input.location.lastData.latitude +
									"\nLongitude : " + Input.location.lastData.longitude +
									"\nXpos : " + _Avatar.transform.position.x +
									"\nYpos : " + _Avatar.transform.position.y;

	
	}

	void DeathTimer()
	{
		if (_DeathSlider.value < 30)
		{
			counter += Time.deltaTime;
			_DeathSignal.SetBool("Loosing", true);
			if (!_Loosed)
			{
				if (counter >= 1)
				{
					_Seconds--;
					counter = 0;
				}
				if (_Seconds < 0)
				{
					_Minutes--;
					if (_Minutes < 0)
					{
						_StatusText.text = "Mort";
						_Loosed = true;
						StartCoroutine(ResetState());
						_Minutes = 0;
						_Seconds = 0;
					}
					else
					{
						_Seconds = 60;
					}
				}
			}
			
		}
		else
		{
			_Seconds = _DeathTimer;
			_Minutes = Mathf.RoundToInt((float)_Seconds / 60.0f);
			_Seconds -= 60 * _Minutes;
			_DeathSignal.SetBool("Loosing", false);
		}
	}

	public void ClearInfos()
	{
		_ZoneCounterE[0] = 0;
		_ZoneCounterE[1] = 0;
		_ZoneCounterE[2] = 0;
		_ZoneCounterA[0] = 0;
		_ZoneCounterA[1] = 0;
		_ZoneCounterA[2] = 0;
	}

	private IEnumerator ResetState()
	{
		yield return new WaitForSeconds(5.0f);
		_StatusText.text = "Vivant";
		_Seconds = _DeathTimer;
		_Minutes = Mathf.RoundToInt((float)_Seconds / 60.0f);
		_Seconds -= 60 * _Minutes;
		_DeathSignal.SetBool("Loosing", false);
		_Loosed = false;
	}

	public void DetectionEntities()
	{
		ClearInfos();
		
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll(new Vector2(_Avatar.transform.position.x, _Avatar.transform.position.y) , _steps[3]);
		foreach (var entity in hitColliders)
		{
			if (entity.tag == "Ennemi"|| entity.tag == "Ally")
			{
				if (Vector3.Distance(entity.transform.position, _Avatar.transform.position) >= _steps[0] &&
					Vector3.Distance(entity.transform.position, _Avatar.transform.position) < _steps[1])
				{
					if (entity.tag == "Ennemi")
					{
						_ZoneCounterE[0]++;
					}
					else if (entity.tag == "Ally")
					{
						_ZoneCounterA[0]++;
					}
				}
				else if (Vector3.Distance(entity.transform.position, _Avatar.transform.position) >= _steps[1] &&
						Vector3.Distance(entity.transform.position, _Avatar.transform.position) < _steps[2])
				{
					if (entity.tag == "Ennemi")
					{
						_ZoneCounterE[1]++;
					}
					else if (entity.tag == "Ally")
					{
						_ZoneCounterA[1]++;
					}
				}
				else if (Vector3.Distance(entity.transform.position, _Avatar.transform.position) >= _steps[2] &&
					Vector3.Distance(entity.transform.position, _Avatar.transform.position) < _steps[3])
				{
					if (entity.tag == "Ennemi")
					{
						_ZoneCounterE[2]++;
					}
					else if (entity.tag == "Ally")
					{
						_ZoneCounterA[2]++;
					}
				}
			}
		}
	}
}
