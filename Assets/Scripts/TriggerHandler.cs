using UnityEngine;
using System.Collections;

public class TriggerHandler : MonoBehaviour
{
    private bool triggerEffectDimensionsisActive = false;
    private bool effectWasTriggeredFirstTime = false;
    private bool bridgeCanBeActivated = false;

    public void setTriggerActive(bool _isActive)
    {
        this.triggerEffectDimensionsisActive = _isActive;
    }

    public bool getIsActive()
    {
        return this.triggerEffectDimensionsisActive;
    }

    public void setEffectWasTriggeredFirstTime(bool _wasTriggered)
    {
        this.effectWasTriggeredFirstTime = _wasTriggered;
    }

    public bool getIfeffectWasTriggeredFirstTime()
    {
        return this.effectWasTriggeredFirstTime;
    }

    public bool getIfBridgeCanBeActivated()
    {
        return this.bridgeCanBeActivated;
    }

    public void setIfBridgeCanBeActivated(bool _canBridgeBeActivated)
    {
        this.bridgeCanBeActivated = _canBridgeBeActivated;
    }
}
