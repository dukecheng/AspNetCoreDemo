using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace AspNetCoreDemo.DemoWeb.Models
{
    public class LoginViewModel
    {
        public bool IsRemberme { get; set; }
        public RemeberOptions? RemberDays { get; set; }

        public LoginViewModel()
        {
            IsRemberme = true;
            RemberDays = RemeberOptions.OneWeek;
        }
    }

    /// <summary>
    /// 这里的中文是不建议的，后面需要重构
    /// </summary>
    public enum RemeberOptions
    {
        [Description("一天")]
        OneDay = 1,
        [Description("两天")]
        TwoDays = 2,
        [Description("三天")]
        ThreeDays = 3,
        [Description("一周")]
        OneWeek = 7,
        [Description("两周")]
        TwoWeeks = 14,
        [Description("一个月")]
        OneMonth = 30
    }

    public static class HtmlExtensions
    {
        public static IEnumerable<SelectListItem> GetEnumDescriptionSelectList<TEnum>(this IHtmlHelper htmlHelper) where TEnum : struct
        {
            var result = new List<SelectListItem>();

            var enumValues = Enum.GetValues(typeof(TEnum));
            foreach (var item in enumValues)
            {
                result.Add(new SelectListItem() { Text = GetDescription<TEnum>(item.ToString()), Value = item.ToString() });
            }

            return result;
        }

        public static string GetDescription<T>(string enumerationValue) where T : struct
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new ArgumentException($"{nameof(enumerationValue)} must be of Enum type", nameof(enumerationValue));
            }
            var memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return enumerationValue.ToString();
        }
    }
}
