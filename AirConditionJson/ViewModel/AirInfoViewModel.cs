using GalaSoft.MvvmLight;

namespace AirConditionJson.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class AirInfoViewModel : ViewModelBase
    {
        private AirInfo airInfo;
        /// <summary>
        /// Initializes a new instance of the AirInfoViewModel1 class.
        /// </summary>
        public AirInfoViewModel(AirInfo airInfo)
        {
            this.airInfo = airInfo;
        }

        public string AQI
        {
            get
            {
                return airInfo.AQI;
            }
        }

        public string Quality
        {
            get
            {
                return airInfo.Quality;
            }
        }

        public string Date
        {
            get
            {
                return airInfo.Date;
            }
        }

        public string City
        {
            get
            {
                return airInfo.City;
            }
        }
    }
}