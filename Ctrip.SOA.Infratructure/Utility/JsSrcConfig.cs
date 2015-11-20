using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Ctrip.SOA.Infratructure.Utility;


namespace Ctrip.SOA.Infratructure
{
    public class JsSrcConfig
    {
        public static object objLock = new object();

        private static JsSrcConfig _instance;
        public static JsSrcConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (objLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new JsSrcConfig();
                        }
                    }
                }
                return _instance;
            }
        }

#if DEBUG
        public static string BookingWebResourcePath = HttpContext.Current.Request.ApplicationPath.EndsWith("/") ? HttpContext.Current.Request.ApplicationPath : string.Format("{0}/", HttpContext.Current.Request.ApplicationPath);
#else
        public static string BookingWebResourcePath = AppSetting.WebResourcePath.EndsWith("/") ? AppSetting.WebResourcePath : string.Format("{0}/", AppSetting.WebResourcePath);
#endif   

     
        //public static readonly string CommonCqueryMin = "http://webresource.c-ctrip.com/code/cquery/" + ResourcePlatForm.PlatformFileName("cQuery_110421.js") + "?" + AppSetting.ReleaseNo;
        public static readonly string CommonJqueryMin = BookingWebResourcePath + "js/common/jquery.min.js?" + AppSetting.ReleaseNo;
        public static readonly string CommonJqueryUnobtrusiveAjaxMin = BookingWebResourcePath + "js/common/jquery.unobtrusive-ajax.min.js?" + AppSetting.ReleaseNo;
        public static readonly string CommonJqueryValidateMin = BookingWebResourcePath + "js/common/jquery.validate.min.js?" + AppSetting.ReleaseNo;
        public static readonly string CommonJqueryValidateUnobtrusiveMin = BookingWebResourcePath + "js/common/jquery.validate.unobtrusive.min.js?" + AppSetting.ReleaseNo;
        public static readonly string Commonknockout = BookingWebResourcePath + "js/common/knockout-2.2.1.js?" + AppSetting.ReleaseNo;
        public static readonly string CommonShared = BookingWebResourcePath + "JS/Common/shared.js?" + AppSetting.ReleaseNo;
        public static readonly string CommonCheckUserAgent = BookingWebResourcePath + "JS/Common/checkUserAgent.js?" + AppSetting.ReleaseNo;

        public static readonly string HomeDestination = BookingWebResourcePath + "js/home/destination.js?" + AppSetting.ReleaseNo;
        //public static readonly string HomeIndex = BookingWebResourcePath + "JS/Home/" + ResourcePlatForm.PlatformFileName("index.js") + "?" + AppSetting.ReleaseNo;
        public static readonly string HomeInterest = BookingWebResourcePath + "js/home/interest.js?" + AppSetting.ReleaseNo;
        public static readonly string V1Common = BookingWebResourcePath + "js/v1/common.js?" + AppSetting.ReleaseNo;

        public static readonly string ProductDetail = BookingWebResourcePath + "js/product/detail.js?" + AppSetting.ReleaseNo;
        public static readonly string ProductDetail_cal = BookingWebResourcePath + "JS/Product/detail_cal.js?" + AppSetting.ReleaseNo;
        public static readonly string ProductDetail_hotel_image = BookingWebResourcePath + "JS/Product/detail_hotel_image.js?" + AppSetting.ReleaseNo;
        public static readonly string ProductDetail_hotel_map = BookingWebResourcePath + "js/product/detail_hotel_map.js?" + AppSetting.ReleaseNo;
        public static readonly string ProductDetail_hotel_mapv2 = BookingWebResourcePath + "js/product/detail_hotel_mapv2.js?" + AppSetting.ReleaseNo;
        public static readonly string ProductDetail_journey = BookingWebResourcePath + "JS/Product/detail_journey.js?" + AppSetting.ReleaseNo;
        //public static readonly string ProductDetail_media = BookingWebResourcePath + "js/product/detail_media.js?" + AppSetting.ReleaseNo;
        public static readonly string ProductSearch = BookingWebResourcePath + "js/product/search.js?" + AppSetting.ReleaseNo;

        public static readonly string CommonJqueryExt = BookingWebResourcePath + "js/common/jqueryExt.js?" + AppSetting.ReleaseNo;
        public static readonly string ProductDetail_SettingSegmentPop = BookingWebResourcePath + "js/product/SettingSegmentPop.js?" + AppSetting.ReleaseNo;
        public static readonly string Booking_AdditionalResource = BookingWebResourcePath + "js/booking/AdditionalResource.js?" + AppSetting.ReleaseNo;
        public static readonly string Booking_NationalitySmartTip = BookingWebResourcePath + "js/booking/NationalitySmartTip.js?" + AppSetting.ReleaseNo;
        public static readonly string Booking_LoadCustomer = BookingWebResourcePath + "js/booking/LoadCustomer.js?" + AppSetting.ReleaseNo;
        public static readonly string Booking_ProductSider = BookingWebResourcePath + "js/booking/ProductSider.js?" + AppSetting.ReleaseNo;
        public static readonly string Booking_Order0 = BookingWebResourcePath + "js/booking/Order0.js?" + AppSetting.ReleaseNo;
        public static readonly string Booking_Order1 = BookingWebResourcePath + "js/booking/Order1.js?" + AppSetting.ReleaseNo;

        //预订新流程
        public static readonly string Booking_Order0A = BookingWebResourcePath + "js/booking/Order0A.js?" + AppSetting.ReleaseNo;
        public static readonly string Booking_Order1A = BookingWebResourcePath + "js/booking/Order1A.js?" + AppSetting.ReleaseNo;
        public static readonly string Complete_A = BookingWebResourcePath + "js/Complete/Complete.js?" + AppSetting.ReleaseNo;

        //私人定制
        public static readonly string Customized = BookingWebResourcePath + "js/product/Customized.js?" + AppSetting.ReleaseNo;
    }
}
