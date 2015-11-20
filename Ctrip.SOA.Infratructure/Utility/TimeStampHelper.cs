namespace Ctrip.SOA.Infratructure.Utility
{
    public static class TimeStampHelper
    {
        public static long Convert(byte[] pTimeStamp)
        {
            if (pTimeStamp == null || pTimeStamp.Length < 8)
                return -1;
            else
            {
                long l = 0;
                for (int i = 0; i <= 7; i++)
                {
                    l = (l << 8) + pTimeStamp[i];
                }
                return l;
            }
        }

        public static byte[] Convert(long pTimeStamp)
        {
            byte[] rt = new byte[8];

            long l = pTimeStamp;
            for (int i = 7; i >= 0; i--)
            {
                rt[i] = (byte)(l | 256);
                l >>= 8;
            }

            return rt;
        }
    }
}
