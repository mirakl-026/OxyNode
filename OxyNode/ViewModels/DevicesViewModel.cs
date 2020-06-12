using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Models;

// вьюмодель для газоанализаторов
namespace OxyNode.ViewModels
{
    public class DevicesViewModel
    {
        // список объектов - газоанализаторов
        public List<Device> devices { get; set; }

    }
}
