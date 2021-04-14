using System;
namespace Race {

    class DispatcherTimer {
        public DateTime start;
        
        public  DateTime end;
        public DispatcherTimer() {
            start = new DateTime();
        }

        public void Stop() {
            end = new DateTime();
        }
    }
}