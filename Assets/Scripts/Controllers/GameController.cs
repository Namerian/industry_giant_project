using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	//######################################################################

	#region Editor

	[Header ("Scene Structure")]

	[SerializeField]
	Transform _viewHolderTransform;

	#endregion Editor

	//######################################################################
	//######################################################################

	#region Variables

	Model _model;

	MapView _mapView;

	#endregion Variables

	//######################################################################
	//######################################################################

	#region MonoBehaviour Methods

	void Start ()
	{
		StartCoroutine (StartGame ());
	}

	void Update ()
	{
		
	}

	#endregion MonoBehaviour Methods

	//######################################################################
	//######################################################################

	IEnumerator StartGame ()
	{
		_model = new Model ();

		var mapViewGo = Instantiate<GameObject> (Resources.Load<GameObject> ("Prefabs/MapView"), _viewHolderTransform);
		_mapView = mapViewGo.GetComponent<MapView> ();
		_mapView.Initialize (_model);

		yield return null;

		_model.CreateHexagonMap (3);
	}
}
