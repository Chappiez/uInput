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
	public KeyCode KeycodeNegative;

	/// <summary>
	/// Keycode mapped to the positive part of the axis.
	/// </summary>
	public KeyCode KeycodePositive;

	/// <summary>
	/// Ease-out speed (range [0;1]). A value of 1 will prevent easing.
	/// </summary>
	public float Ease;

	/// <summary>
	/// If enabled, the axis value will be immediately reset to zero after it receives opposite inputs.
	/// </summary>
	public bool Snap;

	/// <summary>
	/// Gets the axis value in range [-1;1].
	/// </summary>
	public float Value
	{
		get { return value; }
	}

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
		this.KeycodeNegative = keycodeNegative;
		this.KeycodePositive = keycodePositive;
		this.Ease = ease;
		this.Snap = snap;

		Reset();
	}

	/// <summary>
	/// Updates the axis state. Will be called automatically by <c>uInput.Update()</c>.
	/// </summary>
	public void Update()
	{
		if (Input.GetKey(KeycodeNegative))
		{
			if (Snap && value > 0f) value = 0f;
			target = -1f;
		}
		else if (Input.GetKey(KeycodePositive))
		{
			if (Snap && value < 0f) value = 0f;
			target = 1f;
		}
		else target = 0f;

		value += (target - value) * Ease;
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
