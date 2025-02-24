using UnityEngine;
using UnityEngine.InputSystem;

public class Escale : MonoBehaviour
{
    float remapValue(float originValue, float firstIntervalMinimum, float firstIntervalMaximum, float secondIntervalMinimum, float secondIntervalMaximum)
    {
        float relative = (Mathf.Clamp(originValue, firstIntervalMinimum, firstIntervalMaximum) - firstIntervalMinimum) / (firstIntervalMaximum - firstIntervalMinimum);
        return secondIntervalMinimum + (secondIntervalMaximum - secondIntervalMinimum) * relative;
    }

    void Start()
    {
        transform.localScale = scaleOrigin;
        secondsPassed = 0.0f;
        numberOfCyclesPassed = 0;

        firstStagePercentage = Mathf.Clamp01(firstStagePercentage);
        scaleMultiplier = Mathf.Max(scaleMultiplier, 1.0f);
    }

    void Update()
    {
        Inputs();

        if (mAppear)
        {
            if (isInfinite || numberOfCyclesPassed < maximumNumberOfCycles)
            {
                secondsPassed += Time.deltaTime;
                float periodPercentage = Mathf.Clamp01(secondsPassed / cycleSeconds);

                if (periodPercentage <= firstStagePercentage)
                {
                    transform.localScale = remapValue(periodPercentage, 0.0f, firstStagePercentage, 1.0f, scaleMultiplier) * scaleOrigin;
                }
                else
                {
                    transform.localScale = (scaleMultiplier - remapValue(periodPercentage, firstStagePercentage, 1.0f, 0.0f, scaleMultiplier - 1.0f)) * scaleOrigin;
                }

                if (periodPercentage >= 1.0f)
                {
                    if (!isInfinite)
                    {
                        ++numberOfCyclesPassed;
                    }
                    secondsPassed = 0.0f;
                    mAppear = false;
                }
            }
        }
    }

    private void Inputs()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            mAppear = true;
        }
    }

    /**********************************************************************************************************************/
    // ATTRIBUTES
    /**********************************************************************************************************************/

    private float secondsPassed;                       // Seconds passed

    private int numberOfCyclesPassed;                // Number of cycles passed

    public bool isInfinite = false;       // If true, the number of cycles is infinite

    public int maximumNumberOfCycles = 1;           // Maximum number of cycles

    public float cycleSeconds = 1.0f;        // Cycle duration, in seconds

    public Vector3 scaleOrigin = Vector3.one; // Original scale

    public float scaleMultiplier = 2.0f;        // Scale multiplier

    public float firstStagePercentage = 0.2f;        // First stage percentage

    public bool mAppear;

}
