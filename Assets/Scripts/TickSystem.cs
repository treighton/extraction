using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickSystem : MonoBehaviour
{

    public class OnTickEventArgs : EventArgs {
        public int tick;
    }

    public static event EventHandler<OnTickEventArgs> onTick;

    private const float TICK_TIMER_INTERVAL = .2f;
    private int tick;
    private float tickTimer;
    void Awake()
    {
        tick = 0;
    }

    void Update()
    {
        tickTimer += Time.deltaTime;
        if (tickTimer >= TICK_TIMER_INTERVAL) {
            tickTimer -= TICK_TIMER_INTERVAL;
            tick++;
            if(onTick != null) {
                onTick(this, new OnTickEventArgs{
                    tick = tick
                });
            } 
        }
    }
}
