using UnityEngine;
using System.Collections;

public class SampleSpin : MonoBehaviour
{
  void Start()
  {
    if ( GetComponent<Rigidbody>() != null )
    {
      mRigidBody = GetComponent<Rigidbody>();
    }
    mPositionStart = transform.position;
  }

  private void Update()
  {
    if ( Input.GetKeyDown( KeyCode.Space ) )
    {
      mShotRequested = true;
    }
    else if ( Input.GetKeyDown( KeyCode.R ) )
    {
      mResetRequested = true;
    }
  }

  void FixedUpdate()
  {
    if ( mRigidBody != null )
    {
      if ( mResetRequested )
      {
        mResetRequested = false;
        mRigidBody.linearVelocity = Vector3.zero;
        mRigidBody.angularVelocity = Vector3.zero;
        transform.position = mPositionStart;
      }
      else
      {
        if ( mShotRequested )
        {
          mShotRequested = false;
          mRigidBody.AddForce( mShotInitialLinearVelocityMagnitude * mShotInitialDirection.normalized, ForceMode.VelocityChange );
          mRigidBody.maxAngularVelocity = mSpinMagnitude;
          mRigidBody.AddTorque( Mathf.Abs( mSpinMagnitude ) * mSpinAxis, ForceMode.VelocityChange );
        }

        Vector3 effectForceDirection = 100.0f * mRigidBody.linearVelocity.magnitude * Vector3.Cross( mSpinAxis, mRigidBody.linearVelocity );
        // NOTE: This is by NO means an accurate representation; it is just a test taking into account the axes and some factors involved in the effect
        mRigidBody.AddForce( mArbitraryVelocityMultiplier * mRigidBody.linearVelocity.magnitude * Vector3.Cross( mSpinAxis, mRigidBody.linearVelocity.normalized ).normalized, ForceMode.VelocityChange );
      }
    }
  }

  /**********************************************************************************************************************/
  // ATTRIBUTES
  /**********************************************************************************************************************/

  private Vector3    mPositionStart                        = Vector3.zero; // Initial position

  private bool       mShotRequested                        = false;        // Shot request flag

  private bool       mResetRequested                       = false;        // Reset request flag

  private Rigidbody  mRigidBody                            = null;         // Rigid body used

  private float      mArbitraryVelocityMultiplier          = 0.01f;        // Arbitrary multiplier

  public Vector3     mShotInitialDirection                 = Vector3.zero; // Shot initial direction

  public float       mShotInitialLinearVelocityMagnitude   = 0.0f;         // Shot initial linear velocity magnitude

  public Vector3     mSpinAxis                             = Vector3.up;   // Spin axis

  public float       mSpinMagnitude                        = 1.0f;         // Spin magnitude
}
