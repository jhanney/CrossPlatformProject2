﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformProject2.ViewModels
{
    public class AchievementsViewModel
    {
        public ObservableCollection


    
    }
    public class Achievment
    {
        public string Title { get; set; } //title of the achievement
        public string Description { get; set; }//achievment description
        public bool IsUnlocked { get; set; }//whther its been completed
    }
}
