using System;

[Serializable]
class uInputException : Exception
{
	public uInputException(string message)
		: base(message)
	{
	}
}
