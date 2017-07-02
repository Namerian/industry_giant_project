using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Collection of utility methods for the map classes.
/// </summary>
public static class MapUtils
{
	#region Coordinate conversion

	public static AxialCoordinate ConvertCubeToAxial (CubeCoordinate cube)
	{
		int u = cube.X;
		int v = cube.Z;
		return new AxialCoordinate (u, v);
	}

	public static CubeCoordinate ConvertAxialToCube (AxialCoordinate axial)
	{
		int x = axial.U;
		int z = axial.V;
		int y = -x - z;
		return new CubeCoordinate (x, y, z);
	}

	public static OffsetCoordinate ConvertCubeToOffset (CubeCoordinate cube)
	{
		int col = cube.X;
		int row = -cube.Z + (cube.X - (cube.X & 1)) / 2;
		return new OffsetCoordinate (col, row);
	}

	public static CubeCoordinate ConvertOffsetToCube (OffsetCoordinate offset)
	{
		int x = offset.Col;
		int z = -offset.Row + (offset.Col - (offset.Col & 1)) / 2;
		int y = -x - z;
		return new CubeCoordinate (x, y, z);
	}

	public static AxialCoordinate ConvertOffsetToAxial (OffsetCoordinate offset)
	{
		return ConvertCubeToAxial (ConvertOffsetToCube (offset));
	}

	public static OffsetCoordinate ConvertAxialToOffset (AxialCoordinate axial)
	{
		return ConvertCubeToOffset (ConvertAxialToCube (axial));
	}

	public static Vector3 ConvertAxialToPixel (AxialCoordinate axial)
	{
		var vec2 = axial.U * MapConstants.AxisU + axial.V * MapConstants.AxisV;
		return new Vector3 (vec2.x, vec2.y, 0);
	}

	public static Vector3 ConvertVerticeToPixel (VerticeCoordinate vertice)
	{
		var vec2 = vertice.U * MapConstants.AxisU + vertice.V * MapConstants.AxisV;

		switch (vertice.D) {
		case eVerticeDirection.LEFT:
			vec2.x -= MapConstants.HexSize;
			break;
		case eVerticeDirection.RIGHT:
			vec2.x += MapConstants.HexSize;
			break;
		}

		return new Vector3 (vec2.x, vec2.y, 0);
	}

	#endregion

	#region Grid relationships

	public static AxialCoordinate ComputeTileNeighbour (AxialCoordinate coord, eHexDirection dir)
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

	public static List<AxialCoordinate> ComputeTileNeighbours (AxialCoordinate coord)
	{
		var result = new List<AxialCoordinate> ();

		result.Add (ComputeTileNeighbour (coord, eHexDirection.NORTH));
		result.Add (ComputeTileNeighbour (coord, eHexDirection.NORTHEAST));
		result.Add (ComputeTileNeighbour (coord, eHexDirection.SOUTHEAST));
		result.Add (ComputeTileNeighbour (coord, eHexDirection.SOUTH));
		result.Add (ComputeTileNeighbour (coord, eHexDirection.SOUTHWEST));
		result.Add (ComputeTileNeighbour (coord, eHexDirection.NORTHWEST));

		return result;
	}

	public static EdgeCoordinate ComputeTileBorder (AxialCoordinate coord, eHexDirection dir)
	{
		switch (dir) {
		case eHexDirection.NORTH:
			return new EdgeCoordinate (coord, eEdgeDirection.NORTH);
		case eHexDirection.NORTHEAST:
			return new EdgeCoordinate (coord, eEdgeDirection.EAST);
		case eHexDirection.SOUTHEAST:
			return new EdgeCoordinate (ComputeTileNeighbour (coord, eHexDirection.SOUTHEAST), eEdgeDirection.WEST);
		case eHexDirection.SOUTH:
			return new EdgeCoordinate (ComputeTileNeighbour (coord, eHexDirection.SOUTH), eEdgeDirection.NORTH);
		case eHexDirection.SOUTHWEST:
			return new EdgeCoordinate (ComputeTileNeighbour (coord, eHexDirection.SOUTHWEST), eEdgeDirection.EAST);
		case eHexDirection.NORTHWEST:
			return new EdgeCoordinate (coord, eEdgeDirection.WEST);
		default:
			Debug.LogError ("Wrong direction!");
			return new EdgeCoordinate ();
		}
	}

