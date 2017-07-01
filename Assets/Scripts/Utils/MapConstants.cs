using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapConstants : MonoBehaviour
{
	#region Singleton

	private static MapConstants Instance{ get; set; }

	private void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else {
			Destroy (this.gameObject);
		}
	}

	#endregion //Singleton

	#region Editor

	[Header ("Heaxagon")]

	[SerializeField]
	private float _hexWidth = 1;

	#endregion //Editor

	#region Properties

	public static float HexWidth{ get { return Instance._hexWidth; } }

	public static float HexSize{ get { return HexWidth * 0.5f; } }

	public static float HexHeight{ get { return Mathf.Sqrt (3) * 0.5f * HexWidth; } }

	public static Vector2 AxisV{ get { return new Vector2 (0, HexHeight); } }

	public static Vector2 AxisU{ get { return new Vector2 (0.75f * HexWidth, 0.5f * HexHeight); } }

	#endregion //Properties
}
