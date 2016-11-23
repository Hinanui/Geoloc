using UnityEngine;
using System.Collections;
using System.Xml.Schema;

public class Spawner : MonoBehaviour
{

	public GameObject _PrefabEntity;

	public float _NumberOfEntities;
	public float _XMax;
	public float _YMax;

	public bool random = false;

	// Use this for initialization
	void Start ()
	{
		if (random)
		{
			for (int i = 0; i < _NumberOfEntities; i++)
			{
				GameObject Entity = Instantiate(_PrefabEntity) as GameObject;
				Entity.transform.position = new Vector3(Random.Range(-_XMax/10, _XMax/10), Random.Range(-_YMax/10, _YMax/10), 1.0f);
				Entity.transform.parent = this.gameObject.transform;
				Color32 randomcolor = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
				Entity.GetComponent<SpriteRenderer>().material.color = randomcolor;

				int random0 = Random.Range(1, 3);
				if (random0 == 1)
					Entity.tag = "Ennemi";
				else
					Entity.tag = "Ally";
			}
			for (int i = 0; i < _NumberOfEntities; i++)
			{
				GameObject Entity = Instantiate(_PrefabEntity) as GameObject;
				Entity.transform.position = new Vector3(Random.Range(-_XMax, _XMax), Random.Range(-_YMax, _YMax), 1.0f);
				Entity.transform.parent = this.gameObject.transform;
				Color32 randomcolor = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
				Entity.GetComponent<SpriteRenderer>().material.color = randomcolor;

				int random1 = Random.Range(1, 3);
				if (random1 == 1)
					Entity.tag = "Ennemi";
				else
					Entity.tag = "Ally";
			}
		}
		else
		{
			for (int i = 0; i < _NumberOfEntities; i++)
			{
				GameObject Entity = Instantiate(_PrefabEntity) as GameObject;
				Entity.transform.position = new Vector3(i-4, i+2);
				Entity.transform.parent = this.gameObject.transform;
				Color32 randomcolor = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
				Entity.GetComponent<SpriteRenderer>().material.color = randomcolor;

				int random2 = Random.Range(1, 3);
				if (random2 == 1)
					Entity.tag = "Ennemi";
				else
					Entity.tag = "Ally";
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
