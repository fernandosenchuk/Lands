using System;
using System.Collections.Generic;
using System.Text;
using Lands.ViewModels;

namespace Lands.Infrastructure
{
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
    }
}
