using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ctrip.SOA.Infratructure.Consts
{
    public class Consts
    {
    }

    /// <summary>
    /// 可上传的图片信息
    /// </summary>
    public static class ImagePixelInfo
    { 
        //首页产品大图
        public const string MBT_Home_Prd_Big="1280*670";
        //首页产品列表次序为5的倍数的产品大图
        public const string MBT_Home_Last_Prd = "1000*430";
        //详情页产品大图
        public const string MBT_Detail_Prd_Big = "1280*670";
        //详情页路线图
        public const string MBT_Detail_Prd_Route = "1024*430";
        //列表页产品的缩略图
        public const string MBT_AllTrip_Prd_Small = "236*158";
        //行程图（大）
        public const string MBT_TourDaily_Big = "586*430";
        //行程图（小）
        public const string MBT_TourDaily_Small = "383*235";
    }
}
