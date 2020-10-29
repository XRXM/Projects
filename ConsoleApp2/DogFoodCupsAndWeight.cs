using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public class DogFoodCupsAndWeight
    {
        public double WeightOfFood { get; set; }
        public double CupsToFeedInaDay { get; set; }

        public DogFoodCupsAndWeight(double weight, double cupsADay)
        {
            WeightOfFood = weight;
            CupsToFeedInaDay = cupsADay;
        }



        /*Figure out cups for a day 
         * 4 cups equal 1 Lbs
         * Total weight divided by cups perday
         * remind user to order dog food once a day 
         * after food supply drops below 5 days worth of 
         * food until weight is goes higer than said threshold
         * 
         * 30lbs of dog food at 
         */
        public void DogFoodMessage(int daysLeft)
        {
            Message m = new Message();
            //int daysLeft = 0;

            string niceDaysleft = ($"You have approximately {(int)daysLeft} days worth of dog food remaining.");
            string orderFood = ($"You have approximately {(int)daysLeft} days worth of dog food remaining! Order Food!");
            string foodOut = ($"You are out of Food.");
            if (daysLeft <= 5)
            {
                m.SendingMessage(niceDaysleft);

            }
            else if (daysLeft >= 2)
            {
                m.SendingMessage(orderFood);

            }
            else if (daysLeft <= 0)
            {
                m.SendingMessage(foodOut);

            }




        }

        public int DaysOfDogFoodLeft(double weight, double cupsADay)
        {

            double daysLeft = 0;
            daysLeft = weight / (cupsADay / 4);

            return (int)daysLeft;

        }

        public double kgToLb(double weight)
        {
            double lbs = 0;
            lbs = weight * 2.2046226218;
            return lbs;
        }

    }
}
