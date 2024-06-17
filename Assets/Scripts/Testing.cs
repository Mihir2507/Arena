using UnityEngine;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Collections;
using Unity.Burst;

public class TestingJob : MonoBehaviour {

    public bool useJob;

    [System.Obsolete]
    public void Update(){
        float startTime = Time.realtimeSinceStartup;
        if(useJob){
            NativeList<JobHandle> jobHandles = new NativeList<JobHandle>(Allocator.TempJob);
            for (int i = 0; i< 10; i++){
                JobHandle jobHandle = VeryHeavyTaskJob();
                jobHandles.Add(jobHandle);
            }
            JobHandle.CompleteAll(jobHandles);
            jobHandles.Dispose();
        } else {
            for (int i = 0; i< 10; i++){
                VeryHeavyTask();
            }
        }
        Debug.Log(((Time.realtimeSinceStartup - startTime) * 1000f) + " ms");
    }

    private void VeryHeavyTask(){
        float value = 0f;
        for (int i=0; i<500000; i++){
            math.exp10(math.sqrt(value));
        }
    }

    private JobHandle VeryHeavyTaskJob(){
        VeryHeavyJob job = new VeryHeavyJob();
        return job.Schedule();
    }
}

[BurstCompile]
public struct VeryHeavyJob : IJob {
    public void Execute(){
        float value = 0f;
        for (int i=0; i<500000; i++){
            math.exp10(math.sqrt(value));
        }
    }
}

