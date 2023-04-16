﻿using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Dtos.Subscriptions
{
    public class SubscriptionDto
    {
        [Range(0, int.MaxValue)]
        public int PhotographerId { get; set; }

        [Range(0, int.MaxValue)]
        public int SubscriberId { get; set; }
    }
}
