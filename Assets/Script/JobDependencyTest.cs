using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Jobs;

// Job adding two floating point values together
public struct AddJob : IJob
{
    public float a;
    public float b;
    public NativeArray<float> result;

    public void Execute()
    {
        result[0] = a + b;
    }
}

// Job multiply one float value by another
public struct MulResultJob : IJob
{
    public float value;
    public NativeArray<float> result;

    public void Execute()
    {
        result[0] = result[0] * value;
    }
}

// Job adding two floating point values together
public struct AddResultJob : IJob
{
    public float value;
    public NativeArray<float> result;

    public void Execute()
    {
        result[0] = result[0] + value;
    }
}

public class JobDependencyTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Create a native array of a single float to store the result in. This example waits for the job to complete
        NativeArray<float> result = new NativeArray<float>(1, Allocator.TempJob);

        // Setup the data for job #1
        AddJob jobData = new AddJob();
        jobData.a = 10;
        jobData.b = 10;
        jobData.result = result;

        // Schedule job #1
        JobHandle firstHandle = jobData.Schedule();

        // Setup the data for job #2
        MulResultJob mulJobData = new MulResultJob();
        mulJobData.value = 5;
        mulJobData.result = result;

        // Schedule job #2
        JobHandle secondHandle = mulJobData.Schedule(firstHandle);//firstHandle

        // job #3
        AddResultJob addJobData = new AddResultJob();
        addJobData.value = 9;
        addJobData.result = result;
        JobHandle thirdHandle = addJobData.Schedule(secondHandle);//secondHandle

        // Wait for job #3 to complete
        thirdHandle.Complete();

        // All copies of the NativeArray point to the same memory, you can access the result in "your" copy of the NativeArray
        float aPlusB = result[0];
        Debug.Log("Result:" + aPlusB);

        // Free the memory allocated by the result array
        result.Dispose();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
