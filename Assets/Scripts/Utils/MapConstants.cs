using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapConstants : MonoBehaviour
{
	#region Singleton

	static MapConstants Instance{ get; set; }

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else {
			Destroy (this.gameObject);
		}
	}

	#endregion Singleton

	#region Editor

	[Header ("Heaxagon")]

	[SerializeField]
	float _hexWidth = 2;

	#endregion Editor

	#region Properties

	public static float HexWidth{ get { return Instance._hexWidth; } }

	/// <summary>
	/// The half-width of a hex.
	/// </summary>
	/// <value>The size of the hex.</value>
	public static float HexSize{ get { return HexWidth * 0.5f; } }

	public static float HexHeight{ get { return 0.866025404f /* hardcoded sqrt(3)/2 */ * HexWidth; } }

	public static Vector2 AxisV{ get { return new Vector2 (0, HexHeight); } }

	public static Vector2 AxisU{ get { return new Vector2 (0.75f * HexWidth, -0.5f * HexHeight); } }

	#endregion Properties
}
