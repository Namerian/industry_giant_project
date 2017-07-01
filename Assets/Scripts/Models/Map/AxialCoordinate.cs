using System;

/// <summary>
/// Struct with the position of a hexagon in axial coordinates.
/// </summary>
public struct AxialCoordinate : IEquatable<AxialCoordinate>
{
	public int U{ get; private set; }

	public int V{ get; private set; }

	public AxialCoordinate (int u, int v)
	{
		this.U = u;
		this.V = v;
	}

	#region Object overrides

	public override int GetHashCode ()
	{
		return 13 + (7 * this.U) + (7 * this.V);
	}

	public override bool Equals (object obj)
	{
		return obj is AxialCoordinate && Equals ((AxialCoordinate)obj);
	}

	#endregion

	#region IEquatable implementation

	public bool Equals (AxialCoordinate other)
	{
		return this.U == other.U && this.V == other.V;
	}

	#endregion
}
