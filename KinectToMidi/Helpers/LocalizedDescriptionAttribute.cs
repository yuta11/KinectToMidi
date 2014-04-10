using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace KinectToMidi.Helpers
{
    /// <summary>
    /// Descritption attribute
    /// </summary>
    public class LocalizedDescriptionAttributre : DescriptionAttribute
    {
        private readonly string m_resourceKey;
        private readonly ResourceManager m_resource;
        public LocalizedDescriptionAttributre(string resourceKey, Type resourceType)
        {
            m_resource = new ResourceManager(resourceType);
            m_resourceKey = resourceKey;
        }

        public override string Description
        {
            get
            {
                string displayName = m_resource.GetString(m_resourceKey);

                return string.IsNullOrEmpty(displayName)
                    ? string.Format("[[{0}]]", m_resourceKey)
                    : displayName;
            }
        }
    }

    /// <summary>
    /// Enum extension to getting description
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Get value description via LocalizedDescription attribute
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumValue)
        {
            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return enumValue.ToString();
        }
    }
}
