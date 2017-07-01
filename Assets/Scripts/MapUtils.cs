using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Collection of utility methods for the map classes.
/// </summary>
public static class MapUtils
{
	#region Coordinate conversion

	#endregion

	#region Grid relationships

	public static AxialCoordinate ComputeNeighbour (AxialCoordinate coord, eHexDirection dir)
	{
		switch (dir) {
		case eHexDirection.NORTH:
			return new AxialCoordinate (coord.U, coord.V + 1);
		case eHexDirection.NORTHEAST:
			return new AxialCoordinate (coord.U + 1, coord.V + 1);
		case eHexDirection.SOUTHEAST:
			return new AxialCoordinate (coord.U + 1, coord.V);
		case eHexDirection.SOUTH:
			return new AxialCoordinate (coord.U, coord.V - 1);
		case eHexDirection.SOUTHWEST:
			return new AxialCoordinate (coord.U - 1, coord.V - 1);
		case eHexDirection.NORTHWEST:
			return new AxialCoordinate (coord.U - 1, coord.V);
		default:
			Debug.LogError ("Wrong direction!");
			return new AxialCoordinate ();
		}
	}

	public static List<AxialCoordinate> ComputeNeighbours (AxialCoordinate coord)
	{
		List<AxialCoordinate> result = new List<AxialCoordinate> ();

		result.Add (ComputeNeighbour (coord, eHexDirection.NORTH));
		result.Add (ComputeNeighbour (coord, eHexDirection.NORTHEAST));
		result.Add (ComputeNeighbour (coord, eHexDirection.SOUTHEAST));
		result.Add (ComputeNeighbour (coord, eHexDirection.SOUTH));
		result.Add (ComputeNeighbour (coord, eHexDirection.SOUTHWEST));
		result.Add (ComputeNeighbour (coord, eHexDirection.NORTHWEST));

		return result;
	}

	public static EdgeCoordinate ComputeBorder (AxialCoordinate coord, eHexDirection dir)
	{
		switch (dir) {
		case eHexDirection.NORTH:
			return new EdgeCoordinate (coord, eEdgeDirection.NORTH);
		case eHexDirection.NORTHEAST:
			return new EdgeCoordinate (coord, eEdgeDirection.EAST);
		case eHexDirection.SOUTHEAST:
			return new EdgeCoordinate (ComputeNeighbour (coord, eHexDirection.SOUTHEAST), eEdgeDirection.WEST);
		case eHexDirection.SOUTH:
			return new EdgeCoordinate (ComputeNeighbour (coord, eHexDirection.SOUTH), eEdgeDirection.NORTH);
		case eHexDirection.SOUTHWEST:
			return new EdgeCoordinate (ComputeNeighbour (coord, eHexDirection.SOUTHWEST), eEdgeDirection.EAST);
		case eHexDirection.NORTHWEST:
			return new EdgeCoordinate (coord, eEdgeDirection.WEST);
		default:
			Debug.LogError ("Wrong direction!");
			return new EdgeCoordinate ();
		}
	}

	public static List<EdgeCoordinate> ComputeBorders (AxialCoordinate coord)
	{
		List<EdgeCoordinate> result = new List<EdgeCoordinate> ();

		result.Add (ComputeBorder (coord, eHexDirection.NORTH));
		result.Add (ComputeBorder (coord, eHexDirection.NORTHEAST));
		result.Add (ComputeBorder (coord, eHexDirection.SOUTHEAST));
		result.Add (ComputeBorder (coord, eHexDirection.SOUTH));
		result.Add (ComputeBorder (coord, eHexDirection.SOUTHWEST));
		result.Add (ComputeBorder (coord, eHexDirection.NORTHWEST));

		return result;
	}

