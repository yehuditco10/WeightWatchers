using System;
using System.Collections.Generic;
using System.Text;

namespace Messages.Commands
{
   public class AddTrack
    {
        public int CardId { get; set; }
        public float NewWeight { get; set; }
        public float NewBMI { get; set; }
        public int Trand { get; set; }
        public int Comments { get; set; }

    }
}
