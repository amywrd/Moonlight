using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	private static float _amplitude;
	private static float _duration;
	private static bool isShaking = false;

	private Vector3 initialPosition;

	public static void Shake(float amplitude = 0.1f, float duration = 0.5f)
	{
		_amplitude = amplitude;
		_duration = duration;
		isShaking = true;
	}

	public static void Stop()
	{
		isShaking = false;
	}

	void Start()
	{
		initialPosition = transform.localPosition;
	}

	void Update()
	{
		_duration -= Time.deltaTime;
		_amplitude -= 0.1f * Time.deltaTime;

		if(isShaking && _duration > 0)
			transform.localPosition = initialPosition + Random.insideUnitSphere * _amplitude;
	}
}
