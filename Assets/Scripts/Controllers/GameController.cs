using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

	MapController _mapController;

	[SerializeField]
	MapView _mapView;

	void Start ()
	{
		_mapController = new MapController ();
		_mapView.Initialize (_mapController);
	}

	void Update ()
	{
		
	}
}
