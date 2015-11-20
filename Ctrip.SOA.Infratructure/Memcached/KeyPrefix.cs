using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ctrip.SOA.Infratructure.Memcached
{
    /// <summary>
    /// 缓存key前缀
    /// </summary>
    public enum KeyPrefix
    {
        #region HHTravel.Online.HHServiceClient
        /// <summary>
        /// 首页导航
        /// </summary>
        HHBooking_GetIndexNavResponse,
        /// <summary>
        /// 旅游主题
        /// </summary>
        HHBooking_GetInterestNavResponse,
        /// <summary>
        /// 目的地
        /// </summary>
        HHBooking_GetDestinationNavResponse,
        /// <summary>
        /// 查找产品
        /// </summary>
        HHBooking_SearchProducts,
        /// <summary>
        /// 产品数量
        /// </summary>
        HHBooking_GetHHProductCountList,
        /// <summary>
        /// 产品详情
        /// </summary>
        HHBooking_GetProductDetail,
        /// <summary>
        /// 产品日历
        /// </summary>
        HHBooking_GetProductDailyInfo,
        /// <summary>
        /// 产品酒店
        /// </summary>
        HHBooking_GetProductHotel,
        /// <summary>
        /// 产品所有酒店
        /// </summary>
        HHBooking_GetProductAllHotels,
        /// <summary>
        /// 产品成本描述
        /// </summary>
        HHBooking_GetProductCostDescription,
        /// <summary>
        /// 所有出发城市
        /// </summary>
        HHBooking_GetAllDepartureCityList,
        #endregion

        #region HHRepository.Product
        /// <summary>
        /// 产品视频
        /// </summary>
        HHRepProduct_GetVideoList,
        /// <summary>
        /// 获取标签
        /// </summary>
        HHRepProduct_GetTagList,
        /// <summary>
        /// 根据搜索条件获取产品基本信息
        /// </summary>
        HHRepProduct_GetBaseInfoPropertyList,
        /// <summary>
        /// 获取指定出发地+目的地国家的产品COUNT
        /// </summary>
        HHRepProduct_GetProductCountByDCAndDest,
        /// <summary>
        /// 获取指定出发地+目的地国家的产品COUNT
        /// </summary>
        HHRepProduct_GetHHProductCountList,
        #endregion
    }
}
