namespace FollowMe.Configuration
{
    public class ArDroneConfig
    {
        private int altitudeMax;
        private int altitudeMin;
        private bool outdoor;
        private bool flightWithoutShell;
        private int controlLevel;
        private float eulerAngleMax;
        private float controlVzMax;
        private float controlYaw;
        private bool manualTrim;
        private float indoorEulerAngleMax;
        private float indoorControlVzMax;
        private float indoorControlYaw;
        private float outdoorEulerAngleMax;
        private float outdoorControlVzMax;
        private float outdoorControlYaw;


        public int AltitudeMax
        {
            get
            {
                return altitudeMax;
            }
            set { altitudeMax = value; }
        }

        public int AltitudeMin
        {
            get { return altitudeMin; }
            set { altitudeMin = value; }
        }

        public bool Outdoor
        {
            get { return outdoor; }
            set { outdoor = value; }
        }

        public bool FlightWithoutShell
        {
            get { return flightWithoutShell; }
            set { flightWithoutShell = value; }
        }

        public int ControlLevel
        {
            get { return controlLevel; }
            set { controlLevel = value; }
        }

        public float EulerAngleMax
        {
            get { return eulerAngleMax; }
            set { eulerAngleMax = value; }
        }

        public float ControlVzMax
        {
            get { return controlVzMax; }
            set { controlVzMax = value; }
        }

        public float ControlYaw
        {
            get { return controlYaw; }
            set { controlYaw = value; }
        }

        public bool ManualTrim
        {
            get { return manualTrim; }
            set { manualTrim = value; }
        }

        public float IndoorEulerAngleMax
        {
            get { return indoorEulerAngleMax; }
            set { indoorEulerAngleMax = value; }
        }

        public float IndoorControlVzMax
        {
            get { return indoorControlVzMax; }
            set { indoorControlVzMax = value; }
        }

        public float IndoorControlYaw
        {
            get { return indoorControlYaw; }
            set { indoorControlYaw = value; }
        }

        public float OutdoorEulerAngleMax
        {
            get { return outdoorEulerAngleMax; }
            set { outdoorEulerAngleMax = value; }
        }

        public float OutdoorControlVzMax
        {
            get { return outdoorControlVzMax; }
            set { outdoorControlVzMax = value; }
        }

        public float OutdoorControlYaw
        {
            get { return outdoorControlYaw; }
            set { outdoorControlYaw = value; }
        }
    }
}
