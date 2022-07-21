using UnityEngine;

public static class Extension
{
    public static void SetIKPositionAndWeight(this Animator animator, AvatarIKGoal goal, Vector3 goalPosition, float weight = 1f)
    {
        animator.SetIKPositionWeight(goal, weight);
        animator.SetIKPosition(goal, goalPosition);
    }

    public static void SetIKRotationAndWeight(this Animator animator, AvatarIKGoal goal, Quaternion goalRotation, float weight = 1f)
    {
        animator.SetIKRotationWeight(goal, weight);
        animator.SetIKRotation(goal, goalRotation);
    }

    public static void SetIKTransformAndWeight(this Animator animator, AvatarIKGoal goal, Transform goalTransform, float weight = 1f)
    {
        animator.SetIKPositionAndWeight(goal, goalTransform.position, weight);
        animator.SetIKRotationAndWeight(goal, goalTransform.rotation, weight);
    }
}