using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Caliburn.Micro;
using FollowMe.Configuration;
using FollowMe.Interfaces;

namespace FollowMe.ArDrone
{
    public class ControlConfigBasedArDroneConfigProvider : IArDroneConfigProvider
    {
        private readonly string controlConfig;

        public ControlConfigBasedArDroneConfigProvider(string controlConfig)
        {
            if (controlConfig == null) throw new ArgumentNullException("controlConfig");
            this.controlConfig = controlConfig;
        }

        public ArDroneConfig GetArDroneConfig()
        {
            var arDroneConfig = new ArDroneConfig();

            arDroneConfig.AltitudeMax = GetValueFromControlConfig<int>("altitude_max");
            arDroneConfig.AltitudeMin = GetValueFromControlConfig<int>("altitude_min");
            arDroneConfig.Outdoor = GetValueFromControlConfig<bool>("outdoor");
            arDroneConfig.FlightWithoutShell = GetValueFromControlConfig<bool>("flight_without_shell");
            arDroneConfig.ControlLevel = GetValueFromControlConfig<int>("control_level");
            arDroneConfig.EulerAngleMax = GetValueFromControlConfig<float>("euler_angle_max");
            arDroneConfig.ControlVzMax = GetValueFromControlConfig<float>("control_vz_max");
            arDroneConfig.ControlYaw = GetValueFromControlConfig<float>("control_yaw");
            arDroneConfig.ManualTrim = GetValueFromControlConfig<bool>("manual_trim");
            arDroneConfig.IndoorEulerAngleMax = GetValueFromControlConfig<float>("indoor_euler_angle_max");
            arDroneConfig.IndoorControlVzMax = GetValueFromControlConfig<float>("indoor_control_vz_max");
            arDroneConfig.IndoorControlYaw = GetValueFromControlConfig<float>("indoor_control_yaw");
            arDroneConfig.OutdoorEulerAngleMax = GetValueFromControlConfig<float>("outdoor_euler_angle_max");
            arDroneConfig.OutdoorControlVzMax = GetValueFromControlConfig<float>("outdoor_control_vz_max");
            arDroneConfig.OutdoorControlYaw = GetValueFromControlConfig<float>("outdoor_control_yaw");
            
            return arDroneConfig;
        }


        T GetValueFromControlConfig<T>(string valueName)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            StringReader stringReader = new StringReader(controlConfig);
            string line;
            while (stringReader.Peek() != -1)
            {
                line = stringReader.ReadLine();
                if (line == null)
                {
                    continue;
                }

                var words = line.Split('=');

                for (int i = 0; i < words.Length; i++)
                {
                    words[i] = words[i].Trim();
                }
                if (words.Contains(valueName))
                {
                    return (T)converter.ConvertFromString(words.Last());
                }
            }
         
            return default(T);
        }
    }
}
