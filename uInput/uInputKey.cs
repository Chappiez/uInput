using UnityEngine;

/// <summary>
/// A simple key.
/// </summary>
public class uInputKey
{
	/// <summary>
	/// Keycode mapped to this <c>uInputKey</c>.
	/// </summary>
	public KeyCode keycode;

	/// <summary>
	/// Gets if this key is held down.
	/// </summary>
	public bool isDown { get { return Input.GetKey(keycode); } }

	/// <summary>
	/// Gets if this key was pressed this frame.
	/// </summary>
	public bool isPressed { get { return Input.GetKeyDown(keycode); } }

	/// <summary>
	/// Gets if this key was released this frame.
	/// </summary>
	public bool isReleased { get { return Input.GetKeyUp(keycode); } }

	/// <summary>
	/// Creates a new <c>uInputKey</c>. Use <c>uInput.DefineKey()</c> instead.
	/// </summary>
	/// <param name="keycode">A keycode.</param>
	public uInputKey(KeyCode keycode)
	{
		this.keycode = keycode;
	}
}
