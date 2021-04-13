using Reader;

namespace UHFDemo
{
    public class NetCommEventArgs
    {
        NET_COMM comm;
        public NetCommEventArgs(NET_COMM comm)
        {
            this.comm = comm;
        }

        public NET_COMM NetComm { get { return comm; } }
    }
}