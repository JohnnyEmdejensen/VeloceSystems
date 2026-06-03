namespace Veloce
{
    public class Core
    {
        public static long ConvertDateTimeToLong(DateTime value)
        {
            long result = 0;
            var t = string.Format("{0:0000}{1:00}{2:00}{3:00}{4:00}{5:00}", value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second);
            result = Convert.ToInt64(t);
            return result;
        }
    }
}
