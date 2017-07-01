using System;

/// <summary>
/// Struct with the position of a hexagon in cube coordinates.
/// </summary>
public struct CubeCoordinate : IEquatable<CubeCoordinate>
{
	public int X{ get; private set; }

	public int Y{ get; private set; }

	public int Z{ get; private set; }

	public CubeCoordinate (int x, int y, int z)
	{
		this.X = x;
		this.Y = y;
		this.Z = z;
	}

	#region Object overrides

	public override int GetHashCode ()
	{
		return 13 + (7 * this.X) + (7 * this.Y) + (7 * this.Z);
	}

	public override bool Equals (object obj)
	{
		return obj is CubeCoordinate && Equals ((CubeCoordinate)obj);
	}

	#endregion

	#region IEquatable implementation

	public bool Equals (CubeCoordinate other)
	{
		return this.X == other.X && this.Y == other.Y && this.Z == other.Z;
	}

	#endregion
}
