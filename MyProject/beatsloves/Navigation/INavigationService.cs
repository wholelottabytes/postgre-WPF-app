﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beatsloves.Navigation
{
    public interface INavigationService
    {
        void NavigateTo(object destinationPage);
    }
}
