using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ctrip.SOA.Infratructure.Utility;
using Arch.CFramework.Platform;
using System.Web;

namespace Ctrip.SOA.Infratructure
{
    public class CssSrcConfig
    {
        public static object objLock = new object();

        private static CssSrcConfig _instance;
        public static CssSrcConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (objLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new CssSrcConfig();
                            
                        }
                    }
                }
                return _instance;
            }
        }
        #if DEBUG
                public static string BookingWebResourcePath = (AppSetting.WebResourcePath.EndsWith("/") ? AppSetting.WebResourcePath : string.Format("{0}/", AppSetting.WebResourcePath));
        #else 
                public static string BookingWebResourcePath = (AppSetting.WebResourcePath.EndsWith("/") ? AppSetting.WebResourcePath : string.Format("{0}/", AppSetting.WebResourcePath))+"CSS/";
        #endif
        public string BookingBase
        {
            get
            {
                return BookingWebResourcePath + ResourcePlatForm.PlatformFileName("base.css") + "?" + AppSetting.ReleaseNo;
            }
        }

        public string BookingDetail
        {
            get
            {
                return BookingWebResourcePath + ResourcePlatForm.PlatformFileName("detail.css") + "?" + AppSetting.ReleaseNo;
            }
        }

        public string BookingIndex
        {
            get
            {
                return BookingWebResourcePath + ResourcePlatForm.PlatformFileName("index.css") + "?" + AppSetting.ReleaseNo;
            }
        }

        public string BookingOrder
        {
            get
            {
                return BookingWebResourcePath + ResourcePlatForm.PlatformFileName("order.css") + "?" + AppSetting.ReleaseNo;
            }
        }

        public string MyHH
        {
            get
            {
                return BookingWebResourcePath + ResourcePlatForm.PlatformFileName("myhh.css") + "?" + AppSetting.ReleaseNo;
            }
        }

        //public static readonly string BookingBase = BookingWebResourcePath + ResourcePlatForm.PlatformFileName("base.css") + "?" + AppSetting.ReleaseNo;
        //public static readonly string BookingDetail = BookingWebResourcePath + ResourcePlatForm.PlatformFileName("detail.css") + "?" + AppSetting.ReleaseNo;
        //public static readonly string BookingIndex = BookingWebResourcePath + ResourcePlatForm.PlatformFileName("index.css") + "?" + AppSetting.ReleaseNo;
        public static readonly string BookingTheme = BookingWebResourcePath + "theme.css?" + AppSetting.ReleaseNo;
        public static readonly string Booking_IE_Detail = BookingWebResourcePath + "ie_detail.css?" + AppSetting.ReleaseNo;
        public static readonly string Booking_IE_Index = BookingWebResourcePath + "ie_index.css?" + AppSetting.ReleaseNo;
        public static readonly string Booking_IE_Theme = BookingWebResourcePath + "ie_theme.css?" + AppSetting.ReleaseNo;
        //public static readonly string BookingOrder = BookingWebResourcePath + ResourcePlatForm.PlatformFileName("order.css") + "?" + AppSetting.ReleaseNo;
        public static readonly string Booking_IE_Order = BookingWebResourcePath + "ie_order.css?" + AppSetting.ReleaseNo;
        //MBT PROJECT
        public static readonly string MBTBookingBase = BookingWebResourcePath + "global.css?" + AppSetting.ReleaseNo;
        //MyHH
        //public static readonly string MyHH = BookingWebResourcePath + ResourcePlatForm.PlatformFileName("myhh.css") + "?" + AppSetting.ReleaseNo;
        public static readonly string PublicFlightLogo = BookingWebResourcePath + "public_flights_logo.css?" + AppSetting.ReleaseNo;

        public static readonly string OrderOfflineTheme = (AppSetting.WebResourcePath.EndsWith("/") ? AppSetting.WebResourcePath : string.Format("{0}/", AppSetting.WebResourcePath)) + "CSS/base.css?" + AppSetting.ReleaseNo;

        public static readonly string TeamOfflineTheme = (AppSetting.WebResourcePath.EndsWith("/") ? AppSetting.WebResourcePath : string.Format("{0}/", AppSetting.WebResourcePath)) + "CSS/base.css?" + AppSetting.ReleaseNo;

    }
}
