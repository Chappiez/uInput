using UnityEngine;

/// <summary>
/// An axis is defined in a [-1;1] range and thus requires two <c>KeyCode</c> :
/// one for the negative part of the axis, and another one for the positive part.
/// </summary>
public class uInputAxis
{
	/// <summary>
	/// Keycode mapped to the negative part of the axis.
	/// </summary>
	public KeyCode keycodeNegative;

	/// <summary>
	/// Keycode mapped to the positive part of the axis.
	/// </summary>
	public KeyCode keycodePositive;

	/// <summary>
	/// Ease-out speed (range [0;1]). A value of 1 will prevent easing.
	/// </summary>
	public float ease;

	/// <summary>
	/// If enabled, the axis value will be immediately reset to zero after it receives opposite inputs.
	/// </summary>
	public bool snap;

	float value;
	float target;

	/// <summary>
	/// Creates a new <c>uInputAxis</c>. Use <c>uInput.DefineAxis()</c> instead.
	/// </summary>
	/// <param name="keycodeNegative">Keycode to use for the negative part of the axis.</param>
	/// <param name="keycodePositive">Keycode to use for the positive part of the axis.</param>
	/// <param name="ease">Ease-out speed (range [0;1]). A value of 1 will prevent easing.</param>
	/// <param name="snap">If enabled, the axis value will be immediately reset to zero after it receives opposite inputs.</param>
	public uInputAxis(KeyCode keycodeNegative, KeyCode keycodePositive, float ease = 1f, bool snap = false)
	{
		this.keycodeNegative = keycodeNegative;
		this.keycodePositive = keycodePositive;
		this.ease = ease;
		this.snap = snap;

		Reset();
	}

	/// <summary>
	/// Gets the axis value
	/// </summary>
	/// <returns>A value in range [-1;1].</returns>
	public float GetValue()
	{
		return value;
	}

	/// <summary>
	/// Updates the axis state. Will be called automatically by <c>uInput.Update()</c>.
	/// </summary>
	public void Update()
	{
		if (Input.GetKey(keycodeNegative))
		{
			if (snap && value > 0f) value = 0f;
			target = -1f;
		}
		else if (Input.GetKey(keycodePositive))
		{
			if (snap && value < 0f) value = 0f;
			target = 1f;
		}
		else target = 0f;

		value += (target - value) * ease;
	}

	/// <summary>
	/// Resets this axis state.
	/// </summary>
	public void Reset()
	{
		value = 0f;
		target = 0f;
	}
}
