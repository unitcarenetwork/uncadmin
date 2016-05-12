﻿using System;
using System.ComponentModel;
using System.Configuration;

namespace UnitcareNetworkAdmin.UI.Providers
{
    public interface IConfigProvider
    {
        string GetAppSetting(string propertyName);
        T GetAppSetting<T>(string propertyName) where T : struct;
    }
    public class ConfigProvider : IConfigProvider
    {
        public string GetAppSetting(string propertyName)
        {
            return ConfigurationManager.AppSettings.Get(propertyName);
        }

        public T GetAppSetting<T>(string propertyName) where T : struct
        {
            if (propertyName == null)
                throw new ArgumentNullException("propertyName");

            var value = GetAppSetting(propertyName);

            T returnVal = default(T);
            if (string.IsNullOrEmpty(value))
                return returnVal;

            returnVal = TryParse<T>(value);
            return returnVal;
        }

        private T TryParse<T>(string s)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            try
            {
                return (T)converter.ConvertFromString(s);
            }
            catch
            {
                return default(T);
            }
        }
    }
}