using UnityEngine;

/// <summary>
/// A simple key.
/// </summary>
public class uInputKey
{
	/// <summary>
	/// Keycode mapped to this <c>uInputKey</c>.
	/// </summary>
	public KeyCode Keycode;

	/// <summary>
	/// Gets if this key is held down.
	/// </summary>
	public bool IsDown { get { return Input.GetKey(Keycode); } }

	/// <summary>
	/// Gets if this key was pressed this frame.
	/// </summary>
	public bool IsPressed { get { return Input.GetKeyDown(Keycode); } }

	/// <summary>
	/// Gets if this key was released this frame.
	/// </summary>
	public bool IsReleased { get { return Input.GetKeyUp(Keycode); } }

	/// <summary>
	/// Creates a new <c>uInputKey</c>. Use <c>uInput.DefineKey()</c> instead.
	/// </summary>
	/// <param name="keycode">A keycode.</param>
	public uInputKey(KeyCode keycode)
	{
		this.Keycode = keycode;
	}
}
