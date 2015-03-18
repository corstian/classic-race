using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Classic_Race
{
    class GameLogic
    {

        #region Variables

        private string _page = "main";

        // The speed is the numer of pixels which goes by each frame
        // 3*30 = 90px each second
        private int _speed = 3;
        // The pixel var is used by the traffic generator
        private int _pixels;
        // The level defines how much traffic is generated and which speed is used.
        private int _level = 1;

        private int _loop = 0;

        private int _screenWidth = 480;
        private int _screenHeight = 800;

        private int _lanes = 3;
        private int _laneWidth = 96;

        private int _roadWidth;
        private int _carWidth;
        private int _midlineWidth;
        private int _sidelineWidth;
        private int _carMaxLeftLocation;
        private int _carMaxRightLocation;

        private int _leftLineXLocation;
        private int _rightLineXLocation;

        public Vector2 _leftLineVector { get; set; }
        public Vector2 _rightLineVector { get; set; }

        public List<Traffic> Traffic { get; set; }
        public Vector2 CarLocation { get; set; }
        public string Car { get; set; }
        public Vector2 ExplosionVector { get; set; }
        
#endregion

        public GameLogic()
        {
            _roadWidth = _lanes*_laneWidth;

            _carWidth = _laneWidth - 20;
            _midlineWidth = (_laneWidth-_carWidth)/4;
            _sidelineWidth = _midlineWidth*2;

            // +1 to keep the car on the road.
            _carMaxLeftLocation = (_screenWidth-_roadWidth)/2 + 1;
            // Also -2 to keep the car on the road. (To be tested)
            _carMaxRightLocation = _carMaxLeftLocation + _roadWidth - 2;

            _leftLineXLocation = _roadWidth - _sidelineWidth;
            _rightLineXLocation = _roadWidth;

            _leftLineVector = new Vector2(_leftLineXLocation, -80);
            _rightLineVector = new Vector2(_rightLineXLocation, -40);

            // Just a little check
            if (_roadWidth < _screenWidth)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void Game()
        {
            SideLineVectors();
            GenerateTraffic();
            UpdateTrafficLocation();
            Timer();
            DetectCollisions();
        }

        public void SideLineVectors()
        {
            if (_leftLineVector.Y == 0)
            {
                _leftLineVector += new Vector2(0, -80);
            }
            if (_rightLineVector.Y == 0)
            {
                _rightLineVector += new Vector2(0, -80);
            }

            _leftLineVector += new Vector2(0, _speed);
            _rightLineVector += new Vector2(0, _speed);
        }

        public void GenerateTraffic()
        {
            var rand = new Random();

            /*
             * We need to generate a car once in a specified distance. The best thing we can do is to calculate how many pixels passed by and generate cars depending on that number.
             */
            _pixels += _speed;

            if (_pixels >= 100 || rand.Next(50) == 1)
            {
                // reset the pixel count
                _pixels = 0;

                // Choose a random lane based on the number of lanes. (3)
                int lane = rand.Next(_lanes);

                /* 
                 * The first part is to get the most left part where the car has to spawn.
                 * Next part adds a little margin so the car is aligned in the middle of the lane. 
                 * The last part choosed the used lane.
                 */
                int laneXLocation = ((_screenWidth - _roadWidth)/2) + ((_laneWidth - _carWidth)/2) + (_laneWidth*(lane - 1));

                Traffic traffic = new Traffic();

                // - 100 as we don't exactly know how long the car is. Doesn't matter. Just a little bit more memory usage.
                traffic.Location = new Vector2(laneXLocation, -100);
                traffic.Speed = rand.Next(5);
                // This is hardcoded for now. It's possible the cars are choosen randomly somewhere in the future.
                traffic.Car = "pixel_car";

                Traffic.Add(traffic);
            }
        }

        public void UpdateTrafficLocation()
        {
            List<Traffic> tempTraffic = new List<Traffic>();
         
            foreach (Traffic traffic in Traffic)
            {
                // If the car is still on the screen
                if (traffic.Location.Y <= 800)
                {
                    // Add some speed to make it difficult
                    traffic.Location += new Vector2(0, traffic.Speed + _speed);
                    tempTraffic.Add(traffic);
                }
            }

            Traffic.Clear();

            Traffic = tempTraffic;
        }

        public void Timer()
        {
            // Once in the 5 seconds the speed is updated
            if (_loop >= 150)
            {
                _speed = _speed + 1;
                _loop = 0;
            }
            _loop++;
        }

        public void DetectCollisions()
        {
            // ToDo: Get the exact location of the collision
            foreach (Traffic traffic in Traffic)
            {
                if (traffic.Location.Y + 40 >= 300 && traffic.Location.Y <= 340)
                {
                    if (traffic.Location.X >= CarLocation.X || traffic.Location.X <= CarLocation.X) //Traffic is right or left
                    {
                        if (CarLocation.X + 29 >= traffic.Location.X || traffic.Location.X + _carWidth >= CarLocation.X)
                        {
                            // Reset variables
                            _page = "game_over";
                            _speed = 3;
                            _loop = 0;
                            Traffic.Clear();
                            // ToDo: Offcourse don't forget to display a nice little explosion :)
                            return;
                        }
                    }
                }
            }
        }

        public int GetScreenWidth()
        {
            return _screenWidth;
        }

        public int GetScreenHeight()
        {
            return _screenHeight;
        }
    }
}
