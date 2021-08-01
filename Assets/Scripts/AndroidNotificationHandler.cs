using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif

public class AndroidNotificationHandler : MonoBehaviour
{
#if UNITY_ANDROID

    const string ChannelID = "notification_channel";

    public void ScheduleNotification(DateTime dateTime)
    {
        AndroidNotificationChannel notificationChannel = new AndroidNotificationChannel
        {
            Id = ChannelID,
            Name = "Notification Channel",
            Description = "Description",
            Importance = Importance.Default
        };
        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

        AndroidNotification notification = new AndroidNotification
        {
            Title = "Energy recharged.",
            Text = "Your energy has recharged, come back to play again",
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = dateTime
        };
        AndroidNotificationCenter.SendNotification(notification, ChannelID);
    }
#endif
}
