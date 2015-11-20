using System;
using System.Text;

namespace Ctrip.SOA.Infratructure.Utility
{
    public static class ExceptionHelper
    {
        public static string BuildExceptionMessage(Exception ex)
        {
            StringBuilder sb = new StringBuilder();

            if (ex is AggregateException)
            {
                ParseExceptionMessage(sb, ex as AggregateException);
            }
            else
            {
                ParseAllExceptionMessage(sb, ex);
            }

            return sb.ToString();
        }

        private static void ParseExceptionMessage(StringBuilder sb, AggregateException aex)
        {
            if (aex != null)
            {
                ParseCommonExceptionMessage(sb, aex);
                sb.AppendLine();
                sb.AppendLine();
                sb.AppendLine("InnerExceptions :");
                sb.Append("===============================");
                aex.Flatten().Handle((ex) =>
                {
                    ParseAllExceptionMessage(sb, ex);
                    sb.AppendLine();
                    return true;
                });
            }
        }

        private static void ParseAllExceptionMessage(StringBuilder sb, Exception ex)
        {
            ParseCommonExceptionMessage(sb, ex);
            Exception innerException = ex.InnerException;
            while (innerException != null)
            {
                sb.AppendLine();
                sb.AppendLine("InnerException :");
                sb.Append("------------------------------------------------");
                ParseCommonExceptionMessage(sb, innerException);
                innerException = innerException.InnerException;
                sb.AppendLine();
            }
        }

        private static void ParseCommonExceptionMessage(StringBuilder sb, Exception ex)
        {
            if (ex != null)
            {
                sb.AppendLine();
                sb.AppendFormat("{0} {1}", ex.GetType().FullName, ex.Message);
                sb.AppendLine();
                sb.Append("\tStack Trace : ");
                sb.AppendLine();
                sb.AppendFormat("{0}", ex.StackTrace);
                sb.AppendLine();
                sb.AppendFormat("\tSource : {0}", ex.Source);
                sb.AppendLine();
            }
        }
    }
}
