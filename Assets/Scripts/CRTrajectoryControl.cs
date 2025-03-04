using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRTrajectoryControl : MonoBehaviour
{
  public List<Transform> transforms = new List<Transform>();
  private List<Vector3> positions = new List<Vector3>();

  public float processDuration = 1f;
  public float delayUntilLaunch = 0f;

  private float timeFromStart = 0f;
  private float timeFromLaunch = 0f;
  private float timeFactor = 0f;
    private bool mAppear=false;

    // Start is called before the first frame update
    void Start()
  {
    if ( positions.Count == 0 )
    {
      CollectPositions();
    }
  }

  // Update is called once per frame
  void Update()
  {
        Inputs();

        float deltaTime = Time.deltaTime;

    timeFromStart += deltaTime;
    if ( timeFromStart >= delayUntilLaunch && mAppear==true)
    {
      timeFromLaunch += deltaTime;
      timeFactor = Mathf.Clamp01( timeFromLaunch / processDuration );
      transform.position = CatmullRomSplineInterpolate( timeFactor, positions );
        
    }
  }


    private void Inputs()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            mAppear = true;
        }
    }


    public Vector3 CatmullRomSplineInterpolate( float t, List<Vector3> points )
  {
    int numPoints = points.Count;

    if ( numPoints < 4 )
    {
      Debug.LogError( "At least four points are required for Catmull-Rom spline interpolation." );
      return Vector3.zero;
    }

    int startIndex = Mathf.FloorToInt( t * ( numPoints - 1 ) );
    float localT = t * ( numPoints - 1 ) - startIndex;

    Vector3 p0 = points[Mathf.Clamp( startIndex - 1, 0, numPoints - 1 )];
    Vector3 p1 = points[startIndex];
    Vector3 p2 = points[Mathf.Clamp( startIndex + 1, 0, numPoints - 1 )];
    Vector3 p3 = points[Mathf.Clamp( startIndex + 2, 0, numPoints - 1 )];
  
        return 0.5f * (
        ( -p0 + 3f * p1 - 3f * p2 + p3 ) * ( localT * localT * localT ) +
        ( 2f * p0 - 5f * p1 + 4f * p2 - p3 ) * ( localT * localT ) +
        ( -p0 + p2 ) * localT +
        2f * p1
    );
     
  }






  private void CollectPositions()
  {
    positions.Clear();
    foreach ( Transform transform in transforms )
    {
      positions.Add( transform.position );
    }
  }

  private void OnDrawGizmos()
  {
    if ( positions.Count == 0 )
    {
      CollectPositions();
    }

    for ( int i = 0; i < positions.Count; ++i )
    {
      Gizmos.DrawSphere( positions[i], 0.25f );
    }
    int lineCount = 3;
    float stepFactor = 1f / ( float ) lineCount;
    for ( int j = 0; j < lineCount; ++j )
    {
      Gizmos.DrawLine(
        CatmullRomSplineInterpolate( ( float ) j * stepFactor, positions ),
        CatmullRomSplineInterpolate( ( float ) ( j + 1 ) * stepFactor, positions ) );
    }
  }





  [ContextMenu( "REFRESH Trajectory Prediction" )]
  void ResetTrajectoryPositions()
  {
    CollectPositions();
  }
}
