﻿using System;
#if (MF_FRAMEWORK_VERSION_V4_2 || MF_FRAMEWORK_VERSION_V4_3)

#else
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endif

namespace LifxLib.Messages
{
    public class LifxLightStatusMessage : LifxReceivedMessage
    {
        public const UInt16 PACKET_TYPE = 0x6B;

        public LifxLightStatusMessage()
            : base(PACKET_TYPE)
        {

        }

        public UInt16 Hue
        {
            get 
            {
                return BitConverter.ToUInt16(base.ReceivedData.Payload, 0);
            }
        }

        public UInt16 Saturation
        {
            get
            {
                return BitConverter.ToUInt16(base.ReceivedData.Payload, 2);
            }
        }

        public UInt16 Lumnosity
        {
            get
            {
                return BitConverter.ToUInt16(base.ReceivedData.Payload, 4);
            }
        }
        public UInt16 Kelvin
        {
            get
            {
                return BitConverter.ToUInt16(base.ReceivedData.Payload, 6);
            }
        }
        public UInt16 Dim
        {
            get
            {
                return BitConverter.ToUInt16(base.ReceivedData.Payload, 8);
            }
        }
        public LifxPowerState PowerState
        {
            get
            {
                if (BitConverter.ToUInt16(ReceivedData.Payload, 10) == 0)
                    return LifxPowerState.Off;
                if (BitConverter.ToUInt16(ReceivedData.Payload, 10) == UInt16.MaxValue)
                    return LifxPowerState.On;
                else
                    return LifxPowerState.Unknown;
            }
        }


        

        public string Label
        {
            get
            {
#if (MF_FRAMEWORK_VERSION_V4_2 || MF_FRAMEWORK_VERSION_V4_3)
                char[] charMessage = System.Text.Encoding.UTF8.GetChars(base.ReceivedData.Payload);
                return new string(charMessage);
#else
                return Encoding.ASCII.GetString(base.ReceivedData.Payload, 12,32);
#endif
            }
        }

        public UInt64 Tags
        {
            get
            {
                return BitConverter.ToUInt64(base.ReceivedData.Payload, 44);
            }
        }
    }
}