	public static List<EdgeCoordinate> ComputeTileBorders (AxialCoordinate coord)
	{
		var result = new List<EdgeCoordinate> ();

		result.Add (ComputeTileBorder (coord, eHexDirection.NORTH));
		result.Add (ComputeTileBorder (coord, eHexDirection.NORTHEAST));
		result.Add (ComputeTileBorder (coord, eHexDirection.SOUTHEAST));
		result.Add (ComputeTileBorder (coord, eHexDirection.SOUTH));
		result.Add (ComputeTileBorder (coord, eHexDirection.SOUTHWEST));
		result.Add (ComputeTileBorder (coord, eHexDirection.NORTHWEST));

		return result;
	}

	/// <summary>
	/// Returns the next coordinate of a Vertice clockwise relative to the direction. 
	/// </summary>
	/// <returns>The corner.</returns>
	/// <param name="coord">Coordinate.</param>
	/// <param name="dir">Dir.</param>
	public static VerticeCoordinate ComputeTileCorner (AxialCoordinate coord, eHexDirection dir)
	{
		switch (dir) {
		case eHexDirection.NORTH:
			return new VerticeCoordinate (ComputeTileNeighbour (coord, eHexDirection.NORTHEAST), eVerticeDirection.LEFT);
		case eHexDirection.NORTHEAST:
			return new VerticeCoordinate (coord, eVerticeDirection.RIGHT);
		case eHexDirection.SOUTHEAST:
			return new VerticeCoordinate (ComputeTileNeighbour (coord, eHexDirection.SOUTHEAST), eVerticeDirection.LEFT);
		case eHexDirection.SOUTH:
			return new VerticeCoordinate (ComputeTileNeighbour (coord, eHexDirection.SOUTHWEST), eVerticeDirection.RIGHT);
		case eHexDirection.SOUTHWEST:
			return new VerticeCoordinate (coord, eVerticeDirection.LEFT);
		case eHexDirection.NORTHWEST:
			return new VerticeCoordinate (ComputeTileNeighbour (coord, eHexDirection.NORTHWEST), eVerticeDirection.RIGHT);
		default:
			Debug.LogError ("Wrong direction!");
			return new VerticeCoordinate ();
		}
	}

	public static List<VerticeCoordinate> ComputeTileCorners (AxialCoordinate coord)
	{
		var result = new List<VerticeCoordinate> ();

		result.Add (ComputeTileCorner (coord, eHexDirection.NORTH));
		result.Add (ComputeTileCorner (coord, eHexDirection.NORTHEAST));
		result.Add (ComputeTileCorner (coord, eHexDirection.SOUTHEAST));
		result.Add (ComputeTileCorner (coord, eHexDirection.SOUTH));
		result.Add (ComputeTileCorner (coord, eHexDirection.SOUTHWEST));
		result.Add (ComputeTileCorner (coord, eHexDirection.NORTHWEST));

		return result;
	}

	public static List<AxialCoordinate> ComputeEdgeJoinedTiles (EdgeCoordinate coord)
	{
		var result = new List<AxialCoordinate> ();

		switch (coord.D) {
		case eEdgeDirection.NORTH:
			result.Add (new AxialCoordinate (coord.U, coord.V + 1));
			result.Add (new AxialCoordinate (coord.U, coord.V));
			break;
		case eEdgeDirection.EAST:
			result.Add (new AxialCoordinate (coord.U + 1, coord.V + 1));
			result.Add (new AxialCoordinate (coord.U, coord.V));
			break;
		case eEdgeDirection.WEST:
			result.Add (new AxialCoordinate (coord.U, coord.V));
			result.Add (new AxialCoordinate (coord.U - 1, coord.V));
			break;
		}

		return result;
	}

