using System;
using System.Collections.Generic;
using System.Text;

namespace RestfulBookerUITests.TestData
{
    public class Data
    {
        public Data()
        {
            MyMessage = new ContactUsMessage();
            MyRoomBooking = new RoomBooking();
        }

        public ContactUsMessage MyMessage;
        public RoomBooking MyRoomBooking;
    }
}