	public static VerticeCoordinate ComputeCorner (AxialCoordinate coord, eHexDirection dir)
	{
		switch (dir) {
		case eHexDirection.NORTH:
			return new VerticeCoordinate (ComputeNeighbour (coord, eHexDirection.NORTHEAST), eVerticeDirection.LEFT);
		case eHexDirection.NORTHEAST:
			return new VerticeCoordinate (coord, eVerticeDirection.RIGHT);
		case eHexDirection.SOUTHEAST:
			return new VerticeCoordinate (ComputeNeighbour (coord, eHexDirection.SOUTHEAST), eVerticeDirection.LEFT);
		case eHexDirection.SOUTH:
			return new VerticeCoordinate (ComputeNeighbour (coord, eHexDirection.SOUTHWEST), eVerticeDirection.RIGHT);
		case eHexDirection.SOUTHWEST:
			return new VerticeCoordinate (coord, eVerticeDirection.LEFT);
		case eHexDirection.NORTHWEST:
			return new VerticeCoordinate (ComputeNeighbour (coord, eHexDirection.NORTHWEST), eVerticeDirection.RIGHT);
		default:
			Debug.LogError ("Wrong direction!");
			return new VerticeCoordinate ();
		}
	}

	public static List<VerticeCoordinate> ComputeCorners (AxialCoordinate coord)
	{
		List<VerticeCoordinate> result = new List<VerticeCoordinate> ();

		result.Add (ComputeCorner (coord, eHexDirection.NORTH));
		result.Add (ComputeCorner (coord, eHexDirection.NORTHEAST));
		result.Add (ComputeCorner (coord, eHexDirection.SOUTHEAST));
		result.Add (ComputeCorner (coord, eHexDirection.SOUTH));
		result.Add (ComputeCorner (coord, eHexDirection.SOUTHWEST));
		result.Add (ComputeCorner (coord, eHexDirection.NORTHWEST));

		return result;
	}

	public static List<AxialCoordinate> ComputeJoins (EdgeCoordinate coord)
	{
		var result = new List<AxialCoordinate> ();

		switch (coord.D) {
		case eEdgeDirection.NORTH:
			result.Add (new AxialCoordinate (coord.U, coord.V + 1));
			result.Add (new AxialCoordinate (coord.U, coord.V));
			break;
		case eEdgeDirection.EAST:
			result.Add (new AxialCoordinate (coord.U + 1, coord.V));
			result.Add (new AxialCoordinate (coord.U, coord.V));
			break;
		case eEdgeDirection.WEST:
			result.Add (new AxialCoordinate (coord.U, coord.V));
			result.Add (new AxialCoordinate (coord.U - 1, coord.V + 1));
			break;
		}

		return result;
	}

	public static List<VerticeCoordinate> ComputeEndpoints (EdgeCoordinate coord)
	{
		var result = new List<VerticeCoordinate> ();

		switch (coord.D) {
		case eEdgeDirection.NORTH:
			result.Add (new VerticeCoordinate (coord.U, coord.V, eVerticeDirection.LEFT));
			result.Add (new VerticeCoordinate (coord.U, coord.V, eVerticeDirection.LEFT));
			break;
		case eEdgeDirection.EAST:
			result.Add (new VerticeCoordinate (coord.U, coord.V, eVerticeDirection.LEFT));
			result.Add (new VerticeCoordinate (coord.U, coord.V, eVerticeDirection.LEFT));
			break;
		case eEdgeDirection.WEST:
			result.Add (new VerticeCoordinate (coord.U, coord.V, eVerticeDirection.LEFT));
			result.Add (new VerticeCoordinate (coord.U, coord.V, eVerticeDirection.LEFT));
			break;
		}

		return result;
	}

	public static List<AxialCoordinate> ComputeTouches (VerticeCoordinate coord)
	{
		var result = new List<AxialCoordinate> ();

		switch (coord.D) {
		case eVerticeDirection.LEFT:
			result.Add (new AxialCoordinate (coord.U, coord.V));
			result.Add (new AxialCoordinate (coord.U, coord.V));
			result.Add (new AxialCoordinate (coord.U, coord.V));
			break;
		case eVerticeDirection.RIGHT:
			result.Add (new AxialCoordinate (coord.U, coord.V));
			result.Add (new AxialCoordinate (coord.U, coord.V));
			result.Add (new AxialCoordinate (coord.U, coord.V));
			break;
		}

		return result;
	}

	#endregion
}
