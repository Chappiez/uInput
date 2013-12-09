using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// uInput is an alternative to the default Input Manager bundled with Unity3D. It allows you
/// to define and modify keyboard inputs and axis at runtime, which is something the default Input
/// Manager does not, surprisingly. It's purely script-driven so don't expect any GUI.
/// 
/// Usage is quite simple : use <c>uInput.Init()</c> as soon as possible in your code, and call
/// <c>uInput.Update()</c> on every frame. You can skip this last step if you don't make use of
/// axes.
/// 
/// Key bindings can be defined as follows :
/// 
/// <code>
/// uInput.DefineKey("Jump", KeyCode.X);
/// uInput.DefineKey("Fire", KeyCode.C);
/// uInput.DefineAxis("Horizontal", KeyCode.LeftArrow, KeyCode.RightArrow, 0.1f, false);
/// </code>
/// 
/// And then used as follows :
/// 
/// <code>
/// if (uInput.IsPressed("Jump"))
///		Jump();
///		
/// if (uInput.IsDown("Fire"))
///		Fire();
///		
/// float speedX = uInput.GetAxis("Horizontal") * speed;
/// </code>
/// 
/// See the in-code documentation for more information about every method and parameter.
/// 
/// Note : at the moment, uInput only handles keyboard events. For mouse and joystick, you'll have
/// to stick to the default Input Manager.
/// </summary>
public static class uInput
{
	/// <summary>
	/// Is any key or mouse button currently held down ?
	/// </summary>
	public static bool anyKeyDown { get { return Input.anyKey; } }
	
	/// <summary>
	/// Returns true the first frame the user hits any key or mouse button.
	/// </summary>
	public static bool anyKeyPressed { get { return Input.anyKeyDown; } }

	static Dictionary<string, uInputKey> keys;
	static Dictionary<string, uInputAxis> axes;

	/// <summary>
	/// Initializes uInput. Must be called first.
	/// </summary>
	public static void Init()
	{
		keys = new Dictionary<string, uInputKey>();
		axes = new Dictionary<string, uInputAxis>();
	}

	/// <summary>
	/// Defines a new key input or updates an existing one.
	/// </summary>
	/// <param name="name">String to map the key to.</param>
	/// <param name="keycode">A keycode.</param>
	/// <returns>The defined <c>uInputKey</c>.</returns>
	public static uInputKey DefineKey(string name, KeyCode keycode)
	{
		if (keys.ContainsKey(name))
			keys[name].keycode = keycode;
		else
			keys.Add(name, new uInputKey(keycode));

		return keys[name];
	}

	/// <summary>
	/// Defines a new axis or updates an existing one.
	/// </summary>
	/// <param name="name">String to map the axis to.</param>
	/// <param name="keycodeNegative">Keycode to use for the negative part of the axis.</param>
	/// <param name="keycodePositive">Keycode to use for the positive part of the axis.</param>
	/// <param name="ease">Ease-out speed (range [0;1]). A value of 1 will prevent easing.</param>
	/// <param name="snap">If enabled, the axis value will be immediately reset to zero after it receives opposite inputs.</param>
	/// <returns>The defined <c>uInputAxis</c>.</returns>
	public static uInputAxis DefineAxis(string name, KeyCode keycodeNegative, KeyCode keycodePositive, float ease = 1f, bool snap = false)
	{
		if (axes.ContainsKey(name))
		{
			axes[name].keycodeNegative = keycodeNegative;
			axes[name].keycodePositive = keycodePositive;
			axes[name].ease = ease;
			axes[name].snap = snap;
		}
		else
			axes.Add(name, new uInputAxis(keycodeNegative, keycodePositive, ease, snap));

		return axes[name];
	}

	/// <summary>
	/// Updates the axes states. You must call this method on every frame if you make use of axes.
	/// </summary>
	public static void Update()
	{
		foreach (var axis in axes)
			axis.Value.Update();
	}

	/// <summary>
	/// Resets axes states. You should call this method when you want to reset any easing states,
	/// i.e. when a new level starts.
	/// </summary>
	public static void Reset()
	{
		foreach (var axis in axes)
			axis.Value.Reset();
	}

	/// <summary>
	/// Returns the <c>uInputKey</c> associated with the given name.
	/// </summary>
	/// <param name="name">Input name.</param>
	/// <returns>The <c>uInputKey</c> associated with the given name.</returns>
	public static uInputKey GetInputKey(string name)
	{
		if (!keys.ContainsKey(name))
			throw new uInputException("Key definition not found: " + name);

		return keys[name];
	}

	/// <summary>
	/// Returns the <c>uInputAxis</c> associated with the given name.
	/// </summary>
	/// <param name="name">Input name.</param>
	/// <returns>The <c>uInputAxis</c> associated with the given name.</returns>
	public static uInputAxis GetInputAxis(string name)
	{
		if (!axes.ContainsKey(name))
			throw new uInputException("Axis definition not found: " + name);

		return axes[name];
	}

	/// <summary>
	/// Gets an axis value (range [-1;1]).
	/// </summary>
	/// <param name="name">Input name.</param>
	/// <returns>A value in range [-1;1].</returns>
	public static float GetAxis(string name)
	{
		return GetInputAxis(name).GetValue();
	}

	/// <summary>
	/// Gets if a key is held down.
	/// </summary>
	/// <param name="name">Input name.</param>
	/// <returns>The input state.</returns>
	public static bool IsDown(string name)
	{
		return GetInputKey(name).isDown;
	}

	/// <summary>
	/// Gets if a key is held down.
	/// </summary>
	/// <param name="keyCode">Direct KeyCode identifier.</param>
	/// <returns>The input state.</returns>
	public static bool IsDown(KeyCode keyCode)
	{
		return Input.GetKey(keyCode);
	}

	/// <summary>
	/// Gets if a key was pressed this frame.
	/// </summary>
	/// <param name="name">Input name.</param>
	/// <returns>The input state.</returns>
	public static bool IsPressed(string name)
	{
		return GetInputKey(name).isPressed;
	}

	/// <summary>
	/// Gets if a key was pressed this frame.
	/// </summary>
	/// <param name="keyCode">Direct KeyCode identifier.</param>
	/// <returns>The input state.</returns>
	public static bool IsPressed(KeyCode keyCode)
	{
		return Input.GetKeyDown(keyCode);
	}

	/// <summary>
	/// Gets if a key was released this frame.
	/// </summary>
	/// <param name="name">Input name.</param>
	/// <returns>The input state.</returns>
	public static bool IsReleased(string name)
	{
		return GetInputKey(name).isReleased;
	}

	/// <summary>
	/// Gets if a key was released this frame.
	/// </summary>
	/// <param name="keyCode">Direct KeyCode identifier.</param>
	/// <returns>The input state.</returns>
	public static bool IsReleased(KeyCode keyCode)
	{
		return Input.GetKeyUp(keyCode);
	}

	/// <summary>
	/// Clears any memory used by <c>uInput</c>. You'll have to call <c>Init()</c> after that if you
	/// want to use it again.
	/// </summary>
	public static void Clear()
	{
		keys.Clear();
		keys = null;
		axes.Clear();
		axes = null;
	}
}
