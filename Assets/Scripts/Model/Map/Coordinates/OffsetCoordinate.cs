using System;

/// <summary>
/// Struct with the position of a hexagon in odd-q offset coordinates.
/// </summary>
public struct OffsetCoordinate : IEquatable<OffsetCoordinate>
{
	public int Col{ get; private set; }

	public int Row{ get; private set; }

	public OffsetCoordinate (int col, int row)
	{
		this.Col = col;
		this.Row = row;
	}

	#region Object overrides

	public override int GetHashCode ()
	{
		return 13 + (7 * this.Col) + (7 * this.Row);
	}

	public override bool Equals (object obj)
	{
		return obj is OffsetCoordinate && Equals ((OffsetCoordinate)obj);
	}

	#endregion

	#region IEquatable implementation

	public bool Equals (OffsetCoordinate other)
	{
		return this.Col == other.Col && this.Row == other.Row;
	}

	#endregion
}
