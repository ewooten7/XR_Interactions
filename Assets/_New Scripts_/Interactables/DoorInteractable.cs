using UnityEngine;
using UnityEngine.Events;

public class DoorInteractable : SimpleHingeInteractable
{
    public UnityEvent OnOpen;
    [SerializeField] CombinationLock comboLock;
    [SerializeField] Transform doorObject;
    [SerializeField] Vector3 rotationLimits;
    [SerializeField] Collider closedCollider;
    private bool isClosed;
    private Vector3 startRotation;
    [SerializeField] Collider openCollider;
    private bool isOpen;
    [SerializeField] private Vector3 endRotation;
    private float startAngleX;
    protected override void Start()
    {
        base.Start();
        startRotation = transform.localEulerAngles;
        startAngleX = GetAngle(startRotation.x);
        if (comboLock != null)
        {
            comboLock.UnlockAction += OnUnlocked;
            comboLock.LockAction += OnLocked;
        }
    }
    private void OnLocked()
    {
        LockHinge();
    }
    private void OnUnlocked()
    {
        UnlockHinge();
    }

    // Update is called once per frame 
    protected override void Update()
    {
        base.Update();
        if (doorObject != null)
        {
            doorObject.localEulerAngles = new Vector3(
                doorObject.localEulerAngles.x,
                transform.localEulerAngles.y,
                doorObject.localEulerAngles.z
            );
        }

        if (isSelected)
        {
            CheckLimits();
        }
    }
    private void CheckLimits()
    {
        isClosed = false;
        isOpen = false;
        float localAngleX = GetAngle(transform.localEulerAngles.x);
        if (localAngleX >= startAngleX + rotationLimits.x ||
            localAngleX <= startAngleX - rotationLimits.x)
        {
            ReleaseHinge();

        }
    }
    private float GetAngle(float angle)
    {
        if (angle >= 180)
        {
            angle -= 360;
        }
        return angle;
    }
    protected override void ResetHinge()
    {
        if (isClosed)
        {
            transform.localEulerAngles = startRotation;
        }
        else if(isOpen)
        {
            transform.localEulerAngles = endRotation;
            OnOpen.Invoke();
        }
        else
        {
            transform.localEulerAngles = new Vector3(
                startAngleX,
                transform.localEulerAngles.y,
                transform.localEulerAngles.z);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == closedCollider)
        {
            isClosed = true;
            ReleaseHinge();
        }
        else if(other == openCollider)
        {
            isOpen = true;
            ReleaseHinge();
        }
    }
}

