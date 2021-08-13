using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interfaces
{
    public interface IAmazonService
    {
        public void SendMessage(string command, string message);
    }
}
