package org.anddev.andengine.sensor.accelerometer;

using andengine.sensor.SensorDelay;

/**
 * @author Nicolas Gramlich
 * @since 11:10:34 - 31.10.2010
 */
public class AccelerometerSensorOptions {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	final SensorDelay mSensorDelay;

	// ===========================================================
	// Constructors
	// ===========================================================

	public AccelerometerSensorOptions(final SensorDelay pSensorDelay) {
		this.mSensorDelay = pSensorDelay;
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	public SensorDelay getSensorDelay() {
		return this.mSensorDelay;
	}

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