	public static List<EdgeCoordinate> ComputeEdgeContinuations (EdgeCoordinate edge, VerticeCoordinate vertice)
	{
		var result = ComputeVerticeProtrudingEdges (vertice);

		result.Remove (edge);

		return result;
	}

	public static List<VerticeCoordinate> ComputeEdgeEndpoints (EdgeCoordinate coord)
	{
		var result = new List<VerticeCoordinate> ();

		switch (coord.D) {
		case eEdgeDirection.NORTH:
			result.Add (new VerticeCoordinate (coord.U - 1, coord.V, eVerticeDirection.RIGHT));
			result.Add (new VerticeCoordinate (coord.U + 1, coord.V + 1, eVerticeDirection.LEFT));
			break;
		case eEdgeDirection.EAST:
			result.Add (new VerticeCoordinate (coord.U + 1, coord.V + 1, eVerticeDirection.LEFT));
			result.Add (new VerticeCoordinate (coord.U, coord.V, eVerticeDirection.RIGHT));
			break;
		case eEdgeDirection.WEST:
			result.Add (new VerticeCoordinate (coord.U, coord.V, eVerticeDirection.LEFT));
			result.Add (new VerticeCoordinate (coord.U - 1, coord.V, eVerticeDirection.RIGHT));
			break;
		}

		return result;
	}

	public static List<AxialCoordinate> ComputeVerticeTouchedTiles (VerticeCoordinate coord)
	{
		var result = new List<AxialCoordinate> ();

		switch (coord.D) {
		case eVerticeDirection.LEFT:
			result.Add (new AxialCoordinate (coord.U, coord.V));
			result.Add (new AxialCoordinate (coord.U - 1, coord.V - 1));
			result.Add (new AxialCoordinate (coord.U - 1, coord.V));
			break;
		case eVerticeDirection.RIGHT:
			result.Add (new AxialCoordinate (coord.U, coord.V));
			result.Add (new AxialCoordinate (coord.U + 1, coord.V + 1));
			result.Add (new AxialCoordinate (coord.U + 1, coord.V));
			break;
		}

		return result;
	}

	public static List<EdgeCoordinate> ComputeVerticeProtrudingEdges (VerticeCoordinate coord)
	{
		var result = new List<EdgeCoordinate> ();

		switch (coord.D) {
		case eVerticeDirection.LEFT:
			result.Add (new EdgeCoordinate (coord.U, coord.V, eEdgeDirection.WEST));
			result.Add (new EdgeCoordinate (coord.U - 1, coord.V - 1, eEdgeDirection.EAST));
			result.Add (new EdgeCoordinate (coord.U - 1, coord.V - 1, eEdgeDirection.NORTH));
			break;
		case eVerticeDirection.RIGHT:
			result.Add (new EdgeCoordinate (coord.U, coord.V, eEdgeDirection.EAST));
			result.Add (new EdgeCoordinate (coord.U + 1, coord.V, eEdgeDirection.NORTH));
			result.Add (new EdgeCoordinate (coord.U + 1, coord.V, eEdgeDirection.WEST));
			break;
		}

		return result;
	}

	public static List<VerticeCoordinate> ComputeVerticeAdjacentVertices (VerticeCoordinate coord)
	{
		var result = new List<VerticeCoordinate> ();

		switch (coord.D) {
		case eVerticeDirection.LEFT:
			result.Add (new VerticeCoordinate (coord.U - 1, coord.V - 1, eVerticeDirection.RIGHT));
			result.Add (new VerticeCoordinate (coord.U - 2, coord.V - 1, eVerticeDirection.RIGHT));
			result.Add (new VerticeCoordinate (coord.U - 1, coord.V, eVerticeDirection.RIGHT));
			break;
		case eVerticeDirection.RIGHT:
			result.Add (new VerticeCoordinate (coord.U + 2, coord.V + 1, eVerticeDirection.LEFT));
			result.Add (new VerticeCoordinate (coord.U + 1, coord.V, eVerticeDirection.LEFT));
			result.Add (new VerticeCoordinate (coord.U + 1, coord.V + 1, eVerticeDirection.LEFT));
			break;
		}

		return result;
	}

	#endregion
}
